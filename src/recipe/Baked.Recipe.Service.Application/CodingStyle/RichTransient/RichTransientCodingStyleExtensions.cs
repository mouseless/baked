using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.RichTransient;
using Baked.Domain.Model;
using Baked.RestApi;
using Baked.RestApi.Model;
using Humanizer;

using ParameterModel = Baked.RestApi.Model.ParameterModel;

namespace Baked;

public static class RichTransientCodingStyleExtensions
{
    public static RichTransientCodingStyleFeature RichTransient(this CodingStyleConfigurator _) =>
        new();

    public static ParameterModel AddFactoryAsService(this ActionModel action, TypeModel transientType)
    {
        var parameter =
            new ParameterModel(transientType, ParameterModelFrom.Services, $"new{transientType.Name.Pascalize()}")
            {
                IsInvokeMethodParameter = false,
                Type = $"Func<{transientType.CSharpFriendlyFullName}>"
            };

        action.Parameter[parameter.Name] = parameter;

        return parameter;
    }

    public static string BuildInitializerById(this ParameterModel factoryParameter, string valueExpression,
        string? notNullValueExpression = default,
        bool nullable = false
    )
    {
        if (factoryParameter.TypeModel is null) { throw new("FactoryParameter should have mapped parameter"); }

        notNullValueExpression ??= valueExpression;

        var initializer = factoryParameter.TypeModel.GetMembers().Methods.Having<InitializerAttribute>().Single();
        var initializerById = $"new{factoryParameter.TypeModel.Name.Pascalize()}().{initializer.Name}({notNullValueExpression})";

        if (nullable)
        {
            initializerById = $"({valueExpression} != null ? {initializerById} : null)";
        }

        return initializerById;
    }

    public static string BuildInitializerByIds(this ParameterModel factoryParameter, string valueExpression,
        bool isArray = default
    )
    {
        if (factoryParameter.TypeModel is null) { throw new("FactoryParameter should have mapped parameter"); }

        var initializer = factoryParameter.TypeModel.GetMembers().Methods.Having<InitializerAttribute>().Single();
        var byIds = $"{valueExpression}.Select(id => new{factoryParameter.TypeModel.Name.Pascalize()}().{initializer.Name}(id))";

        return isArray
            ? $"{byIds}.ToArray()"
            : $"{byIds}.ToList()";
    }
}