using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.RichTransient;
using Baked.Domain.Model;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked;

public static class RichTransientCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public RichTransientCodingStyleFeature RichTransient() =>
            new();
    }

    extension(ActionModelAttribute action)
    {
        public ParameterModelAttribute AddFactoryAsService(TypeModel transientType)
        {
            var parameter =
                new ParameterModelAttribute($"new{transientType.Name.Pascalize()}", $"Func<{transientType.CSharpFriendlyFullName}>", ParameterModelFrom.Services)
                {
                    IsInvokeMethodParameter = false,
                };

            action.Parameter[parameter.Name] = parameter;

            return parameter;
        }
    }

    extension(TypeModel type)
    {
        public string BuildInitializerById(string valueExpression,
            string? notNullValueExpression = default,
            bool nullable = false
        )
        {
            notNullValueExpression ??= valueExpression;

            var initializer = type.GetMembers().Methods.Having<InitializerAttribute>().Single();
            var initializerById = $"new{type.Name.Pascalize()}().{initializer.Name}({notNullValueExpression})";
            if (initializer.DefaultOverload.ReturnType.IsAssignableTo<Task>())
            {
                initializerById = $"(await {initializerById})";
            }

            if (nullable)
            {
                initializerById = $"({valueExpression} != null ? {initializerById} : null)";
            }

            return initializerById;
        }

        public string BuildInitializerByIds(string valueExpression,
            bool isArray = default
        )
        {
            var initializer = type.GetMembers().Methods.Having<InitializerAttribute>().Single();
            var byIds = $"{valueExpression}.Select(id => new{type.Name.Pascalize()}().{initializer.Name}(id))";
            if (initializer.DefaultOverload.ReturnType.IsAssignableTo<Task>())
            {
                byIds = $"(await Task.WhenAll({byIds}))";
            }

            return isArray
                ? $"{byIds}.ToArray()"
                : $"{byIds}.ToList()";
        }
    }
}