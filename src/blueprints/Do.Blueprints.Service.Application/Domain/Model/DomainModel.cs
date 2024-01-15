using FluentNHibernate;
using FluentNHibernate.Diagnostics;
using System.Reflection;

namespace Do.Domain.Model;

public class DomainModel(DomainOptions _domainOptions)
    : ITypeSource
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
            typeModel.Apply(type =>
            {
                foreach (var genericArgument in type.GenericTypeArguments)
                {
                    typeModel.GenericTypeArguments.Add(GetOrCreateTypeModel(genericArgument));
                }
            });

            typeModel.CustomAttributes.AddRange(CustomAttributes(type));

            var constructorInfos = type.GetConstructors(_domainOptions.ConstuctorBindingFlags) ?? [];
            foreach (var constructor in constructorInfos)
            {
                typeModel.Constructors.Add(new(constructor.Name, typeModel, Parameters(constructor), constructor.IsPublic));
            }

            var methodInfos = type.GetMethods(_domainOptions.MethodBindingFlags) ?? [];
            foreach (var method in methodInfos)
            {
                typeModel.Methods.Add(
                    new(method.Name, GetOrCreateTypeModel(method.ReturnType), method.IsPublic, method.IsFamily, method.IsVirtual, Parameters(method), CustomAttributes(method))
                );
            }

            var propertyInfos = type.GetProperties(_domainOptions.PropertyBindingFlags) ?? [];
            foreach (var property in propertyInfos)
            {
                typeModel.Properties.Add(new(property.Name, GetOrCreateTypeModel(property.PropertyType), property.IsPublic(), property.IsVirtual()));
            }
        });
    }

    TypeModel GetOrCreateTypeModel(Type type)
    {
        if (Types.TryGetValue(IModel.IdFromType(type), out var result))
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

    IEnumerable<Type> ITypeSource.GetTypes()
    {
        var result = new List<Type>();
        foreach (var typeModel in Types)
        {
            typeModel.Apply(result.Add);
        }

        return result;
    }

    void ITypeSource.LogSource(IDiagnosticLogger logger) { }

    string ITypeSource.GetIdentifier() => nameof(DomainModel);
}
