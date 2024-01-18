using System.Reflection;

namespace Do.Domain.Model;

public class DomainModelBuilder(DomainBuilderOptions _domainBuilderOptions)
{
    readonly KeyedModelCollection<AssemblyModel> _assemblies = [];
    readonly KeyedModelCollection<TypeModel> _types = [];

    public DomainModel BuildFrom(IAssemblyCollection assemblyCollection, ITypeCollection typeCollection)
    {
        foreach (var assemblyDescriptor in assemblyCollection)
        {
            var model = new AssemblyModel(assemblyDescriptor.Assembly);
            _assemblies.Add(model);
        }

        foreach (var typeDescriptor in typeCollection)
        {
            var id = IdFrom(typeDescriptor.Type);
            if (_types.Contains(id)) { continue; }

            var model = new TypeModel(typeDescriptor.Type, id);
            _types.Add(model);

            BuildTypeModel(model, typeDescriptor.Type);
        }

        return new(new(_assemblies), new(_types));
    }

    void BuildTypeModel(TypeModel typeModel, Type type)
    {
        if (_domainBuilderOptions.TypeIsBuiltConventions.Any(c => !c(typeModel))) { return; }

        typeModel.Init(
            genericTypeArguments: BuildGenericTypeArguments(type),
            customAttributes: BuildCustomAttributes(type),
            properties: BuildProperties(type),
            methods: BuildMethods(type)
        );
    }

    TypeModel GetOrCreateTypeModel(Type type)
    {
        var id = IdFrom(type);
        if (_types.TryGetValue(id, out var result)) { return result; }

        var typeModel = new TypeModel(type, id);
        _types.Add(typeModel);

        BuildTypeModel(typeModel, type);

        return typeModel;
    }

    ModelCollection<MethodModel> BuildMethods(Type type)
    {
        var result = new Dictionary<string, MethodModel>();

        var constructorInfos = type.GetConstructors(_domainBuilderOptions.ConstuctorBindingFlags) ?? [];

        result[".ctor"] = new(".ctor", true, constructorInfos.Select(BuildConstructorOverload).ToArray());

        var methodInfos = type.GetMethods(_domainBuilderOptions.MethodBindingFlags) ?? [];
        foreach (var group in methodInfos.GroupBy(m => m.Name))
        {
            result[group.Key] = new(group.Key, false, group.Select(BuildMethodOverload).ToArray());
        }

        return new(result.Values);
    }

    ModelCollection<TypeModel> BuildCustomAttributes(MemberInfo member) =>
        new(member.CustomAttributes.Select(attr => GetOrCreateTypeModel(attr.AttributeType)));

    ModelCollection<TypeModel> BuildGenericTypeArguments(Type type) =>
        new(type.GenericTypeArguments.Select(GetOrCreateTypeModel));

    OverloadModel BuildConstructorOverload(ConstructorInfo constructor) =>
        new(constructor.IsPublic, constructor.IsFamily, constructor.IsVirtual, new(BuildParameters(constructor)), new(BuildCustomAttributes(constructor)));

    OverloadModel BuildMethodOverload(MethodInfo method) =>
        new(method.IsPublic, method.IsFamily, method.IsVirtual, new(BuildParameters(method)), new(BuildCustomAttributes(method)), GetOrCreateTypeModel(method.ReturnType));

    ModelCollection<ParameterModel> BuildParameters(MethodBase method) =>
        new(method.GetParameters().Select(p => new ParameterModel(p.Name ?? string.Empty, GetOrCreateTypeModel(p.ParameterType), p.IsOptional, p.DefaultValue)));

    ModelCollection<PropertyModel> BuildProperties(Type type) =>
        new(type.GetProperties(_domainBuilderOptions.PropertyBindingFlags).Select(BuildProperty));

    PropertyModel BuildProperty(PropertyInfo property) =>
        new(property.Name, GetOrCreateTypeModel(property.PropertyType), IsPublic(property), IsVirtual(property));

    static bool IsPublic(PropertyInfo source) =>
        source.GetMethod?.IsPublic == true;

    static bool IsVirtual(PropertyInfo source) =>
        source.GetMethod?.IsVirtual == true;

    static string IdFrom(Type type) =>
        type.FullName ?? $"{type.Namespace}.{type.Name}[{string.Join(',', type.GenericTypeArguments.Select(IdFrom))}]";
}


