using Do.Architecture;
using Do.Domain.Model;
using Do.Orm;
using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

using ParameterModel = Do.RestApi.Model.ParameterModel;

namespace Do;

public static class OrmExtensions
{
    public static void AddOrm(this List<IFeature> source, Func<OrmConfigurator, IFeature<OrmConfigurator>> configure) =>
        source.Add(configure(new()));

    public static ParameterModel AddAsService(this ActionModel action, TypeModel type,
        string? name = default
    )
    {
        name ??= type.Name.Camelize();

        var parameter =
            new ParameterModel(type, ParameterModelFrom.Services, name)
            {
                IsInvokeMethodParameter = false
            };

        action.Parameter[parameter.Name] = parameter;

        return parameter;
    }

    public static ParameterModel AddQueryContextAsService(this ActionModel action, TypeModel queryContextType)
    {
        var entityType = queryContextType.GetGenerics().GenericTypeArguments[0].Model;

        return action.AddAsService(queryContextType, name: $"{entityType.Name.Camelize()}Query");
    }

    public static void MoveParameterToRoute(this ParameterModelContext context, string resourceName,
        string? constraint = default
    )
    {
        constraint = constraint is null ? string.Empty : $":{constraint}";

        context.Parameter.From = ParameterModelFrom.Route;
        context.Action.Route = $"{resourceName}/{{{context.Parameter.Name}{constraint}}}/{context.Action.Name}";
    }

    public static string BuildSingleBy(this ParameterModel queryContextParameter, string valueExpression,
        string property = "Id",
        bool fromRoute = false,
        TypeModel? castTo = default
    )
    {
        var singleBy = fromRoute
            ? $"{queryContextParameter.Name}.SingleBy{property}({valueExpression}, throwNotFound: true)"
            : $"{queryContextParameter.Name}.SingleBy{property}({valueExpression})";

        return castTo is null
            ? singleBy
            : $"({castTo.CSharpFriendlyFullName}){singleBy}";
    }

    public static string BuildByIds(this ParameterModel queryContextParameter, string valueExpression,
        TypeModel? castTo = default,
        bool isArray = false
    )
    {
        var byIds = $"{queryContextParameter.Name}.ByIds({valueExpression})";
        if (castTo is not null)
        {
            byIds += $".Select(i => ({castTo.CSharpFriendlyFullName})i)";
        }

        return isArray
            ? $"{byIds}.ToArray()"
            : $"{byIds}.ToList()";
    }

    public static void ConvertToId(this ParameterModel parameter,
        string? name = default
    )
    {
        name ??= $"{parameter.Name}Id";

        parameter.Type = nameof(Guid);
        parameter.Name = name;
    }

    public static void ConvertToIds(this ParameterModel parameter)
    {
        parameter.Type = "IEnumerable<Guid>";
        parameter.Name = $"{parameter.Name.Singularize()}Ids";
    }

    public static bool TryGetQueryType(this TypeModel type, DomainModel domain, [NotNullWhen(true)] out TypeModel? queryType)
    {
        if (!type.TryGetEntityAttribute(out var entityAttribute))
        {
            queryType = default;

            return false;
        }

        queryType = domain.Types[entityAttribute.QueryType];

        return true;
    }

    public static bool TryGetQueryContextType(this TypeModel type, DomainModel domain, [NotNullWhen(true)] out TypeModel? queryContextType)
    {
        if (!type.TryGetEntityAttribute(out _))
        {
            queryContextType = default;

            return false;
        }

        queryContextType = domain.Types[domain.Types[typeof(IQueryContext<>)].MakeGenericTypeId(type)];

        return true;
    }

    public static bool TryGetEntityAttribute(this TypeModel type, [NotNullWhen(true)] out EntityAttribute? entityAttribute)
    {
        entityAttribute = default;

        return
            type.TryGetMetadata(out var metadata) &&
            metadata.TryGetSingle(out entityAttribute);
    }

    public static void ShouldBeDeleted(this object @object) =>
        ServiceSpec.Session.Contains(@object).ShouldBeFalse($"{@object} should've been deleted, but it's STILL in the session");

    public static void ShouldBeInserted(this object @object) =>
        ServiceSpec.Session.Contains(@object).ShouldBeTrue($"{@object} should've been inserted, but it's NOT in the session");
}