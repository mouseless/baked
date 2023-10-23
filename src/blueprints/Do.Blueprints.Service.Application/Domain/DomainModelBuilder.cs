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
            var model = new TypeModel(
                Type: type,
                Name: type.Name,
                Interfaces: type.GetInterfaces()
            );

            model.Constructors = ExtractConstructorModels(model, type.GetConstructors());
            model.Dependencies = ExtractFieldModels(
                model,
                type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(f => f.IsInitOnly).ToArray()
            );
            model.Methods = ExtractMethodModels(
                model,
                type.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(m => !m.IsSpecialName).ToArray()
            );
            model.Properties = ExtractPropertyModels(model, type.GetProperties());

            result.Add(type, model);
        }

        return result;
    }

    static List<MethodModel>? ExtractConstructorModels(TypeModel target, ConstructorInfo[] constructors)
    {
        if (!constructors.Any()) return null;

        var result = new List<MethodModel>();

        foreach (var constructor in constructors)
        {
            var model = new MethodModel(
                Target: target,
                Name: constructor.Name,
                ReturnType: target.Type,
                IsPublic: constructor.IsPublic,
                IsConstructor: true
            );

            model.Parameters = ExtractParameterModels(model, constructor.GetParameters());

            result.Add(model);
        }

        return result;
    }

    static List<MethodModel>? ExtractMethodModels(TypeModel target, MethodInfo[] methods)
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
                GenericArguements: method.GetGenericArguments()
            );

            model.Parameters = ExtractParameterModels(model, method.GetParameters());

            result.Add(model);
        }

        return result;
    }

    static List<ParameterModel>? ExtractParameterModels(MethodModel owner, ParameterInfo[] parameters)
    {
        if (!parameters.Any()) return null;

        var result = new List<ParameterModel>();

        foreach (var parameter in parameters)
        {
            var model = new ParameterModel(
                Owner: owner,
                Name: parameter.Name ?? throw new ArgumentNullException("Parameter name should not be null. Method:" + owner.Name),
                Type: parameter.ParameterType
            );

            result.Add(model);
        }

        return result;
    }

    static List<PropertyModel>? ExtractPropertyModels(TypeModel owner, PropertyInfo[] propertyInfos)
    {
        if (!propertyInfos.Any()) return null;

        var result = new List<PropertyModel>();

        foreach (var property in propertyInfos)
        {
            var model = new PropertyModel(
                Owner: owner,
                Name: property.Name,
                Type: property.PropertyType
            );

            result.Add(model);
        }

        return result;
    }

    static List<FieldModel>? ExtractFieldModels(TypeModel owner, FieldInfo[] fields)
    {
        if (!fields.Any()) return null;

        var result = new List<FieldModel>();

        foreach (var field in fields)
        {
            var model = new FieldModel(
                Owner: owner,
                Name: field.Name,
                Type: field.FieldType
            );

            result.Add(model);
        }

        return result;
    }
}
