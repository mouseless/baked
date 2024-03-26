using Do.Architecture;
using Do.Domain.Configuration;
using Do.Domain.Model;
using Do.Orm.Default.UserTypes;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Exceptions;

namespace Do.Orm.Default;

public class DefaultOrmFeature : IFeature<OrmConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddScoped(typeof(IEntityContext<>), typeof(EntityContext<>));
            services.AddSingleton(typeof(IQueryContext<>), typeof(QueryContext<>));
        });

        configurator.ConfigureDomainIndexers(indexers =>
        {
            indexers.AddAttributeIndexer<QueryAttribute>();
            indexers.AddAttributeIndexer<EntityAttribute>();
        });

        configurator.ConfigureDomainMetaData(metadata =>
        {
            metadata
                .Type
                    .Add(
                        add: (query, adder) =>
                        {
                            var queryContext = query.Constructor?.GetParameters().First(p => p.ParameterType.Name.StartsWith("IQueryContext")).ParameterType ??
                                throw new("Parameter model not found!");

                            var entity = queryContext.GenericTypeArguments.First();

                            adder.Add(query, entity.Create(t => new QueryAttribute(t)));
                            adder.Add(entity, query.Create(t => new EntityAttribute(t)));
                        },
                        when: type => type.Constructor is not null && type.Constructor.HasParameter(p => p.ParameterType.Name.StartsWith("IQueryContext"))
                    );
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            var domainModel = configurator.Context.GetDomainModel();

            var typeSource = new TypeSource();
            domainModel.Types.Having<EntityAttribute>().Apply(t => typeSource.Add(t));

            model.AddTypeSource(typeSource);

            model
                .Conventions.Add(Table.Is(x => x.EntityType.Name))
                .Conventions.Add(ConventionBuilder.Id.Always(x => x.GeneratedBy.Guid()))
                .Conventions.Add(ConventionBuilder.Id.Always(x => x.Unique()))
                .Conventions.Add(ForeignKey.EndsWith("Id"))
                .Conventions.Add(ConventionBuilder.Reference.Always(x => x.ForeignKey("none")))
                .Conventions.Add(ConventionBuilder.Reference.Always(x => x.LazyLoad(Laziness.Proxy)))
                .Conventions.Add(ConventionBuilder.Reference.Always(x => x.Index(x.EntityType, x.Name)))
                .Conventions.Add(ConventionBuilder.Property.When(
                    x => x.Expect(p => p.Property.PropertyType == typeof(string) && p.Property.Name.EndsWith("Data")),
                    x => x.CustomSqlType("TEXT")
                ))
                .Conventions.Add(ConventionBuilder.Property.When(
                    x => x.Expect(p => p.Property.PropertyType == typeof(object)),
                    x => x.CustomType(typeof(ObjectUserType))
                ))
            ;
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

        configurator.ConfigureAutomapping(automapping =>
        {
            var domainModel = configurator.Context.GetDomainModel();
            var mappedTypes = domainModel.Types.Having<EntityAttribute>();

            automapping.ShouldMapType.Add(t => mappedTypes.Contains(t));
            automapping.MemberIsId.Add(m => m.PropertyType == typeof(Guid) && m.Name == "Id");
            automapping.ShouldMapMember.Add(m => m.IsAutoProperty && !m.PropertyType.IsAssignableTo(typeof(ICollection)));
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
                            .GetRequiredService<ILogger<DefaultOrmFeature>>()
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
                }
            );
        });
    }
}
