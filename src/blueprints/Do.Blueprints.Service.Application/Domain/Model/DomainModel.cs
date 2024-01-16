using System.Reflection;

namespace Do.Domain.Model;

public class DomainModel(DomainOptions _domainOptions)
{
    public ModelCollection<AssemblyModel> Assemblies { get; } = [];
    public ModelCollection<TypeModel> Types { get; } = [];

    internal void Init()
    {
        var enumerator = Types.GetEnumerator();
        TypeModel? next;

        while (enumerator.MoveNext())
        {
            next = enumerator.Current;
            if (_domainOptions.TypeIsBuiltConventions.Any(f => f(next)))
            {
                BuildTypeModel(next);
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
                typeModel.Constructors.Add(new(constructor.Name, typeModel, Parameters(constructor), constructor.IsPublic));
            }

            var methodInfos = type.GetMethods(_domainOptions.MethodBindingFlags) ?? [];
            foreach (var method in methodInfos)
            {
                if (typeModel.Methods.TryGetValue(method.Name, out var methodModel))
                {
                    methodModel.Overloads.Add(new(Parameters(method), CustomAttributes(method)));
                }
                else
                {
                    typeModel.Methods.Add(new(method.Name, GetOrCreateTypeModel(method.ReturnType), method.IsPublic, method.IsFamily, method.IsVirtual, [new(Parameters(method), CustomAttributes(method))]));
                }
            }

            var propertyInfos = type.GetProperties(_domainOptions.PropertyBindingFlags) ?? [];
            foreach (var property in propertyInfos)
            {
                typeModel.Properties.Add(new(property.Name, GetOrCreateTypeModel(property.PropertyType), IsPublic(property), IsVirtual(property)));
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
        method.GetParameters().Select(p => new ParameterModel(p.Name ?? string.Empty, GetOrCreateTypeModel(p.ParameterType), p.IsOptional, p.DefaultValue)).ToList();

    List<TypeModel> CustomAttributes(MemberInfo member) =>
        member.GetCustomAttributesData().Select(a => GetOrCreateTypeModel(a.AttributeType)).ToList();

    static bool IsPublic(PropertyInfo source) => source.GetMethod?.IsPublic == true;
    static bool IsVirtual(PropertyInfo source) => source.GetMethod?.IsVirtual == true;
}
