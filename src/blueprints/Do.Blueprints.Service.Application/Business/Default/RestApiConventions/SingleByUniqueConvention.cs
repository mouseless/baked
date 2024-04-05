using Do.Domain.Model;
using Do.Orm;
using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;

namespace Do.Business.Default.RestApiConventions;

public class SingleByUniqueConvention(DomainModel _domainModel)
    : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Controller.TypeModel.TryGetMetadata(out var controllerMetadata)) { return; }
        if (!controllerMetadata.TryGetSingle<QueryAttribute>(out var queryAttribute)) { return; }

        var entityType = _domainModel.Types[queryAttribute.EntityType];

        var match = Regexes.SingleByUniqueMethod().Match(context.Action.Id);
        if (!match.Success) { return; }

        var uniqueParameterName = match.Groups["Unique"].Value;
        if (!context.Action.Parameter.TryGetValue(uniqueParameterName.Camelize(), out var uniqueParameter)) { return; }

        var newParameterName = $"{entityType.Name.Camelize()}{uniqueParameterName}";
        context.Action.Route = context.Action.Route.Replace(context.Action.Id, $"{{{newParameterName}}}");
        uniqueParameter.From = ParameterModelFrom.Route;
        uniqueParameter.Name = newParameterName;

        if (context.Action.Parameter.TryGetValue("throwNotFound", out var throwNotFoundParameter))
        {
            throwNotFoundParameter.IsHardCoded = true;
            throwNotFoundParameter.LookupRenderer = _ => "true";
        }
    }
}
