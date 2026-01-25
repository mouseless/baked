using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.TransientBinding;
using Baked.Domain.Model;
using Baked.RestApi.Model;

namespace Baked;

public static class TransientBindingCodingStyleExtensions
{
    public static TransientBindingCodingStyleFeature TransientBinding(this CodingStyleConfigurator _) =>
        new();

    public static ParameterModelAttribute AddAsService(this LocatableAttribute locatable, ActionModelAttribute action, string parameterName)
    {
        if (locatable.FromFactory)
        {
            return action.Parameter[parameterName] =
                new(parameterName, typeof(Func<>).MakeGenericType(locatable.ServiceType).GetCSharpFriendlyFullName(), ParameterModelFrom.Services)
                {
                    IsInvokeMethodParameter = false
                };
        }

        return action.Parameter[parameterName] =
            new(parameterName, locatable.ServiceType.GetCSharpFriendlyFullName(), ParameterModelFrom.Services)
            {
                IsInvokeMethodParameter = false
            };
    }

    public static string TargetTemplate(this LocatableAttribute locatable, ParameterModelAttribute locatorServiceParameter, ParameterModelAttribute[] parameters,
        TypeModel? castTo = default
    )
    {
        var locatorTemplate = $"{locatorServiceParameter.Name}{(locatable.FromFactory ? "()" : string.Empty)}";
        locatorTemplate += $".{locatable.LocateSingleMethodName}({parameters.Select(p => $"{p.InternalName}: {p.RenderLookup($"@{p.Name}")}").Join(", ")})";

        return castTo is null
           ? locatorTemplate
           : $"({castTo.CSharpFriendlyFullName}){locatorTemplate}";
    }

    public static string LookupSingleTemplate(this LocatableAttribute locatable, ParameterModelAttribute locatorServiceParameter, string parameter,
        string? notNullValueExpression = default,
        bool nullable = false,
        TypeModel? castTo = default,
        bool? await = default
    )
    {
        notNullValueExpression ??= parameter;
        await ??= locatable.IsAsync;

        var template = $"{(await.Value ? "await " : string.Empty)}{locatorServiceParameter.Name}{(locatable.FromFactory ? "()" : string.Empty)}";
        template += $".{locatable.LocateSingleMethodName}({notNullValueExpression})";

        if (nullable)
        {
            template = $"({parameter} != null ? {template} : null)";
        }

        return castTo is null
           ? template
           : $"({castTo.CSharpFriendlyFullName}){template}";
    }

    public static string LookupMultipleTemplate(this LocatableAttribute locatable, ParameterModelAttribute locatorServiceParameter, string parameter,
        TypeModel? castTo = default,
        bool isArray = false
    )
    {
        var template = locatable.LocateMultipleMethodName is not null
            ? $"{locatorServiceParameter.Name}.{locatable.LocateMultipleMethodName}({parameter})"
            : $"{parameter}.Select(p => {locatable.LookupSingleTemplate(locatorServiceParameter, "p", await: false)})";

        if (castTo is not null)
        {
            template += $".Select(i => ({castTo.CSharpFriendlyFullName})i)";
        }

        if (locatable.IsAsync)
        {
            template = $"(await Task.WhenAll({template}))";
        }

        template = isArray ? $"{template}.ToArray()" : $"{template}.ToList()";

        return template;
    }
}