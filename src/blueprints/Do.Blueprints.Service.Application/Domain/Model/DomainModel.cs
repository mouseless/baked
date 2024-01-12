using System.Reflection;

namespace Do.Domain.Model;

public class DomainModel
{
    public ModelCollection<AssemblyModel> Assemblies { get; } = [];
    public ModelCollection<TypeModel> Types { get; } = [];

    internal TypeModel AddTypeModel(Type type)
    {
        var typeModel = Types.Add(new(type));

        if (typeModel.Namespace.Contains("System")) { return typeModel; }

        typeModel.Apply(type =>
        {
            var genericArguements = type.GetGenericArguments();
            foreach (var genericArgument in genericArguements)
            {
                typeModel.GenericArguements.Add(GetOrAddTypeModel(genericArgument));
            }

            var constructorInfos = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];

            foreach (var constructor in constructorInfos)
            {
                var parameters = constructor.GetParameters().Select(p => new ParameterModel(
                                    p.Name ?? string.Empty,
                                    GetOrAddTypeModel(p.ParameterType)
                                )).ToList();

                typeModel.Constructors.Add(new(constructor.Name, typeModel, parameters));
            }

            var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];

            foreach (var property in propertyInfos)
            {
                typeModel.Properties.Add(
                    new(property.Name, GetOrAddTypeModel(property.PropertyType), property.CanRead)
                );
            }

            var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];

            foreach (var method in methodInfos)
            {
                var parameters = method.GetParameters().Select(p => new ParameterModel(
                                    p.Name ?? string.Empty,
                                    GetOrAddTypeModel(p.ParameterType)
                                )).ToList();

                typeModel.Methods.Add(new(method.Name, GetOrAddTypeModel(method.ReturnType), method.IsPublic, parameters));
            }
        });

        return typeModel;
    }

    TypeModel GetOrAddTypeModel(Type type)
    {
        if (Types.TryGetValue(type.Name, out var result))
        {
            return result;
        }

        return AddTypeModel(type);
    }
}
