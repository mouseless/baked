using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi;
using Baked.RestApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.CodingStyle.Locatable;

public class LocatableCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Type.Add<LocatableAttribute>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.Add(new ReplaceTargetWithIdParameterConvention(), order: Order.At.Defaults);
            conventions.Add(new InitializeLocatablesConvention(), order: Order.At.Defaults);
            conventions.Add(new LookupLocatableParameterConvention(), order: Order.At.Max - 10);
            conventions.Add(new LookupLocatableParametersConvention(), order: Order.At.Max - 10);
            conventions.Add(new TargetFromLocatorConvention(), order: Order.At.Max);
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
                var locatableContext = generatedContext.Assemblies[nameof(LocatableCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<ILocatableContext>();
                locatableContext.Configure(contractResolver);

                contractResolver.SetValueProvider((property, _) =>
                {
                    property.ValueProvider = new ProxyAwareValueProvider(locatableContext.IdPropertyNames, property.PropertyName, property.ValueProvider);
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