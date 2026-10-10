using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.RestApi.Conventions;
using Humanizer;

namespace Baked.CodingStyle.Query;

public class QueryCodingStyleFeature(
    HashSet<string> _queryMethodNames,
    HashSet<string> _primaryParameterNames,
    HashSet<string> _takeParameterNames,
    HashSet<string> _skipParameterNames,
    HashSet<string> _sortingParameterNames
) : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Method.Add<QueryMethod>();
            builder.Index.Parameter.Add<Paging>();
            builder.Index.Parameter.Add<Sorting>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c =>
                    c.Type.Has<LocatableAttribute>() &&
                    c.Domain.Types.TryGetValue(((IModel)c.Type).Id.Pluralize(), out var query) &&
                    query.TryGetMetadata(out var queryMetadata) &&
                    !queryMetadata.Has<QueryClass>(),
                apply: (c, set) =>
                {
                    var queryType = c.Domain.Types[((IModel)c.Type).Id.Pluralize()];
                    set(queryType.GetMetadata(), c.Type.Apply(t => new QueryClass(t)));

                    var locatable = c.Type.Get<LocatableAttribute>();
                    queryType.Apply(qt => locatable.QueryType = qt);
                },
                order: Order.At.Infra + 30
            );

            conventions.Add(new AutoHttpMethodConvention([(Regexes.StartsWithFirstBySingleByOrBy, HttpMethod.Get)]), order: Order.At.Infra - 10);
            conventions.Add(new RemoveFromRouteConvention(["By"],
                _whenContext: c =>
                    c.Type.TryGetMetadata(out var metadata) &&
                    metadata.Has<QueryClass>() &&
                    c.Method.Name.EndsWith("By")
            ), order: Order.At.Infra);

            conventions.SetMethodAttribute(
                when: c => c.Type.Has<QueryClass>() && _queryMethodNames.Contains(c.Method.Name),
                attribute: () => new QueryMethod(),
                order: Order.At.Infra + 40
            );
            conventions.EditMethodAttribute<QueryMethod>(
                when: c => c.Method.DefaultOverload.Parameters.All(p => p.IsOptional),
                attribute: qm => qm.AllParametersAreOptional = true,
                order: Order.At.Infra
            );
            conventions.EditMethodAttribute<QueryMethod>(
                when: c => c.Method.DefaultOverload.Parameters.Any(p => _primaryParameterNames.Contains(p.Name)),
                attribute: (qm, c) =>
                {
                    var primaryParameter =
                        c.Method.DefaultOverload.Parameters.FirstOrDefault(p => _primaryParameterNames.Contains(p.Name)) ??
                        throw DiagnosticCode.InvalidState.Exception(
                            $"{c.Type.Name}.{c.Method.Name} is expected to contain a parameter with name that matches one of ({_primaryParameterNames.Join(", ")})"
                        );

                    qm.PrimaryParameterName = primaryParameter.Name;
                },
                order: Order.At.Infra
            );

            conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethod>() && _takeParameterNames.Contains(c.Parameter.Name),
                attribute: () => new Paging(Paging.Role.Take),
                order: Order.At.Infra + 40
            );

            conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethod>() && _skipParameterNames.Contains(c.Parameter.Name),
                attribute: () => new Paging(Paging.Role.Skip),
                order: Order.At.Infra + 40
            );

            conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethod>() && _sortingParameterNames.Contains(c.Parameter.Name),
                attribute: () => new Sorting(),
                order: Order.At.Infra + 40
            );
        });
    }
}