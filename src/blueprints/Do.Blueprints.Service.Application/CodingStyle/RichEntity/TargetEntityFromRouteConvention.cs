using Do.Domain.Model;
using Do.Orm;
using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;

using ParameterModel = Do.RestApi.Model.ParameterModel;

namespace Do.CodingStyle.RichEntity;

public class TargetEntityFromRouteConvention(DomainModel _domain, Func<ActionModel, bool> _actionFilter)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!_actionFilter(context.Action)) { return; }

        var entityParameter = context.Parameter;
        if (entityParameter.IsInvokeMethodParameter) { return; }

        var entityType = entityParameter.TypeModel;
        if (!entityType.TryGetMetadata(out var entityMetadata) || !entityMetadata.TryGetSingle<EntityAttribute>(out var entityAttribute)) { return; }

        var queryContextType = _domain.Types[entityAttribute.QueryContextType];
        var queryContextParameter = new ParameterModel(queryContextType, ParameterModelFrom.Services, $"{entityType.Name.Camelize()}Query") { IsInvokeMethodParameter = false };
        context.Action.Parameter[queryContextParameter.Name] = queryContextParameter;

        entityParameter.Type = nameof(Guid);
        entityParameter.Name = $"{entityParameter.TypeModel.Name.Camelize()}Id";
        entityParameter.From = ParameterModelFrom.Route;

        context.Action.Route = $"{entityType.Name.Pluralize()}/{{{entityParameter.Name}:guid}}/{context.Action.Name}";
        context.Action.FindTargetStatement = $"{queryContextParameter.Name}.SingleById({entityParameter.Name}, throwNotFound: true)";
    }
}