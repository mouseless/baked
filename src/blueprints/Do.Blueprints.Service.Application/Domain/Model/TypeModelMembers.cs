using Do.Domain.Configuration;
using System.Reflection;

namespace Do.Domain.Model;

public class TypeModelMembers : TypeModelMetadata
{
    public List<ConstructorModel> Constructors { get; private set; } = default!;
    public ModelCollection<PropertyModel> Properties { get; private set; } = default!;
    public GroupedModelCollection<MethodModel> Methods { get; private set; } = default!;

    public ConstructorModel GetConstructor() =>
        Constructors.Single();

    public MethodModel GetMethod(string name) =>
       GetMethods(name).Single();

    public IEnumerable<MethodModel> GetMethods(string name) =>
        Methods.TryGetGroup(name, out var result) ? result : [];

    public new class Factory : TypeModelMetadata.Factory
    {
        protected override TypeModelMembers Create() => new();

        protected override void Fill(TypeModel result, Type type, DomainModelBuilder builder)
        {
            base.Fill(result, type, builder);

            if (result is not TypeModelMembers members) { return; }

            members.Constructors = BuildConstructorGroup();
            members.Properties = new(type.GetProperties(builder.Options.BindingFlags.Property).Select(BuildProperty));
            members.Methods = BuildMethods();

            List<ConstructorModel> BuildConstructorGroup()
            {
                var constructorInfos = type.GetConstructors(builder.Options.BindingFlags.Constructor) ?? [];
                if (!constructorInfos.Any()) { return []; }

                return [.. constructorInfos.Select(BuildConstructor)];
            }

            ConstructorModel BuildConstructor(ConstructorInfo constructorInfo)
            {
                return new(
                    constructorInfo.IsPublic,
                    constructorInfo.IsFamily,
                    new(constructorInfo.GetCustomAttributes()),
                    BuildParameters(constructorInfo)
                );
            }

            PropertyModel BuildProperty(PropertyInfo property) =>
                new(
                    property.Name,
                    builder.Get(property.PropertyType),
                    property.GetMethod?.IsPublic == true,
                    property.GetMethod?.IsVirtual == true,
                    new(property.GetCustomAttributes())
                );

            GroupedModelCollection<MethodModel> BuildMethods()
            {
                var methodGroups = new Dictionary<string, IEnumerable<MethodModel>>();
                var methodInfos = type.GetMethods(builder.Options.BindingFlags.Method) ?? [];
                foreach (var group in methodInfos.GroupBy(m => m.Name))
                {
                    var methods = group.Select(BuildMethod);
                    methodGroups[group.Key] = methods;
                }

                return new(methodGroups);
            }

            MethodModel BuildMethod(MethodInfo methodInfo)
            {
                return new(
                    methodInfo.Name,
                    methodInfo.IsPublic,
                    methodInfo.IsFamily,
                    methodInfo.IsVirtual,
                    builder.Get(methodInfo.ReturnType),
                    new(methodInfo.GetCustomAttributes()),
                    BuildParameters(methodInfo)
                );
            }

            ModelCollection<ParameterModel> BuildParameters(MethodBase method) =>
                new(method.GetParameters().Select(BuildParameter));

            ParameterModel BuildParameter(ParameterInfo parameter) =>
                new(
                    parameter.Name ?? string.Empty,
                    builder.Get(parameter.ParameterType),
                    parameter.IsOptional,
                    parameter.DefaultValue,
                    new(parameter.Member.GetCustomAttributes())
                );
        }
    }
}