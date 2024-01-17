using System.Reflection;

namespace Do.Domain.Model;

public class DomainModel(DomainOptions _domainOptions)
{
    public ModelCollection<AssemblyModel> Assemblies { get; } = [];
    public ModelCollection<TypeModel> Types { get; } = [];

    internal void Init()
    {
        foreach (var type in Types)
        {
            if (_domainOptions.TypeIsBuiltConventions.All(f => f(type)))
            {
                BuildTypeModel(type);
            }
        }
    }

    void BuildTypeModel(TypeModel typeModel)
    {
        typeModel.Apply(type =>
        {
            foreach (var genericArgument in type.GenericTypeArguments)
            {
                typeModel.GenericTypeArguments.Add(GetOrCreateTypeModel(genericArgument));
            }

            typeModel.CustomAttributes.AddRange(CustomAttributes(type));

            var constructorInfos = type.GetConstructors(_domainOptions.ConstuctorBindingFlags) ?? [];
            foreach (var constructor in constructorInfos)
            {
                if (typeModel.Methods.TryGetValue(constructor.Name, out var methodModel))
                {
                    methodModel.Overloads.Add(Overload(constructor));
                }
                else
                {
                    typeModel.Methods.Add(new(constructor.Name, constructor.IsConstructor, [Overload(constructor)]));
                }
            }

            var methodInfos = type.GetMethods(_domainOptions.MethodBindingFlags) ?? [];
            foreach (var method in methodInfos)
            {
                if (typeModel.Methods.TryGetValue(method.Name, out var methodModel))
                {
                    methodModel.Overloads.Add(Overload(method));
                }
                else
                {
                    typeModel.Methods.Add(new(method.Name, method.IsConstructor, [Overload(method)]));
                }
            }

            var propertyInfos = type.GetProperties(_domainOptions.PropertyBindingFlags) ?? [];
            foreach (var property in propertyInfos)
            {
                typeModel.Properties.Add(Property(property));
            }
        });
    }

    TypeModel GetOrCreateTypeModel(Type type)
    {
        if (Types.TryGetValue(TypeModel.IdFromType(type), out var result))
        {
            return result;
        }

        result = new TypeModel(type);
        Types.Add(result);

        return result;
    }

    List<ParameterModel> Parameters(MethodBase method) =>
        method.GetParameters()
            .Select(p =>
                new ParameterModel(p.Name ?? string.Empty, GetOrCreateTypeModel(p.ParameterType), p.IsOptional, p.DefaultValue)
            ).ToList();

    List<AttributeModel> CustomAttributes(MemberInfo member) =>
        member.CustomAttributes
            .Select(attr =>
                new AttributeModel(
                    GetOrCreateTypeModel(attr.AttributeType),
                    attr.ConstructorArguments.Select(a => new ValueModel(GetOrCreateTypeModel(a.ArgumentType), a.Value)).ToList()
                )
            ).ToList();

    OverloadModel Overload(ConstructorInfo method) =>
        new(method.IsPublic, method.IsFamily, method.IsVirtual, Parameters(method), CustomAttributes(method));
    OverloadModel Overload(MethodInfo method) =>
        new(method.IsPublic, method.IsFamily, method.IsVirtual, Parameters(method), CustomAttributes(method), GetOrCreateTypeModel(method.ReturnType));

    PropertyModel Property(PropertyInfo property) =>
        new(property.Name, GetOrCreateTypeModel(property.PropertyType), IsPublic(property), IsVirtual(property));

    static bool IsPublic(PropertyInfo source) =>
        source.GetMethod?.IsPublic == true;

    static bool IsVirtual(PropertyInfo source) =>
        source.GetMethod?.IsVirtual == true;
}
