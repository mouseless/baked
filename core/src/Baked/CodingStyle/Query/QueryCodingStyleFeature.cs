using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Model;
using Baked.RestApi.Conventions;
using Humanizer;

namespace Baked.CodingStyle.Query;

public class QueryCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(
                when: c =>
                    c.Type.Has<LocatableAttribute>() &&
                    c.Domain.Types.TryGetValue(((IModel)c.Type).Id.Pluralize(), out var query) &&
                    query.TryGetMetadata(out var queryMetadata) &&
                    !queryMetadata.Has<QueryAttribute>(),
                apply: (c, set) =>
                {
                    var queryType = c.Domain.Types[((IModel)c.Type).Id.Pluralize()];
                    set(queryType.GetMetadata(), c.Type.Apply(t => new QueryAttribute(t)));

                    var locatable = c.Type.Get<LocatableAttribute>();
                    queryType.Apply(qt => locatable.QueryType = qt);
                },
                order: 30
            );

            builder.Conventions.Add(new AutoHttpMethodConvention([(Regexes.StartsWithFirstBySingleByOrBy, HttpMethod.Get)]), order: -10);
            builder.Conventions.Add(new RemoveFromRouteConvention(["FirstBy", "SingleBy", "By"],
                _whenContext: c => c.Type.TryGetMetadata(out var metadata) && metadata.Has<QueryAttribute>()
            ));
        });
    }
}