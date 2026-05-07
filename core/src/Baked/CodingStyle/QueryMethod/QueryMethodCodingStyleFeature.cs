using Baked.Architecture;
using Baked.Business;

namespace Baked.CodingStyle.QueryMethod;

public class QueryMethodCodingStyleFeature(
    HashSet<string> _queryMethodNames,
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
                requiresIndex: true
            );

            builder.Conventions.AddMethodAttributeConfiguration<QueryMethodAttribute>(
                when: c => c.Method.DefaultOverload.Parameters.All(p => p.IsOptional),
                attribute: qm => qm.AllParametersAreOptional = true
            );

            builder.Conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethodAttribute>() && _takeParameterNames.Contains(c.Parameter.Name),
                attribute: p => new PagingAttribute(PagingAttribute.Role.Take),
                requiresIndex: true,
                order: 35
            );

            builder.Conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethodAttribute>() && _skipParameterNames.Contains(c.Parameter.Name),
                attribute: p => new PagingAttribute(PagingAttribute.Role.Skip),
                requiresIndex: true,
                order: 35
            );

            builder.Conventions.SetParameterAttribute(
                when: c => c.Method.Has<QueryMethodAttribute>() && _sortingParameterNames.Contains(c.Parameter.Name),
                attribute: () => new SortingAttribute(),
                requiresIndex: true,
                order: 35
            );
        });
    }
}