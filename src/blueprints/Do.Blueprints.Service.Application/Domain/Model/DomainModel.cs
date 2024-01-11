using System.Reflection;

namespace Do.Domain.Model;

public class DomainModel
{
    public static DomainModel Build(IAssemblyCollection assemblies, ITypeCollection types)
    {
        var model = new DomainModel();

        foreach (var descriptor in assemblies)
        {
            model.Assemblies.Add(new(descriptor.Assembly));

            foreach (var type in descriptor.Assembly.GetExportedTypes())
            {
                types.Add(type);
            }
        }

        foreach (var descriptor in types)
        {
            if (model._types.ContainsKey(descriptor.Type)) { continue; }

            model.CreateTypeModel(descriptor.Type);
        }

        return model;
    }

    readonly Dictionary<Type, TypeModel> _types = [];

    public List<AssemblyModel> Assemblies { get; } = [];
    public List<TypeModel> Types => _types.Values.ToList();

    TypeModel GetOrCreateTypeModel(Type type)
    {
        if (_types.TryGetValue(type, out var result))
        {
            return result;
        }

        return CreateTypeModel(type);
    }

    TypeModel CreateTypeModel(Type type)
    {
        var typeModel = _types[type] = new(type);

        if (typeModel.Namespace.Contains("System")) { return typeModel; }

        typeModel.Apply(type =>
        {
            var constructorInfos = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];

            foreach (var constructor in constructorInfos)
            {
                typeModel.Constructors.Add(
                    new(
                        typeModel,
                        constructor.GetParameters().Select(p => new ParameterModel(p.Name ?? string.Empty, GetOrCreateTypeModel(p.ParameterType))).ToList()
                    )
                );
            }
        });

        typeModel.Apply(type =>
        {
            var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];

            foreach (var property in propertyInfos)
            {
                typeModel.Properties.Add(
                    new(
                        property.Name,
                        GetOrCreateTypeModel(property.PropertyType),
                        property.CanRead
                    )
                );
            }
        });

        typeModel.Apply(type =>
        {
            var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];

            foreach (var method in methodInfos)
            {
                typeModel.Methods.Add(
                    new(
                        method.Name,
                        GetOrCreateTypeModel(method.ReturnType),
                        method.GetParameters().Select(p => new ParameterModel(
                                    p.Name ?? string.Empty,
                                    GetOrCreateTypeModel(p.ParameterType)
                                )).ToList(),
                        method.IsPublic
                    )
                );
            }
        });

        return typeModel;
    }
}
