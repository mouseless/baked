using Do.Architecture;
using Do.Orm.Default.UserTypes;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate;

namespace Do.Orm.Default;

public class DefaultOrmFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddScoped(typeof(IEntityContext<>), typeof(EntityContext<>));
            services.AddSingleton(typeof(IQueryContext<>), typeof(QueryContext<>));
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
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
            automapping.ShouldMapType.Add(t =>
                t.IsClass && !t.IsAbstract &&
                t.GetConstructors().Any(c => c
                    .GetParameters().Any(p => p
                        .ParameterType.IsAssignableTo(typeof(IEntityContext<>).MakeGenericType(t))
                    )
                )
            );

            automapping.MemberIsId.Add(m => m.PropertyType == typeof(Guid));
            automapping.ShouldMapMember.Add(m => m.IsAutoProperty && !m.PropertyType.IsAssignableTo(typeof(ICollection)));
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
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

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.OperationFilter<NullTypesAreObjectOperationFilter>();
        });
    }
}
