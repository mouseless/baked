using Baked.Architecture;
using Baked.Business;
using Baked.CodingStyle.LocateViaId;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.RestApi;
using Baked.RestApi.Model;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class LocateViaIdCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> LocateViaId(this CodingStyleFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Type.Add<Locatable>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.Add(new ReplaceTargetWithIdParameterConvention(), order: Order.At.Infra);
            conventions.Add(new InitializeLocatablesConvention(), order: Order.At.Infra);
            conventions.Add(new LookupLocatableParameterConvention(), order: Order.At.Max - 20);
            conventions.Add(new LookupLocatableParametersConvention(), order: Order.At.Max - 20);
            conventions.Add(new TargetFromLocatorConvention(), order: Order.At.Max - 10);
        });

        configurator.Domain.ConfigureExportConfigurations(exports =>
        {
            exports.Build("RestApi", export => export
                .Include<ApiController>()
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
                generatedAssemblies.Add(nameof(LocateViaIdCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<CodingStyleFeature.Configurator>()
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
                services.AddFromAssembly(generatedContext.Assemblies[nameof(LocateViaIdCodingStyleFeature)]);

                var idPropertyNames = generatedContext.Assemblies[nameof(LocateViaIdCodingStyleFeature)]
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
                var locatableContext = generatedContext.Assemblies[nameof(LocateViaIdCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<ILocatableContext>();
                locatableContext.Configure(contractResolver);

                contractResolver.SetValueProvider((property, _) =>
                {
                    property.ValueProvider = new ProxyAwareValueProvider(contractResolver, locatableContext.IdPropertyNames, property.PropertyName, property.ValueProvider);
                });
            });
        });

        configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            configurator.Buildtime.UsingGeneratedContext(generatedContext =>
            {
                var idPropertyNames = generatedContext.Assemblies[nameof(LocateViaIdCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<ILocatableContext>()
                    .IdPropertyNames;

                swaggerGenOptions.SchemaFilter<ReadOnlyPropertiesSchemaFilter>(idPropertyNames);
            });
        });
    });

    extension(Locatable locatable)
    {
        public ApiParameter AddLocatorAsService(ApiAction action, TypeModel locatableType) =>
            action.Parameter[$"{locatableType.Name.Camelize()}Locator"] = new($"{locatableType.Name.Camelize()}Locator", locatable.RenderLocatorType(locatableType.CSharpFriendlyFullName), ParameterModelFrom.Services)
            {
                IsInvokeMethodParameter = false
            };

        public string BuildLocate(ApiParameter locatorServiceParameter, string parameter,
            string? notNullParameterExpression = default,
            bool nullable = false
        )
        {
            notNullParameterExpression ??= parameter;

            var locate = locatable.RenderLocate(locatorServiceParameter.Name, notNullParameterExpression);
            if (nullable)
            {
                locate = $"({parameter} != null ? {locate} : null)";
            }

            return locate;
        }

        public string BuildLocateMany(ApiParameter locatorServiceParameter, string parameter,
            bool isArray = false
        )
        {
            var locateMany = locatable.RenderLocateMany(locatorServiceParameter.Name, parameter);

            return isArray ? $"({locateMany}).ToArray()" : $"({locateMany}).ToList()";
        }
    }

    extension(IDomainModelConventionCollection conventions)
    {
        public void AddLocateAction<TLocatable>() =>
            conventions.Add(new AddLocateActionConvention<TLocatable>(), order: Order.At.Infra.Max - 20);
    }

    extension(ApiParameter parameter)
    {
        public void ConvertToId(IdInfo idInfo,
            string? name = default,
            bool dontAddRequired = false,
            bool nullable = false
        )
        {
            name ??= $"{parameter.Name}{idInfo.PropertyName}";

            if (!nullable && dontAddRequired)
            {
                parameter.AddRequiredAttributes(isValueType: true);
            }

            parameter.Type = nullable ? $"{idInfo.Type}?" : idInfo.Type;
            parameter.Name = name;
        }

        public void ConvertToIds(IdInfo idInfo)
        {
            parameter.Type = $"IEnumerable<{idInfo.Type}>";
            parameter.Name = $"{parameter.Name.Singularize()}{idInfo.PropertyName.Pluralize()}";
        }
    }

    extension(TypeModel type)
    {
        public bool TryGetLocatable([NotNullWhen(true)] out Locatable? locatable)
        {
            locatable = default;

            return
                type.TryGetMetadata(out var metadata) &&
                metadata.TryGet(out locatable);
        }

        public bool TryGetQueryType(DomainModel domain, [NotNullWhen(true)] out TypeModel? queryType)
        {
            if (!type.TryGetLocatable(out var locatable) || locatable.QueryType is null)
            {
                queryType = default;

                return false;
            }

            queryType = domain.Types[locatable.QueryType];

            return true;
        }
    }
}