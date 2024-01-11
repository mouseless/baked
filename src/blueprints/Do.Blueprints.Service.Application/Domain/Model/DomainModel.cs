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

            foreach (var type in descriptor.Assembly.GetTypes())
            {
                model._types[type] = new(type);
            }
        }

        foreach (var descriptor in types)
        {
            model._types[descriptor.Type] = new(descriptor.Type);
        }

        BuildTypeModels(model);

        return model;
    }

    Dictionary<Type, TypeModel> _types = new Dictionary<Type, TypeModel>();

    public List<AssemblyModel> Assemblies { get; } = new();
    public List<TypeModel> Types => _types.Values.ToList();

    static void BuildTypeModels(DomainModel model)
    {
        foreach (var typeModel in model.Types)
        {
            typeModel.Apply(type =>
            {
                var constructorInfos = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];

                foreach (var constructor in constructorInfos)
                {
                    typeModel.Constructors.Add(
                        new(
                            typeModel,
                            constructor.GetParameters().Select(p => new ParameterModel(p.Name ?? string.Empty, model._types[p.ParameterType])).ToList()
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
                            model._types[property.PropertyType],
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
                            method.ReturnType == typeof(void) || method.ReturnType.IsGenericParameter ? null : model._types[method.ReturnType],
                            method.GetParameters().Select(p => new ParameterModel(
                                        p.Name ?? string.Empty,
                                        p.ParameterType == typeof(void) || p.ParameterType.IsGenericParameter ? null : model._types[p.ParameterType]
                                    )).ToList(),
                            method.IsPublic
                        )
                    );
                }
            });
        }
    }
}
