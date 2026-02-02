using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.TransientBinding;
using Baked.RestApi.Model;

namespace Baked;

public static class TransientBindingCodingStyleExtensions
{
    public static TransientBindingCodingStyleFeature TransientBinding(this CodingStyleConfigurator _) =>
        new();

    public static ParameterModelAttribute AddAsService(this LocatableAttribute locatable, ActionModelAttribute action, string parameterName) =>
        action.Parameter[parameterName] = new(parameterName, locatable.ServiceType.GetCSharpFriendlyFullName(), ParameterModelFrom.Services)
        {
            IsInvokeMethodParameter = false
        };

    public static string BuildLookupRenderer(this LocatableAttribute locatable, ParameterModelAttribute locatorServiceParameter, string parameter,
        string? notNullValueExpression = default
    )
    {
        notNullValueExpression ??= parameter;

        return locatable.LocateRenderer(locatorServiceParameter.Name, notNullValueExpression);
    }

    public static string BuildLookupManyTemplate(this LocatableAttribute locatable, ParameterModelAttribute locatorServiceParameter, string parameter,
        bool isArray = false
    )
    {
        var template = locatable.LocateManyRenderer is not null
            ? locatable.LocateManyRenderer(locatorServiceParameter.Name, parameter)
            : locatable.IsAsync
                ? $"await {parameter}.Select(async p => await {locatable.LocateRenderer(locatorServiceParameter.Name, "p")})"
                : $"{parameter}.Select(p => {locatable.LocateRenderer(locatorServiceParameter.Name, "p")})";

        return isArray ? $"({template}).ToArray()" : $"({template}).ToList()";
    }
}