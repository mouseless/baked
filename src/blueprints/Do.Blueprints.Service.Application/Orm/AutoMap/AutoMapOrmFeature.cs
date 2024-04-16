using Do.Architecture;
using Do.RestApi.Conventions;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Exceptions;

namespace Do.Orm.AutoMap;

public class AutoMapOrmFeature : IFeature<OrmConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddScoped(typeof(IEntityContext<>), typeof(EntityContext<>));
            services.AddSingleton(typeof(IQueryContext<>), typeof(QueryContext<>));
        });

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add(typeof(QueryAttribute));
            builder.Index.Type.Add(typeof(EntityAttribute));
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            var domainModel = configurator.Context.GetDomainModel();
            model.AddTypeSource(new TypeModelTypeSource(domainModel.Types.Having<EntityAttribute>()));

            model.Conventions.Add(Table.Is(x => x.EntityType.Name));
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.GeneratedBy.Guid()));
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.Unique()));
            model.Conventions.Add(ForeignKey.EndsWith("Id"));
            model.Conventions.Add(ConventionBuilder.Reference.Always(x => x.ForeignKey("none")));
            model.Conventions.Add(ConventionBuilder.Reference.Always(x => x.LazyLoad(Laziness.Proxy)));
            model.Conventions.Add(ConventionBuilder.Reference.Always(x => x.Index(x.EntityType, x.Name)));
        });

        configurator.ConfigureAutomapping(automapping =>
        {
            automapping.ShouldMapType.Add(_ => true);
            automapping.ShouldMapMember.Add(m => m.IsAutoProperty);
            automapping.MemberIsId.Add(m => m.PropertyType == typeof(Guid) && m.Name == "Id");
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app =>
                app.Use(async (context, next) =>
                {
                    try
                    {
                        await next(context);
                    }
                    catch (GenericADOException e)
                    {
                        context.RequestServices
                            .GetRequiredService<ILogger<AutoMapOrmFeature>>()
                            .LogError(e.InnerException, e.InnerException?.Message);

                        throw;
                    }
                })
            );

            middlewares.Add(app =>
            {
                var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
                lifetime.ApplicationStarted.Register(() =>
                {
                    // to see mapping errors on start
                    var _ = app.ApplicationServices.GetRequiredService<ISessionFactory>();
                });
            });
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            var domainModel = configurator.Context.GetDomainModel();

            conventions.Add(new LookupEntityByIdConvention(domainModel));
            conventions.Add(new LookupEntitiesByIdsConvention(domainModel));
            conventions.Add(new SingleByUniqueConvention(domainModel));
            conventions.Add(new RemoveActionNameFromRouteConvention(["By"]));
        });
    }
}