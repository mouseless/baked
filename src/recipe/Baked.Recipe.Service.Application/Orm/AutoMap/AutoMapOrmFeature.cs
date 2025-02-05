using Baked.Architecture;
using Baked.RestApi;
using Baked.RestApi.Conventions;
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
            builder.Index.Type.Add(typeof(QueryAttribute));
            builder.Index.Type.Add(typeof(EntityAttribute));
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(AutoMapOrmFeature),
                assembly =>
                {
                    assembly
                        .AddReferenceFrom<AutoMapOrmFeature>()
                        .AddCodes(new ManyToOneFetcherTemplate(domain))
                        .AddCodes(new TypeModelTypeSourceTemplate(domain));

                    foreach (var entity in domain.Types.Having<EntityAttribute>())
                    {
                        entity.Apply(t => assembly.AddReferenceFrom(t));
                    }
                },
                usings: [
                    "Baked.Orm",
                    "Baked.Runtime",
                    "FluentNHibernate",
                    "FluentNHibernate.Diagnostics",
                    "Microsoft.Extensions.DependencyInjection",
                    "NHibernate.Linq"
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
        });

        configurator.ConfigureFluentConfiguration(builder =>
        {
            builder.MaxFetchDepth(1);
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            configurator.UsingGeneratedContext(generatedContext =>
            {
                var assembly = generatedContext.Assemblies[nameof(AutoMapOrmFeature)];

                var typeSource = assembly.GetExportedTypes().SingleOrDefault(t => t.IsAssignableTo(typeof(ITypeSource))) ?? throw new("`ITypeSource` implementation not found");
                var typeSourceInstance = (ITypeSource?)Activator.CreateInstance(typeSource) ?? throw new($"Cannot create instance of {typeSource}");

                model.AddTypeSource(typeSourceInstance);
            });

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

        configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            if (options.SerializerSettings.ContractResolver is null) { return; }

            options.SerializerSettings.ContractResolver = new ProxyAwareContractResolver<INHibernateProxy>(options.SerializerSettings.ContractResolver);
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            configurator.UsingDomainModel(domain =>
            {
                conventions.Add(new AutoHttpMethodConvention([(Regexes.StartsWithFirstBySingleByOrBy, HttpMethod.Get)]), order: -10);
                conventions.Add(new LookupEntityByIdConvention(domain));
                conventions.Add(new LookupEntitiesByIdsConvention(domain));
                conventions.Add(new RemoveFromRouteConvention(["FirstBy", "SingleBy", "By"],
                    _when: c => c.Controller.MappedType.TryGetMetadata(out var metadata) && metadata.Has<QueryAttribute>()
                ));
            });
        });
    }
}