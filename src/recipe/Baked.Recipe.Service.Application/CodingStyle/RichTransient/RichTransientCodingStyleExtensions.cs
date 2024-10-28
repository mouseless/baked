using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.RichTransient;
using Baked.Domain.Model;
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

    public static string BuildInitializer(this ParameterModel factoryParameter, string valueExpression)
    {
        if (factoryParameter.TypeModel is null) { throw new("FactoryParameter shold have mapped parameter"); }

        var initializer = factoryParameter.TypeModel.GetMembers().Methods.Having<InitializerAttribute>().Single();

        return $"new{factoryParameter.TypeModel.Name.Pascalize()}().{initializer.Name}({valueExpression})";
    }

    public static string BuildInitializerByIds(this ParameterModel factoryParameter, string valueExpression,
        bool isArray = default
    )
    {
        if (factoryParameter.TypeModel is null) { throw new("FactoryParameter shold have mapped parameter"); }

        var initializer = factoryParameter.TypeModel.GetMembers().Methods.Having<InitializerAttribute>().Single();
        var byIds = $"{valueExpression}.Select(id => new{factoryParameter.TypeModel.Name.Pascalize()}().{initializer.Name}(id))";

        return isArray
            ? $"{byIds}.ToArray()"
            : $"{byIds}.ToList()";
    }
}