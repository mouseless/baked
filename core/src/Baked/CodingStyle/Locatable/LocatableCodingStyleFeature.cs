using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Model;
using Baked.RestApi;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.CodingStyle.Locatable;

public class LocatableCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<LocatableAttribute>();

            builder.Conventions.SetTypeAttribute(
                when: c =>
                    c.Type.Has<LocatableAttribute>() &&
                    c.Domain.Types.TryGetValue(((IModel)c.Type).Id.Pluralize(), out var query) &&
                    query.TryGetMetadata(out var queryMetadata) &&
                    !queryMetadata.Has<QueryAttribute>(),
                apply: (c, set) =>
                {
                    var queryType = c.Domain.Types[((IModel)c.Type).Id.Pluralize()];

                    set(queryType.GetMetadata(), c.Type.Apply(t => new QueryAttribute(t)));
                }
            );

            builder.Conventions.Add(new AddIdParameterToRouteConvention());
            builder.Conventions.Add(new InitializeLocatablesConvention());
            builder.Conventions.Add(new LookupLocatableParameterConvention(), order: RestApiLayer.MaxConventionOrder - 20);
            builder.Conventions.Add(new LookupLocatableParametersConvention(), order: RestApiLayer.MaxConventionOrder - 20);
            builder.Conventions.Add(new TargetFromLocatorConvention(), order: RestApiLayer.MaxConventionOrder - 10);
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(LocatableCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<LocatableCodingStyleFeature>()
                        .AddCodes(new LocatableTemplate(domain)),
                    usings: [.. LocatableTemplate.GlobalUsings]
                );
            });
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddScopedWithFactory<LocatableInitializations>();
            services.AddSingleton<InitializeLocatablesFilter>();

            configurator.UsingGeneratedContext(generatedContext =>
            {
                services.AddFromAssembly(generatedContext.Assemblies[nameof(LocatableCodingStyleFeature)]);
            });
        });

        configurator.ConfigureServiceCollection(services =>
        {
            configurator.UsingGeneratedContext(generatedContext =>
            {
                var idPropertyNames = generatedContext.Assemblies[nameof(LocatableCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<ILocatableContext>()
                    .IdPropertyNames;

                services.Configure<MvcOptions>(options =>
                {
                    options.ModelMetadataDetailsProviders.Add(new LocatableMetadataDetailsProvider(idPropertyNames));
                });
            });
        }, afterAddServices: true);

        configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            if (options.SerializerSettings.ContractResolver is not ExtendedContractResolver contractResolver) { return; }

            configurator.UsingGeneratedContext(generatedContext =>
            {
                generatedContext.Assemblies[nameof(LocatableCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<ILocatableContext>()
                    .Configure(contractResolver);
            });
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            configurator.UsingGeneratedContext(generatedContext =>
            {
                var idPropertyNames = generatedContext.Assemblies[nameof(LocatableCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<ILocatableContext>()
                    .IdPropertyNames;

                swaggerGenOptions.SchemaFilter<ReadOnlyPropertiesSchemaFilter>(idPropertyNames);
            });
        });
    }
}