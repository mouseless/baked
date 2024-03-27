using Do.Domain.Model;
using Do.Orm;
using Do.RestApi.Configuration;
using Do.RestApi.Model;

using ParameterModel = Do.RestApi.Model.ParameterModel;

namespace Do.Business.Default.RestApiConventions;

public class LookupEntityByIdConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        // if (!_domain.Types[context.Parameter.Type].IsEntity()) { return; }

        var entityType = _domain.Types[context.Parameter.Type];
        var queryContextType = _domain.Types[typeof(IQueryContext<>)]; //.MakeGenericType(entityType);

        var queryContextParameter = new ParameterModel(ParameterModelFrom.Services, queryContextType.RequiredFullName, $"{entityType.Name}Query");
        context.Action.Parameter[queryContextParameter.Name] = queryContextParameter;

        context.Parameter.Type = nameof(Guid);
        context.Parameter.Name += "Id";
        context.Parameter.ApiName += "Id";
        context.Parameter.RenderLookup = parameterExpression => $"{queryContextParameter.Name}.SingleById({parameterExpression})";
    }
}
