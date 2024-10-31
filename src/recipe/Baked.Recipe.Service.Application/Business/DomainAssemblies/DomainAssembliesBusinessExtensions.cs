using Baked.Business;
using Baked.Business.DomainAssemblies;
using Baked.Domain.Model;
using Baked.RestApi.Model;
using System.Reflection;

namespace Baked;

public static class DomainAssembliesBusinessExtensions
{
    public static DomainAssembliesBusinessFeature DomainAssemblies(this BusinessConfigurator configurator, List<Assembly> assemblies,
        Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel>? defaultOverloadSelector = default,
        bool addEmbeddedFileProviders = true,
        string? baseNamespace = default
    ) => configurator.DomainAssemblies(assemblies.Select(a => (a, baseNamespace)),
        defaultOverloadSelector: defaultOverloadSelector,
        addEmbeddedFileProviders: addEmbeddedFileProviders
    );

    public static DomainAssembliesBusinessFeature DomainAssemblies(this BusinessConfigurator _, IEnumerable<(Assembly, string?)> assemblyDescriptors,
        Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel>? defaultOverloadSelector = default,
        bool addEmbeddedFileProviders = true
    ) => new(
        assemblyDescriptors,
        defaultOverloadSelector ?? (overloads =>
            overloads.FirstPublicInstanceWithMostParametersOrDefault() ??
            overloads.FirstNonPublicInstanceWithMostParametersOrDefault() ??
            overloads.FirstPublicStaticWithMostParametersOrDefault() ??
            overloads.FirstNonPublicStaticWithMostParametersOrDefault() ??
            overloads.FirstWithMostParametersOrDefault() ??
            throw new($"Method without an overload should not exist")
        ),
        addEmbeddedFileProviders
    );

    public static void AddAction(this ControllerModel controller, TypeModel type, MethodModel method) =>
        controller.Action.Add(
            method.Name,
            new(
                Id: method.Name,
                Method: HttpMethod.Post,
                RouteParts: [type.Name, method.Name],
                Return: new(method.DefaultOverload.ReturnType),
                FindTargetStatement: "target",
                MappedMethod: method
            )
            {
                Parameters = [
                    new(type, ParameterModelFrom.Services, "target"),
                    .. method.DefaultOverload.Parameters.Select(p =>
                        new RestApi.Model.ParameterModel(p.ParameterType, ParameterModelFrom.BodyOrForm, p.Name, MappedParameter: p)
                        {
                            IsOptional = p.IsOptional,
                            DefaultValue = p.DefaultValue,
                            DefaultValueRenderer = defaultValue => $"{defaultValue}"
                        }
                    )
                ]
            }
        );

    public static bool IsTarget(this RestApi.Model.ParameterModel parameter) =>
        parameter.Id == "target";

    public static MethodOverloadModel? FirstPublicInstanceWithMostParametersOrDefault(this IEnumerable<MethodOverloadModel> overloads) =>
        overloads
            .Where(o => o.IsPublic && !o.IsStatic)
            .OrderByDescending(o => o.Parameters.Count)
            .FirstOrDefault();

    public static MethodOverloadModel? FirstNonPublicInstanceWithMostParametersOrDefault(this IEnumerable<MethodOverloadModel> overloads) =>
        overloads
            .Where(o => !o.IsPublic && !o.IsStatic)
            .OrderByDescending(o => o.Parameters.Count)
            .FirstOrDefault();

    public static MethodOverloadModel? FirstPublicStaticWithMostParametersOrDefault(this IEnumerable<MethodOverloadModel> overloads) =>
        overloads
            .Where(o => o.IsPublic && o.IsStatic)
            .OrderByDescending(o => o.Parameters.Count)
            .FirstOrDefault();

    public static MethodOverloadModel? FirstNonPublicStaticWithMostParametersOrDefault(this IEnumerable<MethodOverloadModel> overloads) =>
        overloads
            .Where(o => !o.IsPublic && o.IsStatic)
            .OrderByDescending(o => o.Parameters.Count)
            .FirstOrDefault();

    public static MethodOverloadModel? FirstWithMostParametersOrDefault(this IEnumerable<MethodOverloadModel> overloads) =>
        overloads
            .OrderByDescending(o => o.Parameters.Count)
            .FirstOrDefault();

    public static bool IsPublicInstanceWithNoSpecialName(this MethodOverloadModel overload) =>
        overload.IsPublic && !overload.IsStatic && !overload.IsSpecialName;

    public static bool AllParametersAreApiInput(this MethodOverloadModel overload) =>
        overload.Parameters.All(p => p.ParameterType.TryGetMetadata(out var metadata) && metadata.Has<ApiInputAttribute>());
}