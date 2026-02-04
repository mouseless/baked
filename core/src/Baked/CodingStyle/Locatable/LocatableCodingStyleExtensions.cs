using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.Locatable;
using Baked.Domain;
using Baked.Domain.Model;
using Baked.RestApi;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked;

public static class LocatableCodingStyleExtensions
{
    public static LocatableCodingStyleFeature Locatable(this CodingStyleConfigurator _) =>
        new();

    public static ParameterModelAttribute AddLocatorAsService(this LocatableAttribute locatable, ActionModelAttribute action, TypeModel locatableType) =>
        action.Parameter[$"{locatableType.Name.Camelize()}Locator"] = new($"{locatableType.Name.Camelize()}Locator", locatable.BuildLocatorType(locatableType.CSharpFriendlyFullName), ParameterModelFrom.Services)
        {
            IsInvokeMethodParameter = false
        };

    public static void AddLocateAction<TLocatable>(this IDomainModelConventionCollection conventions) =>
        conventions.Add(new AddLocateActionConvention<TLocatable>(), order: RestApiLayer.MaxConventionOrder);

    public static string BuildLocateMany(this LocatableAttribute locatable, ParameterModelAttribute locatorServiceParameter, string parameter,
        bool isArray = false
    )
    {
        var locateMany = locatable.LocateManyRenderer(locatorServiceParameter.Name, parameter);

        return isArray ? $"({locateMany}).ToArray()" : $"({locateMany}).ToList()";
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