using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.TransientBinding;
using Baked.Domain;
using Baked.RestApi;
using Baked.RestApi.Model;
using Humanizer;

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

    public static void AddSingleById<TQuery>(this IDomainModelConventionCollection conventions) =>
        conventions.Add(new SingleByIdConvention<TQuery>(), order: RestApiLayer.MaxConventionOrder);

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

    public static void ConvertToId(this ParameterModelAttribute parameter, IdInfo idInfo,
        string? name = default,
        bool dontAddRequired = false
    )
    {
        name ??= $"{parameter.Name}{idInfo.PropertyName}";

        if (!parameter.Nullable && dontAddRequired)
        {
            parameter.AddRequiredAttributes(isValueType: true);
        }

        parameter.Type = parameter.Nullable ? $"{idInfo.Type}?" : idInfo.Type;
        parameter.Name = name;
    }

    public static void ConvertToIds(this ParameterModelAttribute parameter, IdInfo idInfo)
    {
        parameter.Type = $"IEnumerable<{idInfo.Type}>";
        parameter.Name = $"{parameter.Name.Singularize()}{idInfo.PropertyName.Pluralize()}";
    }
}