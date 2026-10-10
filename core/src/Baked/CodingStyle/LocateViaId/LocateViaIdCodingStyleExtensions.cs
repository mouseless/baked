using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.LocateViaId;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.RestApi.Model;
using Humanizer;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class LocateViaIdCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public LocateViaIdCodingStyleFeature LocateViaId() =>
            new();
    }

    extension(Locatable locatable)
    {
        public ApiParameter AddLocatorAsService(ApiAction action, TypeModel locatableType) =>
            action.Parameter[$"{locatableType.Name.Camelize()}Locator"] = new($"{locatableType.Name.Camelize()}Locator", locatable.RenderLocatorType(locatableType.CSharpFriendlyFullName), ParameterModelFrom.Services)
            {
                IsInvokeMethodParameter = false
            };

        public string BuildLocate(ApiParameter locatorServiceParameter, string parameter,
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

        public string BuildLocateMany(ApiParameter locatorServiceParameter, string parameter,
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
            conventions.Add(new AddLocateActionConvention<TLocatable>(), order: Order.At.Infra.Max - 20);
    }

    extension(ApiParameter parameter)
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
        public bool TryGetLocatable([NotNullWhen(true)] out Locatable? locatable)
        {
            locatable = default;

            return
                type.TryGetMetadata(out var metadata) &&
                metadata.TryGet(out locatable);
        }

        public bool TryGetQueryType(DomainModel domain, [NotNullWhen(true)] out TypeModel? queryType)
        {
            if (!type.TryGetLocatable(out var locatable) || locatable.QueryType is null)
            {
                queryType = default;

                return false;
            }

            queryType = domain.Types[locatable.QueryType];

            return true;
        }
    }
}