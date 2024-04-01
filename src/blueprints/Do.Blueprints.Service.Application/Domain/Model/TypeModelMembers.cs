using Do.Domain.Configuration;
using System.Reflection;

namespace Do.Domain.Model;

public class TypeModelMembers : TypeModelMetadata
{
    public List<ConstructorModel> Constructors { get; private set; } = default!;
    public ModelCollection<PropertyModel> Properties { get; private set; } = default!;
    public ModelCollection<MethodGroupModel> MethodGroups { get; private set; } = default!;

    public ConstructorModel GetConstructor() =>
        Constructors.Single();

    public MethodModel GetMethod(string name) =>
        MethodGroups[name].Methods.Single();

    public IEnumerable<MethodModel> GetMethods(string name) =>
        MethodGroups[name].Methods;

    public new class Factory : TypeModelMetadata.Factory
    {
        protected override TypeModelMembers Create() => new();

        protected override void Fill(TypeModel result, Type type, DomainModelBuilder builder)
        {
            base.Fill(result, type, builder);

            if (result is not TypeModelMembers members) { return; }

            members.Constructors = BuildConstructorGroup();
            members.Properties = new(type.GetProperties(builder.Options.BindingFlags.Property).Select(BuildProperty));
            members.MethodGroups = BuildMethodGroups();

            List<ConstructorModel> BuildConstructorGroup()
            {
                var constructorInfos = type.GetConstructors(builder.Options.BindingFlags.Constructor) ?? [];
                if (!constructorInfos.Any()) { return []; }

                return [.. constructorInfos.Select(BuildConstructor)];
            }

            ConstructorModel BuildConstructor(ConstructorInfo constructorInfo)
            {
                return new(
                    "ctor",
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

            ModelCollection<MethodGroupModel> BuildMethodGroups()
            {
                var methodGroups = new Dictionary<string, MethodGroupModel>();
                var methodInfos = type.GetMethods(builder.Options.BindingFlags.Method) ?? [];
                foreach (var group in methodInfos.GroupBy(m => m.Name))
                {
                    var methods = group.Select(BuildMethod).ToList();
                    methodGroups[group.Key] = new(
                        group.Key,
                        methods,
                        new(methods.SelectMany(m => m.CustomAttributes.SelectMany(a => a.Attributes)))
                    );
                }

                return new(methodGroups.Values);
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