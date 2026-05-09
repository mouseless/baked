using Baked.Architecture;
using Baked.Business;

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
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Method.Add<QueryMethodAttribute>();
            builder.Index.Parameter.Add<PagingAttribute>();
            builder.Index.Parameter.Add<SortingAttribute>();

            builder.Conventions.SetMethodAttribute(
                when: c => c.Type.Has<QueryAttribute>() && _queryMethodNames.Contains(c.Method.Name),
                attribute: () => new QueryMethodAttribute(),
                order: 40
            );
            builder.Conventions.AddMethodAttributeConfiguration<QueryMethodAttribute>(
                when: c => c.Method.DefaultOverload.Parameters.All(p => p.IsOptional),
                attribute: qm => qm.AllParametersAreOptional = true
            );
            builder.Conventions.AddMethodAttributeConfiguration<QueryMethodAttribute>(
                when: c => c.Method.DefaultOverload.Parameters.Any(p => _primaryParameterNames.Contains(p.Name)),
                attribute: (qm, c) =>
                {
                    var primaryParameter =
                        c.Method.DefaultOverload.Parameters.FirstOrDefault(p => _primaryParameterNames.Contains(p.Name)) ??
                        throw DiagnosticCode.InvalidState.Exception(
                            $"{c.Type.Name}.{c.Method.Name} is expected to contain a parameter with name that matches one of ({_primaryParameterNames.Join(", ")})"
                        );

                    qm.PrimaryParameterName = primaryParameter.Name;
                }
            );

            builder.Conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethodAttribute>() && _takeParameterNames.Contains(c.Parameter.Name),
                attribute: () => new PagingAttribute(PagingAttribute.Role.Take),
                order: 40
            );

            builder.Conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethodAttribute>() && _skipParameterNames.Contains(c.Parameter.Name),
                attribute: () => new PagingAttribute(PagingAttribute.Role.Skip),
                order: 40
            );

            builder.Conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethodAttribute>() && _sortingParameterNames.Contains(c.Parameter.Name),
                attribute: () => new SortingAttribute(),
                order: 40
            );
        });
    }
}