using Baked.Architecture;
using Baked.Business;
using Baked.RestApi;
using FluentNHibernate;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Exceptions;
using NHibernate.Proxy;

namespace Baked.Orm.AutoMap;

public class AutoMapOrmFeature : IFeature<OrmConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureConfigurationBuilder(configuration =>
        {
            configuration.AddJsonAsDefault($$"""
            {
              "Logging": {
                "LogLevel": {
                  "NHibernate": "None",
                  "NHibernate.Sql": "{{(configurator.IsDevelopment() ? "Debug" : "None")}}"
                }
              }
            }
            """);
        });

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add(typeof(EntityAttribute));
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(AutoMapOrmFeature),
                    assembly => assembly
                        .AddReferenceFrom<AutoMapOrmFeature>()
                        .AddCodes(new ManyToOneFetcherTemplate(domain))
                        .AddCodes(new TypeModelTypeSourceTemplate(domain)),
                    usings:
                    [
                        .. ManyToOneFetcherTemplate.GlobalUsings,
                        .. TypeModelTypeSourceTemplate.GlobalUsings
                    ]
                );
            });
        });

        configurator.ConfigureServiceCollection(services =>
        {
            configurator.UsingGeneratedContext(generatedContext =>
            {
                services.AddFromAssembly(generatedContext.Assemblies[nameof(AutoMapOrmFeature)]);
            });

            services.AddScoped(typeof(IEntityContext<>), typeof(EntityContext<>));
            services.AddSingleton(typeof(IQueryContext<>), typeof(QueryContext<>));
            services.AddSingleton(typeof(ILocator<>), typeof(EntityLocator<>));
        });

        configurator.ConfigureFluentConfiguration(builder =>
        {
            builder.MaxFetchDepth(1);
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            configurator.UsingGeneratedContext(generatedContext =>
            {
                model.AddTypeSource(generatedContext.Assemblies[nameof(AutoMapOrmFeature)].CreateRequiredImplementationInstance<ITypeSource>());
            });

            model.Conventions.Add(Table.Is(x => x.EntityType.Name));
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.Unique()));
            model.Conventions.Add(ConventionBuilder.Reference.Always(x => x.ForeignKey("none")));
            model.Conventions.Add(ConventionBuilder.Reference.Always(x => x.LazyLoad(Laziness.Proxy)));
            model.Conventions.Add(ConventionBuilder.Reference.Always(x => x.Index(x.EntityType, x.Name)));
        });

        configurator.ConfigureAutomapping(automapping =>
        {
            automapping.ShouldMapType.Add(_ => true);
            automapping.ShouldMapMember.Add(m => m.IsAutoProperty);
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

        configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            if (options.SerializerSettings.ContractResolver is not ExtendedContractResolver contractResolver) { return; }

            contractResolver.ProxyType = typeof(INHibernateProxy);
        });
    }
}