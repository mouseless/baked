using Do.Domain.Model;
using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;

namespace Do.Orm.AutoMap;

public class SingleByUniqueConvention(DomainModel _domainModel)
    : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Controller.TypeModel.TryGetMetadata(out var controllerMetadata)) { return; }
        if (!controllerMetadata.TryGetSingle<QueryAttribute>(out var queryAttribute)) { return; }

        var entityType = _domainModel.Types[queryAttribute.EntityType];

        var match = Regexes.StartsWithSingleBy().Match(context.Action.Id);
        if (!match.Success) { return; }

        var uniqueParameterName = match.Groups["Name"].Value;
        if (!context.Action.Parameter.TryGetValue(uniqueParameterName.Camelize(), out var uniqueParameter)) { return; }

        var newParameterName = $"{uniqueParameterName.Camelize()}";
        uniqueParameter.From = ParameterModelFrom.Route;
        uniqueParameter.Name = newParameterName;

        var newParameterRoute = $"{{{newParameterName}{(uniqueParameter.TypeModel.Is<Guid>() ? ":guid" : string.Empty)}}}";
        context.Action.Route = context.Action.Route.Replace(context.Action.Name, newParameterRoute);

        if (context.Action.Parameter.TryGetValue("throwNotFound", out var throwNotFoundParameter))
        {
            throwNotFoundParameter.IsHardCoded = true;
            throwNotFoundParameter.LookupRenderer = _ => "true";
        }
    }
}