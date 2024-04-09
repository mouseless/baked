using Do.Architecture;
using Do.Business;
using Do.Orm;

namespace Do.CodingStyle.RichEntity;

public class RichEntityCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(
                apply: (query, add) =>
                {
                    var parameter =
                        query.GetMembers()
                            .Constructors
                            .SelectMany(o => o.Parameters)
                            .First(p => p.ParameterType.IsAssignableTo(typeof(IQueryContext<>)));

                    var entity = parameter.ParameterType.GetGenerics().GenericTypeArguments.First().Model;
                    Type? queryContext = null;
                    entity.Apply(t => queryContext = typeof(IQueryContext<>).MakeGenericType(t));
                    if (queryContext is null) { return; }

                    entity.Apply(t =>
                        add(query, new QueryAttribute(t))
                    );
                    query.Apply(t =>
                        add(entity.GetMetadata(), new EntityAttribute(t, queryContext))
                    );
                    add(entity.GetMetadata(), new ApiInputAttribute());
                },
                when: type =>
                    type.TryGetMembers(out var members) &&
                    members.Constructors.Any(o => o.Parameters.Any(p => p.ParameterType.IsAssignableTo(typeof(IQueryContext<>))))
            );
        });

        configurator.ConfigureNHibernateInterceptor(interceptor =>
        {
            interceptor.Instantiator = (ctx, id) =>
            {
                var result = ctx.ApplicationServices.GetRequiredServiceUsingRequestServices(ctx.MetaData.MappedClass);

                ctx.MetaData.SetIdentifier(result, id);

                return result;
            };
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            var domainModel = configurator.Context.GetDomainModel();

            conventions.Add(new EntityUnderEntitiesConvention());
            conventions.Add(new TargetEntityFromRouteConvention(domainModel, action => action.Id != "With"));
        });
    }
}