using Do.Business;
using Do.Business.DomainAssemblies;
using Do.Domain.Model;
using Do.RestApi.Model;
using System.Reflection;

namespace Do;

public static class DomainAssembliesBusinessExtensions
{
    public static DomainAssembliesBusinessFeature DomainAssemblies(this BusinessConfigurator _, List<Assembly> assemblies,
        Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel>? overloadSelector = default
    ) => new(
        assemblies,
        overloadSelector ?? (overloads => overloads.OrderByDescending(o => o.Parameters.Count).First())
    );

    public static void AddAction(this ControllerModel controller, TypeModel type, MethodModel method, MethodOverloadModel overload) =>
        controller.Action.Add(
            method.Name,
            new(
                Id: method.Name,
                Method: HttpMethod.Post,
                Route: $"{type.Name}/{method.Name}",
                Return: new(overload.ReturnType),
                FindTargetStatement: "target",
                MethodModel: method
            )
            {
                Parameters = [
                    new(type, ParameterModelFrom.Services, "target"),
                    .. overload.Parameters.Select(p =>
                        new RestApi.Model.ParameterModel(p.ParameterType, ParameterModelFrom.BodyOrForm, p.Name)
                        {
                            IsOptional = p.IsOptional,
                            DefaultValue = p.DefaultValue,
                            DefaultValueRenderer = defaultValue => $"{defaultValue}"
                        }
                    )
                ]
            }
        );

    public static bool AllParametersAreApiInput(this MethodOverloadModel overload) =>
        overload.Parameters.All(p => p.ParameterType.TryGetMetadata(out var metadata) && metadata.Has<ApiInputAttribute>());
}