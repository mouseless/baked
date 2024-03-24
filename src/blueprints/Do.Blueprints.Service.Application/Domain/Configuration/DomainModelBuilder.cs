using Do.Domain.Model;
using System.Reflection;

namespace Do.Domain.Configuration;

internal class DomainModelBuilder : IDomainService, ITypeModelFactory
{
    static IDomainService IDomainService.New(DomainServiceProvider sp) =>
        new DomainModelBuilder(sp.Get<DomainBuilderOptions>(), sp.Get<ModelConfigurer>(), sp.Get<ModelIndexer>());

    readonly DomainBuilderOptions _domainBuilderOptions;
    readonly ModelConfigurer _configurer;
    readonly ModelIndexer _indexer;
    readonly KeyedModelCollection<AssemblyModel> _assemblies = [];
    readonly KeyedModelCollection<TypeModel> _types = [];

    DomainModelBuilder(DomainBuilderOptions domainBuilderOptions, ModelConfigurer configurer, ModelIndexer indexer)
    {
        _domainBuilderOptions = domainBuilderOptions;
        _configurer = configurer;
        _indexer = indexer;
    }

    internal DomainModel BuildFrom(IDomainAssemblyCollection domainAssemblies, IDomainTypeCollection domainTypes)
    {
        foreach (var assembly in domainAssemblies)
        {
            _assemblies.Add(new(assembly));

            foreach (var type in assembly.GetExportedTypes())
            {
                domainTypes.Add(type);
            }
        }

        foreach (var type in domainTypes)
        {
            _types.Add(new(type, TypeModel.IdFrom(type), assembly: _assemblies.GetOrDefault(type.Assembly.FullName), isBusinessType: true));
        }

        foreach (var type in _types.ToList())
        {
            InitTypeModel(type);
        }

        var model = new DomainModel(new(_assemblies), new(_types));

        _configurer.Execute(model);
        _indexer.Execute(model);

        return model;
    }

    TypeModel GetOrCreateTypeModel(Type type)
    {
        var id = TypeModel.IdFrom(type);
        if (_types.TryGetValue(id, out var result)) { return result; }

        var isGenericBusinessType = type.IsGenericType && !type.IsGenericTypeDefinition && _assemblies.Contains(type.Assembly?.FullName ?? string.Empty);
        var typeModel = new TypeModel(type, id, isBusinessType: isGenericBusinessType);

        _types.Add(typeModel);

        InitTypeModel(typeModel);

        return typeModel;
    }

    void InitTypeModel(TypeModel typeModel)
    {
        typeModel.Apply(t =>
            typeModel.Init(
                genericTypeArguments: typeModel.IsBusinessType || typeModel.IsGenericType ? BuildGenericTypeArguments(t) : [],
                customAttributes: typeModel.IsBusinessType ? BuildCustomAttributes(t) : [],
                properties: typeModel.IsBusinessType ? BuildProperties(t, typeModel) : [],
                methods: typeModel.IsBusinessType ? BuildMethods(t, typeModel) : [],
                interfaces: typeModel.IsBusinessType ? BuildInterfaces(t) : [],
                baseType: (typeModel.IsBusinessType || t.BaseType == typeof(Task)) && t.BaseType is not null ? GetOrCreateTypeModel(t.BaseType) : default
            )
        );
    }

    ModelCollection<MethodModel> BuildMethods(Type type, TypeModel target)
    {
        var methods = new Dictionary<string, MethodModel>();
        var constructorInfos = type.GetConstructors(_domainBuilderOptions.ConstuctorBindingFlags) ?? [];

        methods[".ctor"] = new(target, ".ctor", constructorInfos.Select(BuildConstructorOverload).ToArray(), [], IsConstructor: true);

        var methodInfos = type.GetMethods(_domainBuilderOptions.MethodBindingFlags) ?? [];
        foreach (var group in methodInfos.GroupBy(m => m.Name))
        {
            methods[group.Key] = new(target, group.Key, group.Select(BuildMethodOverload).ToArray(), []);
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
        new(method.GetParameters().Select(p => new ParameterModel(p.Name ?? string.Empty, GetOrCreateTypeModel(p.ParameterType), p.IsOptional, p.DefaultValue, BuildCustomAttributes(p.Member))));

    ModelCollection<PropertyModel> BuildProperties(Type type, TypeModel owner) =>
        new(type.GetProperties(_domainBuilderOptions.PropertyBindingFlags).Select(p => BuildProperty(p, owner)));

    PropertyModel BuildProperty(PropertyInfo property, TypeModel owner) =>
        new(owner, property.Name, GetOrCreateTypeModel(property.PropertyType), IsPublic(property), IsVirtual(property), BuildCustomAttributes(property));

    bool IsPublic(PropertyInfo property) =>
        property.GetMethod?.IsPublic == true;

    bool IsVirtual(PropertyInfo property) =>
        property.GetMethod?.IsVirtual == true;

    TypeModel ITypeModelFactory.Create(Type type) => GetOrCreateTypeModel(type);
}
