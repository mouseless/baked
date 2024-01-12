using System.Reflection;

namespace Do.Domain.Model;

public class DomainModel
{
    public ModelCollection<AssemblyModel> Assemblies { get; } = [];
    public ModelCollection<TypeModel> Types { get; } = [];

    internal TypeModel AddTypeModel(Type type)
    {
        var typeModel = new TypeModel(type);
        Types.Add(typeModel);

        if (typeModel.Namespace.Contains("System")) { return typeModel; }

        typeModel.Apply(type =>
        {
            foreach (var genericArgument in type.GenericTypeArguments)
            {
                typeModel.GenericTypeArguments.Add(GetOrAddTypeModel(genericArgument));
            }

            foreach (var attributeData in type.GetCustomAttributesData())
            {
                typeModel.CustomAttributes.Add(GetOrAddTypeModel(attributeData.AttributeType));
            }

            var constructorInfos = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];
            foreach (var constructor in constructorInfos)
            {
                typeModel.Constructors.Add(new(constructor.Name, typeModel, ExtractParameters(constructor)));
            }

            var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];
            foreach (var property in propertyInfos)
            {
                typeModel.Properties.Add(new(property.Name, GetOrAddTypeModel(property.PropertyType), property.CanRead));
            }

            var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];
            foreach (var method in methodInfos)
            {
                typeModel.Methods.Add(new(method.Name, GetOrAddTypeModel(method.ReturnType), method.IsPublic, ExtractParameters(method), ExtractCustomAttributes(method)));
            }
        });

        return typeModel;
    }

    TypeModel GetOrAddTypeModel(Type type)
    {
        if (Types.TryGetValue(type.FullName ?? string.Empty, out var result))
        {
            return result;
        }

        return AddTypeModel(type);
    }

    List<ParameterModel> ExtractParameters(MethodBase method) =>
        method.GetParameters().Select(p => new ParameterModel(p.Name ?? string.Empty, GetOrAddTypeModel(p.ParameterType), p.IsOptional, p.DefaultValue)).ToList();
    List<TypeModel> ExtractCustomAttributes(MemberInfo member) =>
        member.GetCustomAttributesData().Select(a => GetOrAddTypeModel(a.AttributeType)).ToList();
}
