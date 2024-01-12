using Shouldly;
using System.Reflection;

namespace Do.Domain.Model;

public class DomainModel
{
    public ModelCollection<AssemblyModel> Assemblies { get; } = [];
    public ModelCollection<TypeModel> Types { get; } = [];

    internal void Init()
    {
        int counter = 0;
        var enumerator = Types.GetEnumerator();
        TypeModel? next;

        while (enumerator.MoveNext())
        {
            next = enumerator.Current;
            BuildTypeModel(next);
            counter++;
        }

        counter.ShouldBe(Types.Count);
    }

    void BuildTypeModel(TypeModel typeModel)
    {
        if (typeModel.Namespace.Contains("System")) { return; }

        typeModel.Apply(type =>
        {
            foreach (var genericArgument in type.GenericTypeArguments)
            {
                typeModel.GenericTypeArguments.Add(GetOrCreateTypeModel(genericArgument));
            }

            foreach (var attributeData in type.GetCustomAttributesData())
            {
                typeModel.CustomAttributes.Add(GetOrCreateTypeModel(attributeData.AttributeType));
            }

            var constructorInfos = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];
            foreach (var constructor in constructorInfos)
            {
                typeModel.Constructors.Add(new(constructor.Name, typeModel, ExtractParameters(constructor)));
            }

            var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];
            foreach (var property in propertyInfos)
            {
                typeModel.Properties.Add(new(property.Name, GetOrCreateTypeModel(property.PropertyType), property.CanRead));
            }

            var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];
            foreach (var method in methodInfos)
            {
                typeModel.Methods.Add(new(method.Name, GetOrCreateTypeModel(method.ReturnType), method.IsPublic, ExtractParameters(method), ExtractCustomAttributes(method)));
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

    List<ParameterModel> ExtractParameters(MethodBase method) =>
        method.GetParameters().Select(p => new ParameterModel(p.Name ?? string.Empty, GetOrCreateTypeModel(p.ParameterType), p.IsOptional, p.DefaultValue)).ToList();
    List<TypeModel> ExtractCustomAttributes(MemberInfo member) =>
        member.GetCustomAttributesData().Select(a => GetOrCreateTypeModel(a.AttributeType)).ToList();
}
