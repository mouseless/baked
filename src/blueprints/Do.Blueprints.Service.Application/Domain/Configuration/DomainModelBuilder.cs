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
                constructor: typeModel.IsBusinessType ? BuildConstructor(t, typeModel) : default,
                customAttributes: typeModel.IsBusinessType ? BuildCustomAttributes(t) : [],
                properties: typeModel.IsBusinessType ? BuildProperties(t, typeModel) : [],
                methods: typeModel.IsBusinessType ? BuildMethods(t, typeModel) : [],
                interfaces: typeModel.IsBusinessType ? BuildInterfaces(t) : [],
                baseType: (typeModel.IsBusinessType || t.BaseType == typeof(Task)) && t.BaseType is not null ? GetOrCreateTypeModel(t.BaseType) : default
            )
        );
    }

    MethodModel? BuildConstructor(Type type, TypeModel target)
    {
        var constructorInfos = type.GetConstructors(_domainBuilderOptions.ConstuctorBindingFlags) ?? [];

        if (!constructorInfos.Any()) { return null; }

        var ctor = new MethodModel(target, ".ctor", IsConstructor: true);
        ctor.Init(
            overloads: constructorInfos.Select(c => BuildConstructorOverload(c, ctor)).ToArray(),
            customAttributes: []
        );

        return ctor;
    }

    ModelCollection<MethodModel> BuildMethods(Type type, TypeModel target)
    {
        var methods = new Dictionary<string, MethodModel>();
        var methodInfos = type.GetMethods(_domainBuilderOptions.MethodBindingFlags) ?? [];
        foreach (var group in methodInfos.GroupBy(m => m.Name))
        {
            var method = methods[group.Key] = new(target, group.Key);
            method.Init(
                overloads: group.Select(m => BuildMethodOverload(m, method)).ToArray(),
                customAttributes: []
            );
        }

        return new(methods.Values);
    }

    AttributeCollection BuildCustomAttributes(MemberInfo member) =>
        new(member.GetCustomAttributes());

    ModelCollection<TypeModel> BuildGenericTypeArguments(Type type) =>
        new(type.GenericTypeArguments.Select(GetOrCreateTypeModel));

    ModelCollection<TypeModel> BuildInterfaces(Type type) =>
        new(type.GetInterfaces().Select(GetOrCreateTypeModel));

    OverloadModel BuildConstructorOverload(ConstructorInfo constructor, MethodModel parent)
    {
        var overload = new OverloadModel(parent, constructor.IsPublic, constructor.IsFamily, constructor.IsVirtual);
        overload.Init(
            parameters: BuildParameters(constructor, overload),
            customAttributes: []
        );

        return overload;
    }

    OverloadModel BuildMethodOverload(MethodInfo method, MethodModel parent)
    {
        var overload = new OverloadModel(parent, method.IsPublic, method.IsFamily, method.IsVirtual, GetOrCreateTypeModel(method.ReturnType));
        overload.Init(
            parameters: BuildParameters(method, overload),
            customAttributes: []
        );

        return overload;
    }

    ModelCollection<ParameterModel> BuildParameters(MethodBase method, OverloadModel overload) =>
        new(method.GetParameters().Select(p => BuildParameter(p, overload)));

    ParameterModel BuildParameter(ParameterInfo parameter, OverloadModel overload) =>
        new(overload, parameter.Name ?? string.Empty, GetOrCreateTypeModel(parameter.ParameterType), parameter.IsOptional, parameter.DefaultValue, BuildCustomAttributes(parameter.Member));

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
