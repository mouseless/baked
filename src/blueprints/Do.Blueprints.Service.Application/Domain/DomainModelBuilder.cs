using Do.Domain.Model;
using System.Reflection;

namespace Do.Domain;

public class DomainModelBuilder
{
    public static DomainModelBuilder CreateBuilder(DomainDescriptor descriptor) =>
        new(descriptor);

    readonly DomainDescriptor _descriptor;

    DomainModelBuilder(DomainDescriptor descriptor)
    {
        _descriptor = descriptor;
    }

    public DomainModel Build() => new(AssemblyModels: GenerateAssemblyModels(_descriptor.AssemblyList));

    List<AssemblyModel> GenerateAssemblyModels(List<Assembly> assemblies)
    {
        var result = new List<AssemblyModel>();
        var types = new List<Type>();
        types.AddRange(_descriptor.IncludedTypes);

        foreach (var assembly in assemblies)
        {
            types.AddRange(assembly.GetTypes());
        }

        types = types.Distinct().ToList();

        var groups = types.GroupBy(
                keySelector: t => t.Assembly,
                elementSelector: t => t,
                resultSelector: (assembly, types) => new { Assembly = assembly, Types = types.ToList() }
            ).ToList();

        foreach (var group in groups)
        {
            result.Add(
                new(
                    group.Assembly,
                    GenerateTypeModelList(group.Types)
                )
            );
        }

        return result;
    }

    static List<TypeModel> GenerateTypeModelList(List<Type> types)
    {
        var result = new List<TypeModel>();

        foreach (var type in types)
        {
            result.Add(
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
        var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
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
        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
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
