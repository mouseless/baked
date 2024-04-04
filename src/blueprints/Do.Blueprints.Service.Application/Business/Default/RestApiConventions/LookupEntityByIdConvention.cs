using Do.Domain.Model;
using Do.Orm;
using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;

using ParameterModel = Do.RestApi.Model.ParameterModel;

namespace Do.Business.Default.RestApiConventions;

public class LookupEntityByIdConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        var entityParameter = context.Parameter;
        var entityType = entityParameter.TypeModel;
        if (!entityType.TryGetMetadata(out var metadata) || !metadata.TryGetSingle<EntityAttribute>(out var entityAttribute)) { return; }

        var queryContextType = _domain.Types[entityAttribute.QueryContextType];
        var queryContextParameter = new ParameterModel(queryContextType, ParameterModelFrom.Services, $"{entityType.Name}Query") { IsInvokeMethodParameter = false };
        context.Action.Parameter[queryContextParameter.Name] = queryContextParameter;

        entityParameter.Type = nameof(Guid);
        entityParameter.Name += "Id";

        if (!entityParameter.IsInvokeMethodParameter)
        {
            entityParameter.Name = $"{entityParameter.TypeModel.Name.Camelize()}Id";
            entityParameter.From = ParameterModelFrom.Route;
            context.Action.Route = $"generated/{entityType.Name.Pluralize()}/{{{entityParameter.Name}:guid}}/{context.Action.Name}";
        }

        entityParameter.RenderLookup = parameterExpression => $"{queryContextParameter.Name}.SingleById({parameterExpression}, throwNotFound: {entityParameter.FromRoute.ToString().ToLowerInvariant()})";

        if (!entityParameter.IsInvokeMethodParameter)
        {
            context.Action.FindTargetStatement = entityParameter.RenderLookup(entityParameter.Name);
        }
    }
}
