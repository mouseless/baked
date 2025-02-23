using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.RichTransient;
using Baked.Domain.Model;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked;

public static class RichTransientCodingStyleExtensions
{
    public static RichTransientCodingStyleFeature RichTransient(this CodingStyleConfigurator _) =>
        new();

    public static ParameterModelAttribute AddFactoryAsService(this ActionModelAttribute action, TypeModel transientType)
    {
        var parameter =
            new ParameterModelAttribute($"new{transientType.Name.Pascalize()}", $"Func<{transientType.CSharpFriendlyFullName}>", ParameterModelFrom.Services)
            {
                IsInvokeMethodParameter = false,
            };

        action.Parameter[parameter.Name] = parameter;

        return parameter;
    }

    public static string BuildInitializerById(this TypeModel type, string valueExpression,
        string? notNullValueExpression = default,
        bool nullable = false
    )
    {
        notNullValueExpression ??= valueExpression;

        var initializer = type.GetMembers().Methods.Having<InitializerAttribute>().Single();
        var initializerById = $"new{type.Name.Pascalize()}().{initializer.Name}({notNullValueExpression})";

        if (nullable)
        {
            initializerById = $"({valueExpression} != null ? {initializerById} : null)";
        }

        return initializerById;
    }

    public static string BuildInitializerByIds(this TypeModel type, string valueExpression,
        bool isArray = default
    )
    {
        var initializer = type.GetMembers().Methods.Having<InitializerAttribute>().Single();
        var byIds = $"{valueExpression}.Select(id => new{type.Name.Pascalize()}().{initializer.Name}(id))";

        return isArray
            ? $"{byIds}.ToArray()"
            : $"{byIds}.ToList()";
    }
}