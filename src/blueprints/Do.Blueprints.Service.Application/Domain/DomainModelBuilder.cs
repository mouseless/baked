using Do.Domain.Model;
using System.Reflection;

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
                    Constructors: ExtractConstructorModels(type),
                    Methods: ExtractMethodModels(type)
                )
            );
        }

        return result;
    }

    static List<MethodModel>? ExtractConstructorModels(Type type)
    {
        var constructors = type.GetConstructors();
        if (!constructors.Any()) { return null; }

        var result = new List<MethodModel>();

        foreach (var constructor in constructors)
        {
            result.Add(
                new(
                    Name: constructor.Name,
                    ReturnType: type,
                    IsPublic: constructor.IsPublic,
                    IsConstructor: true,
                    Parameters: ExtractParameterModels(constructor)
                )
            );
        }

        return result;
    }

    static List<MethodModel>? ExtractMethodModels(Type type)
    {
        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        if (!methods.Any()) return null;

        var result = new List<MethodModel>();

        foreach (var method in methods)
        {
            var model = new MethodModel(
                Name: method.Name,
                ReturnType: method.ReturnType,
                IsPublic: method.IsPublic,
                IsConstructor: method.IsConstructor,
                GenericArguements: method.GetGenericArguments(),
                Parameters: ExtractParameterModels(method)
            );

            result.Add(model);
        }

        return result;
    }

    static List<ParameterModel>? ExtractParameterModels(ConstructorInfo constructor) => ExtractParameterModelsInner(constructor);

    static List<ParameterModel>? ExtractParameterModels(MethodInfo method) => ExtractParameterModelsInner(method);

    static List<ParameterModel>? ExtractParameterModelsInner(MethodBase method)
    {
        var parameters = method.GetParameters();
        if (!parameters.Any()) { return null; }

        var result = new List<ParameterModel>();

        foreach (var parameter in parameters)
        {
            result.Add(
                new(
                    Name: parameter.Name ?? throw new ArgumentNullException("Parameter name should not be null. Method:" + method.Name),
                    Type: parameter.ParameterType
                )
            );
        }

        return result;
    }
}
