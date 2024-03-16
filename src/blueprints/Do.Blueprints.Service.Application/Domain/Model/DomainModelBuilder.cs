using System.Reflection;

namespace Do.Domain.Model;

public class DomainModelBuilder(DomainBuilderOptions _domainBuilderOptions)
{
    readonly KeyedModelCollection<AssemblyModel> _assemblies = [];
    readonly KeyedModelCollection<TypeModel> _types = [];

    public DomainModel BuildFrom(IAssemblyCollection assemblyCollection, ITypeCollection typeCollection)
    {
        foreach (var assembly in assemblyCollection)
        {
            _assemblies.Add(new(assembly));

            foreach (var type in assembly.GetExportedTypes())
            {
                typeCollection.Add(type);
            }
        }

        foreach (var type in typeCollection)
        {
            _types.Add(new(type, TypeModel.IdFrom(type), assembly: _assemblies.GetOrDefault(type.Assembly.FullName), isBusinessType: true));
        }

        foreach (var type in _types.ToList())
        {
            type.Apply(t =>
                type.Init(
                    genericTypeArguments: BuildGenericTypeArguments(t),
                    customAttributes: BuildCustomAttributes(t),
                    properties: BuildProperties(t),
                    methods: BuildMethods(t),
                    interfaces: BuildInterfaces(t),
                    baseType: t.BaseType is not null ? GetOrCreateTypeModel(t.BaseType) : default
                )
            );
        }

        return new(new(_assemblies), new(_types));
    }

    TypeModel GetOrCreateTypeModel(Type type)
    {
        var id = TypeModel.IdFrom(type);
        if (_types.TryGetValue(id, out var result)) { return result; }

        var typeModel = new TypeModel(type, id);
        _types.Add(typeModel);

        typeModel.Apply(t =>
            typeModel.Init(
                genericTypeArguments: type.IsGenericType ? BuildGenericTypeArguments(t) : [],
                baseType: t.IsAssignableTo(typeof(Task)) ? GetOrCreateTypeModel(typeof(Task)) : default
            )
        );

        return typeModel;
    }

    ModelCollection<MethodModel> BuildMethods(Type type)
    {
        var methods = new Dictionary<string, MethodModel>();

        var constructorInfos = type.GetConstructors(_domainBuilderOptions.ConstuctorBindingFlags) ?? [];

        methods[".ctor"] = new(".ctor", constructorInfos.Select(BuildConstructorOverload).ToArray(), IsConstructor: true);

        var methodInfos = type.GetMethods(_domainBuilderOptions.MethodBindingFlags) ?? [];
        foreach (var group in methodInfos.GroupBy(m => m.Name))
        {
            methods[group.Key] = new(group.Key, group.Select(BuildMethodOverload).ToArray());
        }

        return new(methods.Values);
    }

    ModelCollection<TypeModel> BuildCustomAttributes(MemberInfo member) =>
        new(member.CustomAttributes.Select(attr => GetOrCreateTypeModel(attr.AttributeType)));

    ModelCollection<TypeModel> BuildGenericTypeArguments(Type type) =>
        new(type.GenericTypeArguments.Select(GetOrCreateTypeModel));

    ModelCollection<TypeModel> BuildInterfaces(Type type) =>
        new(type.GetInterfaces().Select(GetOrCreateTypeModel));

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

    bool IsPublic(PropertyInfo property) =>
        property.GetMethod?.IsPublic == true;

    bool IsVirtual(PropertyInfo property) =>
        property.GetMethod?.IsVirtual == true;
}
