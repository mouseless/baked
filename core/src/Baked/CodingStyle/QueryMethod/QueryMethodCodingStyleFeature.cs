using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;

namespace Baked.CodingStyle.QueryMethod;

public class QueryMethodCodingStyleFeature(
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
            builder.Index.Method.Add<QueryMethodAttribute>();
            builder.Index.Parameter.Add<PagingAttribute>();
            builder.Index.Parameter.Add<SortingAttribute>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetMethodAttribute(
                when: c => c.Type.Has<QueryAttribute>() && _queryMethodNames.Contains(c.Method.Name),
                attribute: () => new QueryMethodAttribute(),
                order: Order.At.Infra + 40
            );
            conventions.AddMethodAttributeConfiguration<QueryMethodAttribute>(
                when: c => c.Method.DefaultOverload.Parameters.All(p => p.IsOptional),
                attribute: qm => qm.AllParametersAreOptional = true,
                order: Order.At.Infra
            );
            conventions.AddMethodAttributeConfiguration<QueryMethodAttribute>(
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
                when: c => c.Method.Has<QueryMethodAttribute>() && _takeParameterNames.Contains(c.Parameter.Name),
                attribute: () => new PagingAttribute(PagingAttribute.Role.Take),
                order: Order.At.Infra + 40
            );

            conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethodAttribute>() && _skipParameterNames.Contains(c.Parameter.Name),
                attribute: () => new PagingAttribute(PagingAttribute.Role.Skip),
                order: Order.At.Infra + 40
            );

            conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethodAttribute>() && _sortingParameterNames.Contains(c.Parameter.Name),
                attribute: () => new SortingAttribute(),
                order: Order.At.Infra + 40
            );
        });
    }
}