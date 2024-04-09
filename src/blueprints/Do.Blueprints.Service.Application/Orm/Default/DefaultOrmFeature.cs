using Do.Architecture;
using Do.Business.Attributes;
using Do.Business.Default.RestApiConventions;
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

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add(typeof(QueryAttribute));
            builder.Index.Type.Add(typeof(EntityAttribute));

            builder.Conventions.AddType(
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
            model.Conventions.Add(ConventionBuilder.Property.When(
                x => x.Expect(p => p.Property.PropertyType == typeof(object)),
                x => x.CustomType(typeof(ObjectUserType))
            ));
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
            automapping.ShouldMapType.Add(t => true);
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
            });
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            var domainModel = configurator.Context.GetDomainModel();

            conventions.Add(new EntityUnderEntitiesConvention());
            conventions.Add(new LookupEntityByIdConvention(domainModel, action => action.Id != "With"));
            conventions.Add(new LookupEntitiesByIdsConvention(domainModel));
            conventions.Add(new SingleByUniqueConvention(domainModel));
            conventions.Add(new RemoveActionNameFromRouteConvention("Delete", "Update", "By"));
            conventions.Add(new AddChildToChildrenConvention());
            conventions.Add(new GetChildrenToChildrenConvention());
        });
    }
}