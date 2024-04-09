using Do.Domain.Model;
using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;

using ParameterModel = Do.RestApi.Model.ParameterModel;

namespace Do.Orm.Default;

public class LookupEntityByIdConvention(DomainModel _domain, Func<ActionModel, bool> _actionFilter)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!_actionFilter(context.Action)) { return; }

        var entityParameter = context.Parameter;
        if (!entityParameter.IsInvokeMethodParameter) { return; }

        var entityType = entityParameter.TypeModel;
        if (!entityType.TryGetMetadata(out var metadata) || !metadata.TryGetSingle<EntityAttribute>(out var entityAttribute)) { return; }

        var queryContextType = _domain.Types[entityAttribute.QueryContextType];
        var queryContextParameter = new ParameterModel(queryContextType, ParameterModelFrom.Services, $"{entityType.Name.Camelize()}Query") { IsInvokeMethodParameter = false };
        context.Action.Parameter[queryContextParameter.Name] = queryContextParameter;

        entityParameter.Type = nameof(Guid);
        entityParameter.Name += "Id";
        entityParameter.LookupRenderer = parameterExpression => $"{queryContextParameter.Name}.SingleById({parameterExpression})";
    }
}