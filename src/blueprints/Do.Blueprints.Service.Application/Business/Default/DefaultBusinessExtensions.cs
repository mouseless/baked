using Do.Business;
using Do.Business.Attributes;
using Do.Business.Default;
using Do.Domain.Model;
using Do.RestApi.Model;
using System.Reflection;

namespace Do;

public static class DefaultBusinessExtensions
{
    public static DefaultBusinessFeature Default(this BusinessConfigurator _, List<Assembly> assemblies) =>
        new(assemblies);

    public static void AddAction(this ControllerModel controller, TypeModel type, string methodName)
    {
        if (!type.TryGetMembers(out var members) || !members.Methods.TryGetValue(methodName, out var method)) { return; }

        controller.AddAction(type, method);
    }

    public static void AddAction(this ControllerModel controller, TypeModel type, MethodModel method)
    {
        var overload = method.Overloads
            .Where(o => o.Parameters.All(p => p.ParameterType.TryGetMetadata(out var metadata) && metadata.Has<ApiInputAttribute>()))
            .OrderByDescending(o => o.Parameters.Count) // overload with most parameters
            .First();

        controller.Action.Add(
            method.Name,
            new(
                Id: method.Name,
                Method: HttpMethod.Post,
                Route: $"{type.Name}/{method.Name}",
                Return: new(overload.ReturnType),
                FindTargetStatement: "target"
            )
            {
                Parameters = [
                    new(type, ParameterModelFrom.Services, "target"),
                    .. overload.Parameters.Select(p =>
                        new RestApi.Model.ParameterModel(p.ParameterType, ParameterModelFrom.BodyOrForm, p.Name)
                        {
                            IsOptional = p.IsOptional,
                            DefaultValue = p.DefaultValue,
                            DefaultValueRenderer = defaultValue =>
                                p.ParameterType.Is<bool>() ? $"{defaultValue}".ToLowerInvariant() :
                                p.ParameterType.Is<bool?>() ? $"{defaultValue}".ToLowerInvariant() :
                                p.ParameterType.Is<string>() ? $"\"{defaultValue}\"" :
                                $"{defaultValue}"
                        }
                    )
                ]
            }
        );
    }
}