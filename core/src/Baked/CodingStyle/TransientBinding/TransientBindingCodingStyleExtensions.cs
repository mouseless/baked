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
        if (locatable.IsFactory)
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
        var locatorTemplate = $"{locatorServiceParameter.Name}{(locatable.IsFactory ? "()" : string.Empty)}";
        locatorTemplate += $".{locatable.LocateMethodName}({parameters.Select(p => $"{p.InternalName}: {p.RenderLookup($"@{p.Name}")}").Join(", ")})";

        return castTo is null
           ? locatorTemplate
           : $"({castTo.CSharpFriendlyFullName}){locatorTemplate}";
    }

    public static string LookupTemplate(this LocatableAttribute locatable, ParameterModelAttribute locatorServiceParameter, string parameter,
        string? notNullValueExpression = default,
        bool nullable = false,
        TypeModel? castTo = default,
        bool? await = default
    )
    {
        notNullValueExpression ??= parameter;
        await ??= locatable.IsAsync;

        var template = $"{(await.Value ? "await " : string.Empty)}{locatorServiceParameter.Name}{(locatable.IsFactory ? "()" : string.Empty)}";
        template += $".{locatable.LocateMethodName}({notNullValueExpression})";

        if (nullable)
        {
            template = $"({parameter} != null ? {template} : null)";
        }

        return castTo is null
           ? template
           : $"({castTo.CSharpFriendlyFullName}){template}";
    }

    public static string LookupManyTemplate(this LocatableAttribute locatable, ParameterModelAttribute locatorServiceParameter, string parameter,
        TypeModel? castTo = default,
        bool isArray = false
    )
    {
        var template = locatable.LocateManyMethodName is not null
            ? $"{locatorServiceParameter.Name}.{locatable.LocateManyMethodName}({parameter})"
            : $"{parameter}.Select(p => {locatable.LookupTemplate(locatorServiceParameter, "p", await: false)})";

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