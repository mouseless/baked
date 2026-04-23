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
        configurator.Runtime.ConfigureConfigurationBuilder(configuration =>
        {
            configuration.AddJsonAsDefault($$"""
            {
              "Logging": {
                "LogLevel": {
                  "NHibernate": "None",
                  "NHibernate.Sql": "{{(configurator.IsDevelopment ? "Debug" : "None")}}"
                }
              }
            }
            """);
        });

        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add(typeof(EntityAttribute));
            builder.Index.Property.Add(typeof(UniqueAttribute));
        });

        configurator.Domain.ConfigureExportConfigurations(exports =>
        {
            exports.Build("DataAccess", export =>
            {
                export.Include<EntityAttribute>();
                export.Include<IdAttribute>();
                export.Include<UniqueAttribute>();
            });
        });

        configurator.CodeGeneration.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(AutoMapOrmFeature),
                    assembly => assembly
                        .AddReferenceFrom<AutoMapOrmFeature>()
                        .AddCodes(new AutoPersistenceModelConfigurerTemplate(domain))
                        .AddCodes(new ManyToOneFetcherTemplate(domain))
                        .AddCodes(new TypeModelTypeSourceTemplate(domain)),
                    usings:
                    [
                        .. AutoPersistenceModelConfigurerTemplate.GlobalUsings,
                        .. ManyToOneFetcherTemplate.GlobalUsings,
                        .. TypeModelTypeSourceTemplate.GlobalUsings
                    ]
                );
            });
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            configurator.CodeGeneration.UsingGeneratedContext(generatedContext =>
            {
                services.AddFromAssembly(generatedContext.Assemblies[nameof(AutoMapOrmFeature)]);
            });

            services.AddScoped(typeof(IEntityContext<>), typeof(EntityContext<>));
            services.AddSingleton(typeof(IQueryContext<>), typeof(QueryContext<>));
            services.AddSingleton(typeof(ILocator<>), typeof(EntityLocator<>));
        });

        configurator.DataAccess.ConfigureFluentConfiguration(builder =>
        {
            builder.MaxFetchDepth(1);
        });

        configurator.DataAccess.ConfigureAutoPersistenceModel(model =>
        {
            configurator.CodeGeneration.UsingGeneratedContext(generatedContext =>
            {
                var featureAssembly = generatedContext.Assemblies[nameof(AutoMapOrmFeature)];
                var typeSource = featureAssembly.CreateRequiredImplementationInstance<ITypeSource>();
                var modelConfigurer = featureAssembly.CreateRequiredImplementationInstance<IAutoPersistenceModelConfigurer>();

                model.AddTypeSource(typeSource);
                modelConfigurer.Configure(model);
            });

            model.Conventions.Add(Table.Is(x => x.EntityType.Name));
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.Unique()));
            model.Conventions.Add(ConventionBuilder.Reference.Always(x => x.ForeignKey("none")));
            model.Conventions.Add(ConventionBuilder.Reference.Always(x => x.LazyLoad(Laziness.Proxy)));
            model.Conventions.Add(ConventionBuilder.Reference.Always(x => x.Index(x.EntityType, x.Name)));
        });

        configurator.DataAccess.ConfigureAutomapping(automapping =>
        {
            automapping.ShouldMapType.Add(_ => true);
            automapping.ShouldMapMember.Add(m => m.IsAutoProperty);
        });

        configurator.HttpServer.ConfigureMiddlewareCollection(middlewares =>
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

        configurator.RestApi.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            if (options.SerializerSettings.ContractResolver is not ExtendedContractResolver contractResolver) { return; }

            contractResolver.ProxyType = typeof(INHibernateProxy);
        });
    }
}