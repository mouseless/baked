using Baked.Architecture;
using Baked.Business;
using Baked.RestApi;
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
                    assembly =>
                    {
                        assembly
                            .AddReferenceFrom<LocatableCodingStyleFeature>()
                            .AddCodes(new JsonConverterTemplate(domain));

                        foreach (var entity in domain.Types.Having<LocatableAttribute>())
                        {
                            entity.Apply(t => assembly.AddReferenceFrom(t));
                        }
                    },
                    usings: [.. JsonConverterTemplate.GlobalUsings]
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

        configurator.ConfigureServiceCollectionConfiguration(configuration =>
        {
            configurator.UsingGeneratedContext(generatedContext =>
            {
                configuration.Services.Configure<MvcOptions>(options =>
                {
                    var idPropertyNames = generatedContext.Assemblies[nameof(LocatableCodingStyleFeature)]
                        .CreateRequiredImplementationInstance<IContractResolverConfigurer>()
                        .IdPropertyNames;

                    options.ModelMetadataDetailsProviders.Add(new LocatableMetadataDetailsProvider(idPropertyNames));
                });
            });
        });

        configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            if (options.SerializerSettings.ContractResolver is not ExtendedContractResolver contractResolver) { return; }

            configurator.UsingGeneratedContext(generatedContext =>
            {
                generatedContext.Assemblies[nameof(LocatableCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<IContractResolverConfigurer>()
                    .Configure(contractResolver);
            });
        });
    }
}