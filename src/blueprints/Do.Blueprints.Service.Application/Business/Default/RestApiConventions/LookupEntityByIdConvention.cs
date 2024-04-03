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
        var entityType = context.Parameter.TypeModel;
        if (!entityType.TryGetMetadata(out var metadata) || !metadata.TryGetSingle<EntityAttribute>(out var entityAttribute)) { return; }

        var queryContextType = _domain.Types[entityAttribute.QueryContextType];
        var queryContextParameter = new ParameterModel(queryContextType, ParameterModelFrom.Services, $"{entityType.Name}Query") { IsInvokeMethodParameter = false };
        context.Action.Parameter[queryContextParameter.Name] = queryContextParameter;

        context.Parameter.Type = nameof(Guid);
        context.Parameter.Name += "Id";

        if (!context.Parameter.IsInvokeMethodParameter)
        {
            context.Parameter.From = ParameterModelFrom.Route;
            context.Action.Route = $"generated/{entityType.Name.Pluralize()}/{{{context.Parameter.Name}}}/{context.Action.Name}";
        }

        context.Parameter.RenderLookup = parameterExpression => $"{queryContextParameter.Name}.SingleById({parameterExpression}, throwNotFound: {context.Parameter.FromRoute.ToString().ToLowerInvariant()})";

        if (!context.Parameter.IsInvokeMethodParameter)
        {
            context.Action.FindTargetStatement = context.Parameter.RenderLookup(context.Parameter.Name);
        }
    }
}
