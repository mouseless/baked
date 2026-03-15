using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.Locatable;
using Baked.Domain;
using Baked.Domain.Model;
using Baked.RestApi;
using Baked.RestApi.Model;
using Humanizer;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class LocatableCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public LocatableCodingStyleFeature Locatable() =>
            new();
    }

    extension(LocatableAttribute locatable)
    {
        public ParameterModelAttribute AddLocatorAsService(ActionModelAttribute action, TypeModel locatableType) =>
            action.Parameter[$"{locatableType.Name.Camelize()}Locator"] = new($"{locatableType.Name.Camelize()}Locator", locatable.RenderLocatorType(locatableType.CSharpFriendlyFullName), ParameterModelFrom.Services)
            {
                IsInvokeMethodParameter = false
            };

        public string BuildLocate(ParameterModelAttribute locatorServiceParameter, string parameter,
            string? notNullParameterExpression = default,
            bool nullable = false
        )
        {
            notNullParameterExpression ??= parameter;

            var locate = locatable.RenderLocate(locatorServiceParameter.Name, notNullParameterExpression);
            if (nullable)
            {
                locate = $"({parameter} != null ? {locate} : null)";
            }

            return locate;
        }

        public string BuildLocateMany(ParameterModelAttribute locatorServiceParameter, string parameter,
            bool isArray = false
        )
        {
            var locateMany = locatable.RenderLocateMany(locatorServiceParameter.Name, parameter);

            return isArray ? $"({locateMany}).ToArray()" : $"({locateMany}).ToList()";
        }
    }

    extension(IDomainModelConventionCollection conventions)
    {
        public void AddLocateAction<TLocatable>() =>
            conventions.Add(new AddLocateActionConvention<TLocatable>(), order: RestApiLayer.MaxConventionOrder);
    }

    extension(ParameterModelAttribute parameter)
    {
        public void ConvertToId(IdInfo idInfo,
            string? name = default,
            bool dontAddRequired = false,
            bool nullable = false
        )
        {
            name ??= $"{parameter.Name}{idInfo.PropertyName}";

            if (!nullable && dontAddRequired)
            {
                parameter.AddRequiredAttributes(isValueType: true);
            }

            parameter.Type = nullable ? $"{idInfo.Type}?" : idInfo.Type;
            parameter.Name = name;
        }

        public void ConvertToIds(IdInfo idInfo)
        {
            parameter.Type = $"IEnumerable<{idInfo.Type}>";
            parameter.Name = $"{parameter.Name.Singularize()}{idInfo.PropertyName.Pluralize()}";
        }
    }

    extension(TypeModel type)
    {
        public bool TryGetLocatableAttribute([NotNullWhen(true)] out LocatableAttribute? locatableAttribute)
        {
            locatableAttribute = default;

            return
                type.TryGetMetadata(out var metadata) &&
                metadata.TryGet(out locatableAttribute);
        }

        public bool TryGetQueryType(DomainModel domain, [NotNullWhen(true)] out TypeModel? queryType)
        {
            if (!type.TryGetLocatableAttribute(out var locatableAttribute) || locatableAttribute.QueryType is null)
            {
                queryType = default;

                return false;
            }

            queryType = domain.Types[locatableAttribute.QueryType];

            return true;
        }
    }
}