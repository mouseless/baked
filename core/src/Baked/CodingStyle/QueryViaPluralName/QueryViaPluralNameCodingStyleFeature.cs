using Baked.Architecture;
using Baked.Business;
using Baked.CodingStyle.QueryViaPluralName;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.RestApi.Conventions;
using Humanizer;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class QueryViaPluralNameCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> QueryViaPluralName(this CodingStyleFeature.Configurator x,
        HashSet<string>? queryMethodNames = default,
        HashSet<string>? primaryParameterNames = default,
        HashSet<string>? takeParameterNames = default,
        HashSet<string>? skipParameterNames = default,
        HashSet<string>? sortingParameterNames = default
    ) => x.Define((
        queryMethodNames ?? ["By"],
        primaryParameterNames ?? ["searchText"],
        takeParameterNames ?? ["take"],
        skipParameterNames ?? ["skip"],
        sortingParameterNames ?? ["sort"]
    ), (configurator, args) =>
    {
        var (queryMethodNames, primaryParameterNames, takeParameterNames, skipParameterNames, sortingParameterNames) = args;

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
                    c.Type.Has<Locatable>() &&
                    c.Domain.Types.TryGetValue(((IModel)c.Type).Id.Pluralize(), out var query) &&
                    query.TryGetMetadata(out var queryMetadata) &&
                    !queryMetadata.Has<Query>(),
                apply: (c, set) =>
                {
                    var queryType = c.Domain.Types[((IModel)c.Type).Id.Pluralize()];
                    set(queryType.GetMetadata(), c.Type.Apply(t => new Query(t)));

                    var locatable = c.Type.Get<Locatable>();
                    queryType.Apply(qt => locatable.QueryType = qt);
                },
                order: Order.At.Infra + 30
            );

            conventions.Add(new AutoHttpMethodConvention([(Regexes.StartsWithFirstBySingleByOrBy, HttpMethod.Get)]), order: Order.At.Infra - 10);
            conventions.Add(new RemoveFromRouteConvention(["By"],
                _whenContext: c =>
                    c.Type.TryGetMetadata(out var metadata) &&
                    metadata.Has<Query>() &&
                    c.Method.Name.EndsWith("By")
            ), order: Order.At.Infra);

            conventions.SetMethodAttribute(
                when: c => c.Type.Has<Query>() && queryMethodNames.Contains(c.Method.Name),
                attribute: () => new QueryMethod(),
                order: Order.At.Infra + 40
            );
            conventions.EditMethodAttribute<QueryMethod>(
                when: c => c.Method.DefaultOverload.Parameters.All(p => p.IsOptional),
                attribute: qm => qm.AllParametersAreOptional = true,
                order: Order.At.Infra
            );
            conventions.EditMethodAttribute<QueryMethod>(
                when: c => c.Method.DefaultOverload.Parameters.Any(p => primaryParameterNames.Contains(p.Name)),
                attribute: (qm, c) =>
                {
                    var primaryParameter =
                        c.Method.DefaultOverload.Parameters.FirstOrDefault(p => primaryParameterNames.Contains(p.Name)) ??
                        throw DiagnosticCode.InvalidState.Exception(
                            $"{c.Type.Name}.{c.Method.Name} is expected to contain a parameter with name that matches one of ({primaryParameterNames.Join(", ")})"
                        );

                    qm.PrimaryParameterName = primaryParameter.Name;
                },
                order: Order.At.Infra
            );

            conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethod>() && takeParameterNames.Contains(c.Parameter.Name),
                attribute: () => new Paging(Paging.Role.Take),
                order: Order.At.Infra + 40
            );

            conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethod>() && skipParameterNames.Contains(c.Parameter.Name),
                attribute: () => new Paging(Paging.Role.Skip),
                order: Order.At.Infra + 40
            );

            conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethod>() && sortingParameterNames.Contains(c.Parameter.Name),
                attribute: () => new Sorting(),
                order: Order.At.Infra + 40
            );
        });
    });

    extension(TypeModel type)
    {
        public bool TryGetQuery([NotNullWhen(true)] out Query? query)
        {
            query = default;

            return
                type.TryGetMetadata(out var metadata) &&
                metadata.TryGet(out query);
        }

        public bool TryGetLocatableType(DomainModel domain, [NotNullWhen(true)] out TypeModel? locatableType)
        {
            if (!type.TryGetQuery(out var query))
            {
                locatableType = default;

                return false;
            }

            locatableType = domain.Types[query.LocatableType];

            return true;
        }
    }
}