using Do.Domain.Configuration;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Do.Domain.Model;

public class TypeModelMembers : TypeModelMetadata
{
    public ReadOnlyCollection<ConstructorModel> Constructors { get; private set; } = default!;
    public ModelCollection<PropertyModel> Properties { get; private set; } = default!;
    public ModelCollection<MethodModel> Methods { get; set; } = default!;

    public ConstructorModel GetConstructor() =>
        Constructors.Single();

    public bool TryGetMethod(string name, [NotNullWhen(true)] out MethodOverloadModel? result)
    {
        if (!TryGetMethods(name, out var methods))
        {
            result = null;

            return false;
        }

        result = methods.SingleOrDefault();

        return result is not null;
    }

    public MethodOverloadModel GetMethod(string name) =>
       GetMethods(name).Single();

    public bool TryGetMethods(string name, [NotNullWhen(true)] out IEnumerable<MethodOverloadModel>? result)
    {
        if (!Methods.TryGetValue(name, out var method))
        {
            result = null;

            return false;
        }

        result = method.Overloads;

        return true;
    }

    public IEnumerable<MethodOverloadModel> GetMethods(string name) =>
        Methods[name].Overloads;

    public new class Factory : TypeModelMetadata.Factory
    {
        protected override TypeModelMembers Create() => new();

        protected override void Fill(TypeModel result, Type type, DomainModelBuilder builder)
        {
            base.Fill(result, type, builder);

            if (result is not TypeModelMembers members) { return; }

            members.Constructors = BuildConstructors();
            members.Properties = new(type.GetProperties(builder.Options.BindingFlags.Property).Select(BuildProperty));
            members.Methods = BuildMethods();

            ReadOnlyCollection<ConstructorModel> BuildConstructors()
            {
                var constructorInfos = type.GetConstructors(builder.Options.BindingFlags.Constructor) ?? [];
                if (!constructorInfos.Any()) { return ReadOnlyCollection<ConstructorModel>.Empty; }

                return constructorInfos.Select(BuildConstructor).ToList().AsReadOnly();
            }

            ConstructorModel BuildConstructor(ConstructorInfo constructorInfo)
            {
                return new(
                    constructorInfo.IsPublic,
                    constructorInfo.IsFamily,
                    BuildParameters(constructorInfo)
                );
            }

            PropertyModel BuildProperty(PropertyInfo property)
            {
                return new(
                    property.Name,
                    builder.GetReference(property.PropertyType),
                    property.GetMethod?.IsPublic == true,
                    property.GetMethod?.IsVirtual == true,
                    new(property.GetCustomAttributes()),
                    apply => apply(property)
                );
            }

            ModelCollection<MethodModel> BuildMethods()
            {
                var result = new ModelCollection<MethodModel>.KeyedCollection();
                var methodInfos = type.GetMethods(builder.Options.BindingFlags.Method) ?? [];
                foreach (var methodsByName in methodInfos.GroupBy(m => m.Name))
                {
                    var overloads = methodsByName.Select(BuildMethod).ToList().AsReadOnly();
                    result.Add(new(
                        methodsByName.Key,
                        builder.Options.DefaultOverloadSelector(overloads),
                        overloads,
                        new(methodsByName.SelectMany(m => m.GetCustomAttributes()))
                    ));
                }

                return new(result);
            }

            MethodOverloadModel BuildMethod(MethodInfo methodInfo)
            {
                var baseDefinition = methodInfo.GetBaseDefinition();
                if (baseDefinition == methodInfo)
                {
                    baseDefinition = null;
                }

                return new(
                    methodInfo.IsPublic,
                    methodInfo.IsFamily,
                    methodInfo.IsVirtual,
                    methodInfo.IsStatic,
                    methodInfo.IsSpecialName,
                    BuildParameters(methodInfo),
                    builder.GetReference(methodInfo.ReturnType),
                    methodInfo.DeclaringType is not null ? builder.GetReference(methodInfo.DeclaringType) : null,
                    baseDefinition is not null ? BuildMethod(baseDefinition) : null,
                    apply => apply(methodInfo)
                );
            }

            ModelCollection<ParameterModel> BuildParameters(MethodBase method)
            {
                return new(method.GetParameters().Select(BuildParameter));
            }

            ParameterModel BuildParameter(ParameterInfo parameter)
            {
                return new(
                    parameter.Name ?? string.Empty,
                    builder.GetReference(parameter.ParameterType),
                    parameter.IsOptional,
                    parameter.DefaultValue,
                    new(parameter.Member.GetCustomAttributes()),
                    apply => apply(parameter)
                );
            }
        }
    }
}