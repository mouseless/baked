using Baked.Architecture;
using Baked.Business;
using Baked.RestApi;
using Baked.RestApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.CodingStyle.Locatable;

public class LocatableCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<LocatableAttribute>();

            builder.Conventions.Add(new ReplaceTargetWithIdParameterConvention());
            builder.Conventions.Add(new InitializeLocatablesConvention());
            builder.Conventions.Add(new LookupLocatableParameterConvention(), order: RestApiLayer.MaxConventionOrder - 20);
            builder.Conventions.Add(new LookupLocatableParametersConvention(), order: RestApiLayer.MaxConventionOrder - 20);
            builder.Conventions.Add(new TargetFromLocatorConvention(), order: RestApiLayer.MaxConventionOrder - 10);
        });

        configurator.Domain.ConfigureExportConfigurations(exports =>
        {
            exports.Build("RestApi", export => export
                .Include<ControllerModelAttribute>()
                .AddProperty(controller =>
                {
                    string? value = null;
                    if (controller.Action.TryGetValue("Locate", out var locate))
                    {
                        value = $"{locate.Method} /{locate.GetRoute()}";
                    }

                    return new("locate-route", Value: value);
                })
            );
        });

        configurator.Buildtime.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(LocatableCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<LocatableCodingStyleFeature>()
                        .AddCodes(new LocatableTemplate(domain)),
                    usings: [.. LocatableTemplate.GlobalUsings]
                );
            });
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddScopedWithFactory<LocatableInitializations>();
            services.AddSingleton<InitializeLocatablesFilter>();

            configurator.Buildtime.UsingGeneratedContext(generatedContext =>
            {
                services.AddFromAssembly(generatedContext.Assemblies[nameof(LocatableCodingStyleFeature)]);

                var idPropertyNames = generatedContext.Assemblies[nameof(LocatableCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<ILocatableContext>()
                    .IdPropertyNames;
                services.PostConfigure<MvcOptions>(options =>
                {
                    options.ModelMetadataDetailsProviders.Add(new LocatableMetadataDetailsProvider(idPropertyNames));
                });
            });
        });

        configurator.RestApi.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            if (options.SerializerSettings.ContractResolver is not ExtendedContractResolver contractResolver) { return; }

            configurator.Buildtime.UsingGeneratedContext(generatedContext =>
            {
                generatedContext.Assemblies[nameof(LocatableCodingStyleFeature)]
                var locatableContext = generatedContext.Assemblies[nameof(LocatableCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<ILocatableContext>();
                locatableContext.Configure(contractResolver);

                contractResolver.SetValueProvider((property, serviceProvider) =>
                {
                    property.ValueProvider = new ProxyAwareValueProvider(locatableContext.IdPropertyNames)
                        .With(property.PropertyName, property.ValueProvider);
                });
            });
        });

        configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            configurator.Buildtime.UsingGeneratedContext(generatedContext =>
            {
                var idPropertyNames = generatedContext.Assemblies[nameof(LocatableCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<ILocatableContext>()
                    .IdPropertyNames;

                swaggerGenOptions.SchemaFilter<ReadOnlyPropertiesSchemaFilter>(idPropertyNames);
            });
        });
    }
}