using NHibernate.Util;
using System.Reflection;

using static Do.Domain.DomainModel;

namespace Do.Domain;

public class DomainModelBuilder
{
    public static DomainModelBuilder CreateBuilder(DomainDescriptor descriptor) =>
        new(descriptor);

    readonly DomainDescriptor _descriptor;

    List<Type> _types = new();

    DomainModelBuilder(DomainDescriptor descriptor)
    {
        _descriptor = descriptor;
    }

    public DomainModel Build()
    {
        _descriptor.AssemblyList.ForEach(a => a.GetExportedTypes().ToList().ForEach(t => _types.Add(t)));
        _descriptor.IncludedTypes.ForEach(t => _types.Add(t));

        return new(
            TypeModels: GenerateTypeModelList()
        );
    }

    Dictionary<Type, TypeModel> GenerateTypeModelList()
    {
        var result = new Dictionary<Type, TypeModel>();

        foreach (var type in _types)
        {
            result.Add(
                type,
                new(
                    Type: type,
                    Name: type.Name,
                    IsValueType: type.IsValueType,
                    Interfaces: type.GetInterfaces(),
                    Constructors: (model) => GenerateConstructorModels(model, type.GetConstructors()),
                    Dependencies: (model) => GenerateFieldModels(
                        model,
                        type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).Where(f => f.IsInitOnly && f.CustomAttributes.Count() == 0).ToArray()
                    ),
                    Methods: (model) => GenerateMethodModels(
                        model,
                        type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(m => !m.IsSpecialName).ToArray()
                    ),
                    Properties: (model) => GeneratePropertyModels(model, type.GetProperties())
                )
            );
        }

        return result;
    }

    static List<MethodModel>? GenerateConstructorModels(TypeModel target, ConstructorInfo[] constructors)
    {
        if (!constructors.Any()) return null;

        var result = new List<MethodModel>();

        foreach (var constructor in constructors)
        {
            result.Add(
                new(
                    Target: target,
                    Name: constructor.Name,
                    ReturnType: target.Type,
                    IsPublic: constructor.IsPublic,
                    IsConstructor: true,
                    ParametersFactory: (model) => GenerateParameterModels(model, constructor.GetParameters())
                )
            );
        }

        return result;
    }

    static List<MethodModel>? GenerateMethodModels(TypeModel target, MethodInfo[] methods)
    {
        if (!methods.Any()) return null;

        var result = new List<MethodModel>();

        foreach (var method in methods)
        {
            var model = new MethodModel(
                Target: target,
                Name: method.Name,
                ReturnType: method.ReturnType,
                IsPublic: method.IsPublic,
                IsConstructor: method.IsConstructor,
                GenericArguements: method.GetGenericArguments(),
                ParametersFactory: (model) => GenerateParameterModels(model, method.GetParameters())
            );

            result.Add(model);
        }

        return result;
    }

    static List<ParameterModel>? GenerateParameterModels(MethodModel owner, ParameterInfo[] parameters)
    {
        if (!parameters.Any()) return null;

        var result = new List<ParameterModel>();

        foreach (var parameter in parameters)
        {
            result.Add(
                new(
                    Owner: owner,
                    Name: parameter.Name ?? throw new ArgumentNullException("Parameter name should not be null. Method:" + owner.Name),
                    Type: parameter.ParameterType
                )
            );
        }

        return result;
    }

    static List<PropertyModel>? GeneratePropertyModels(TypeModel owner, PropertyInfo[] propertyInfos)
    {
        if (!propertyInfos.Any()) return null;

        var result = new List<PropertyModel>();

        foreach (var property in propertyInfos)
        {
            result.Add(
                new(
                    Owner: owner,
                    Name: property.Name,
                    Type: property.PropertyType
                )
            );
        }

        return result;
    }

    static List<FieldModel>? GenerateFieldModels(TypeModel owner, FieldInfo[] fields)
    {
        if (!fields.Any()) return null;

        var result = new List<FieldModel>();

        foreach (var field in fields)
        {
            result.Add(
                new(
                    Owner: owner,
                    Name: field.Name,
                    Type: field.FieldType
                )
            );
        }

        return result;
    }
}
