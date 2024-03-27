using Do.Domain.Model;
using System.Reflection;

namespace Do.Domain.Configuration;

internal class DomainModelBuilder(DomainBuilderOptions _options)
{
    readonly ModelKeyedCollection<TypeModel> _reflectedTypes = [];
    readonly ModelKeyedCollection<TypeModel> _referencedTypes = [];

    internal DomainModel BuildFrom(IDomainTypeCollection reflectedTypes)
    {
        foreach (var type in reflectedTypes)
        {
            _reflectedTypes.Add(new(type, TypeModel.IdFrom(type)));
        }

        foreach (var typeModel in _reflectedTypes)
        {
            typeModel.Apply(t =>
            {
                typeModel.SetGenerics(
                    genericTypeArguments: typeModel.IsGenericType ? BuildGenericTypeArguments(t) : [],
                    genericTypeDefinition: typeModel.IsGenericType ? BuildGenericTypeDefinition(t) : default
                );
                typeModel.SetInheritance(
                    baseType: BuildBaseType(t),
                    interfaces: BuildInterfaces(t)
                );
                typeModel.SetMetadata(
                    customAttributes: BuildCustomAttributes(t)
                );
                typeModel.SetMembers(
                    constructor: BuildConstructorGroup(t, typeModel),
                    properties: BuildProperties(t, typeModel),
                    methodGroups: BuildMethodGroups(t, typeModel)
                );
            });
        }

        return new(new(_reflectedTypes), new(_referencedTypes));
    }

    TypeModel GetOrCreateTypeModel(Type type)
    {
        var id = TypeModel.IdFrom(type);
        if (_reflectedTypes.TryGetValue(id, out var result)) { return result; }
        if (_referencedTypes.TryGetValue(id, out result)) { return result; }

        var typeModel = new TypeModel(type, id);

        _referencedTypes.Add(typeModel);

        typeModel.Apply(t =>
        {
            if (_options.ReferencedType.ShouldSkipSetGenerics.Any(f => f(t))) { return; }

            typeModel.SetGenerics(
                genericTypeArguments: typeModel.IsGenericType ? BuildGenericTypeArguments(t) : [],
                genericTypeDefinition: typeModel.IsGenericType ? BuildGenericTypeDefinition(t) : default
            );
        });

        typeModel.Apply(t =>
        {
            if (_options.ReferencedType.ShouldSkipSetInheritance.Any(f => f(t))) { return; }

            typeModel.SetInheritance(
                baseType: BuildBaseType(t),
                interfaces: BuildInterfaces(t)
            );
        });

        return typeModel;
    }

    ConstructorGroupModel? BuildConstructorGroup(Type type, TypeModel typeModel)
    {
        var constructorInfos = type.GetConstructors(_options.ReflectedType.ConstructorBindingFlags) ?? [];
        if (!constructorInfos.Any()) { return null; }

        var ctorGroup = new ConstructorGroupModel(typeModel);
        ctorGroup.Init([.. constructorInfos.Select(c => BuildConstructor(c, ctorGroup))]);

        return ctorGroup;
    }

    ModelCollection<MethodGroupModel> BuildMethodGroups(Type type, TypeModel typeModel)
    {
        var methods = new Dictionary<string, MethodGroupModel>();
        var methodInfos = type.GetMethods(_options.ReflectedType.MethodBindingFlags) ?? [];
        foreach (var group in methodInfos.GroupBy(m => m.Name))
        {
            var method = methods[group.Key] = new(typeModel, group.Key);
            // reflected type parent
            method.Init(methods: [.. group.Select(m => BuildMethod(m, method))]);
        }

        return new(methods.Values);
    }

    TypeModel? BuildBaseType(Type type) =>
        type.BaseType is null ? default : GetOrCreateTypeModel(type.BaseType);

    AttributeCollection BuildCustomAttributes(MemberInfo member) =>
        new(member.GetCustomAttributes());

    ModelCollection<TypeModel> BuildGenericTypeArguments(Type type) =>
        new(type.GenericTypeArguments.Select(GetOrCreateTypeModel));

    TypeModel? BuildGenericTypeDefinition(Type type) =>
         type.IsGenericTypeDefinition ? default : GetOrCreateTypeModel(type.GetGenericTypeDefinition());

    ModelCollection<TypeModel> BuildInterfaces(Type type) =>
        new(type.GetInterfaces().Select(GetOrCreateTypeModel));

    ConstructorModel BuildConstructor(ConstructorInfo constructorInfo, ConstructorGroupModel ctorGroup)
    {
        var ctor = new ConstructorModel(ctorGroup, constructorInfo.IsPublic, constructorInfo.IsFamily);
        ctor.Init(
            parameters: BuildParameters(constructorInfo, ctor),
            customAttributes: BuildCustomAttributes(constructorInfo)
        );

        return ctor;
    }

    MethodModel BuildMethod(MethodInfo methodInfo, MethodGroupModel group)
    {
        var method = new MethodModel(group, methodInfo.IsPublic, methodInfo.IsFamily, methodInfo.IsVirtual, GetOrCreateTypeModel(methodInfo.ReturnType));
        method.Init(
            parameters: BuildParameters(methodInfo, method),
            customAttributes: BuildCustomAttributes(methodInfo)
        );

        return method;
    }

    ModelCollection<ParameterModel> BuildParameters(MethodBase method, MethodBaseModel methodbase) =>
        new(method.GetParameters().Select(p => BuildParameter(p, methodbase)));

    ParameterModel BuildParameter(ParameterInfo parameter, MethodBaseModel overload) =>
        new(overload, parameter.Name ?? string.Empty, GetOrCreateTypeModel(parameter.ParameterType), parameter.IsOptional, parameter.DefaultValue, BuildCustomAttributes(parameter.Member));

    ModelCollection<PropertyModel> BuildProperties(Type type, TypeModel owner) =>
        new(type.GetProperties(_options.ReflectedType.PropertyBindingFlags).Select(p => BuildProperty(p, owner)));

    PropertyModel BuildProperty(PropertyInfo property, TypeModel owner) =>
        new(owner, property.Name, GetOrCreateTypeModel(property.PropertyType), IsPublic(property), IsVirtual(property), BuildCustomAttributes(property));

    bool IsPublic(PropertyInfo property) =>
        property.GetMethod?.IsPublic == true;

    bool IsVirtual(PropertyInfo property) =>
        property.GetMethod?.IsVirtual == true;
}
