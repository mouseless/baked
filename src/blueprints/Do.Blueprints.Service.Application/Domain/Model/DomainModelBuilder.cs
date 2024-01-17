using System.Reflection;

namespace Do.Domain.Model;

public class DomainModelBuilder(DomainBuilderOptions _domainBuilderOptions)
{
    readonly Dictionary<string, AssemblyModel> _assemlies = [];
    readonly Dictionary<string, TypeModel> _types = [];

    public DomainModel BuildFrom(IAssemblyCollection assemblyCollection, ITypeCollection typeCollection)
    {
        foreach (var assemblyDescriptor in assemblyCollection)
        {
            var model = new AssemblyModel(assemblyDescriptor.Assembly);
            _assemlies.Add(model.Id, model);
        }

        foreach (var typeDescriptor in typeCollection)
        {
            if (!_types.ContainsKey(TypeModel.GetId(typeDescriptor.Type)))
            {
                var model = new TypeModel(typeDescriptor.Type);
                _types.Add(model.Id, model);
                BuildTypeModel(model, typeDescriptor.Type);
            }
        }

        return new(
            Assemblies: new ModelCollection<AssemblyModel>([.. _assemlies.Values]),
            Types: new ModelCollection<TypeModel>([.. _types.Values])
        );
    }

    void BuildTypeModel(TypeModel typeModel, Type type)
    {
        if (_domainBuilderOptions.TypeIsBuiltConventions.All(f => f(type)))
        {
            typeModel.Init(
                genericTypeArguments: new(type.GenericTypeArguments.Select(GetOrCreateTypeModel)),
                customAttributes: new(BuildCustomAttributes(type)),
                properties: new(BuildProperties(type)),
                methods: new(BuildMethods(type))
            );
        }
    }

    TypeModel GetOrCreateTypeModel(Type type)
    {
        if (_types.TryGetValue(TypeModel.GetId(type), out var result))
        {
            return result;
        }

        var newTypeModel = new TypeModel(type);
        _types[newTypeModel.Id] = newTypeModel;

        BuildTypeModel(newTypeModel, type);

        return newTypeModel;
    }

    List<MethodModel> BuildMethods(Type type)
    {
        var result = new Dictionary<string, MethodModel>();
        var overloads = new Dictionary<string, List<OverloadModel>>();

        var constructorInfos = type.GetConstructors(_domainBuilderOptions.ConstuctorBindingFlags) ?? [];

        result[".ctor"] = new MethodModel(".ctor", false, new(constructorInfos.Select(BuildConstructorOverload).ToList()));

        var methodInfos = type.GetMethods(_domainBuilderOptions.MethodBindingFlags) ?? [];
        var groups = methodInfos.GroupBy(m => m.Name);
        foreach (var group in groups)
        {
            result[group.Key] = new MethodModel(group.Key, false, new(group.Select(BuildOverload).ToList()));
        }

        return [.. result.Values];
    }

    IEnumerable<ParameterModel> BuildParameters(MethodBase method) =>
        method.GetParameters()
            .Select(p => new ParameterModel(p.Name ?? string.Empty, GetOrCreateTypeModel(p.ParameterType), p.IsOptional, p.DefaultValue));

    IEnumerable<AttributeModel> BuildCustomAttributes(MemberInfo member) =>
        member.CustomAttributes
            .Select(attr =>
                new AttributeModel(
                    GetOrCreateTypeModel(attr.AttributeType),
                    new(attr.ConstructorArguments.Select(a => new ValueModel(GetOrCreateTypeModel(a.ArgumentType), a.Value)))
                )
            );

    IEnumerable<PropertyModel> BuildProperties(Type type) =>
        type.GetProperties(_domainBuilderOptions.PropertyBindingFlags).Select(BuildProperty);

    OverloadModel BuildConstructorOverload(ConstructorInfo method) =>
        new(method.IsPublic, method.IsFamily, method.IsVirtual, new(BuildParameters(method)), new(BuildCustomAttributes(method)));

    OverloadModel BuildOverload(MethodInfo method) =>
        new(method.IsPublic, method.IsFamily, method.IsVirtual, new(BuildParameters(method)), new(BuildCustomAttributes(method)), GetOrCreateTypeModel(method.ReturnType));

    PropertyModel BuildProperty(PropertyInfo property) =>
        new(property.Name, GetOrCreateTypeModel(property.PropertyType), IsPublic(property), IsVirtual(property));

    static bool IsPublic(PropertyInfo source) =>
        source.GetMethod?.IsPublic == true;

    static bool IsVirtual(PropertyInfo source) =>
        source.GetMethod?.IsVirtual == true;
}


