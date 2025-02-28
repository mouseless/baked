using Baked.Architecture;
using Baked.ExceptionHandling;
using Baked.RestApi.Model;
using Baked.Test.Authentication;
using Baked.Test.Business;
using Baked.Test.CodingStyle.RichTransient;
using Baked.Test.ExceptionHandling;
using Baked.Test.Orm;
using Baked.Theme.Admin;
using Baked.Ui;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace Baked.Test.ConfigurationOverrider;

public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddSingleById<Entities>();
            builder.Conventions.AddSingleById<Parents>();
            builder.Conventions.AddSingleById<Children>();
            builder.Conventions.AddConfigureAction<AuthenticationSamples>(nameof(AuthenticationSamples.FormPostAuthenticate), useForm: true);
            builder.Conventions.AddConfigureAction<DocumentationSamples>(nameof(DocumentationSamples.Route), parameter: p =>
            {
                p["route"].From = ParameterModelFrom.Route;
                p["route"].RoutePosition = 2;
            });
            builder.Conventions.AddConfigureAction<ExceptionSamples>(nameof(ExceptionSamples.Throw), parameter: p => p["handled"].From = ParameterModelFrom.Query);

            builder.Conventions.AddOverrideAction<OverrideSamples>(nameof(OverrideSamples.UpdateRoute),
                routeParts: ["override-samples", "override", "update-route"],
                method: HttpMethod.Post
            );
            builder.Conventions.AddOverrideAction<OverrideSamples>(nameof(OverrideSamples.Parameter),
                parameter: parameter =>
                {
                    parameter["parameter"].Name = "id";
                    parameter["parameter"].From = ParameterModelFrom.Route;
                    parameter["parameter"].RoutePosition = 2;
                }
            );
            builder.Conventions.AddOverrideAction<OverrideSamples>(nameof(OverrideSamples.RequestClass),
                useRequestClassForBody: false
            );
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<IExceptionHandler, ClientExceptionHandler>();
            services.AddSingleton<IExceptionHandler, SampleExceptionHandler>();
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Override<Entity>(x => x.Map(e => e.String).Length(500));
            model.Override<Entity>(x => x.Map(e => e.Unique).Column("UniqueString").Unique());
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.SwaggerDoc("samples", new() { Title = "Samples", Version = "v1" });
            swaggerGenOptions.SwaggerDoc("external", new() { Title = "External", Version = "v1" });

            swaggerGenOptions.DocInclusionPredicate((documentName, api) =>
                documentName == "samples" ||
                documentName == "external" && api.GroupName == "ExternalSamples"
            );

            swaggerGenOptions.AddSecurityDefinition("Custom",
                new()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "X-Custom-API-Key",
                    Description = "Demonstration of additional security definitions",
                },
                documentName: "external"
            );
            swaggerGenOptions.AddSecurityDefinition("Custom.Secret",
                new()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "X-Custom-API-Secret",
                    Description = "Demonstration of adding two requirements",
                },
                documentName: "external"
            );

            swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<AuthorizeAttribute>(["Custom", "Custom.Secret"], documentName: "external");
            swaggerGenOptions.AddParameterToOperationsThatUse<AuthorizeAttribute>(new() { Name = "X-Security-2", In = ParameterLocation.Header }, documentName: "external");
            swaggerGenOptions.AddParameterToOperationsThatUse<AuthorizeAttribute>(new() { Name = "X-Security-1", In = ParameterLocation.Header }, position: 0, documentName: "external");
        });

        configurator.ConfigureSwaggerUIOptions(swaggerUIOptions =>
        {
            swaggerUIOptions.SwaggerEndpoint($"samples/swagger.json", "Samples");
            swaggerUIOptions.SwaggerEndpoint($"external/swagger.json", "External");
        });

        configurator.ConfigureLayoutDescriptors(layouts =>
        {
            layouts.Add("default", new ComponentDescriptorAttribute<DefaultLayout>(new()
            {
                SideMenu = new ComponentDescriptorAttribute<SideMenu>(new()
                {
                    Menu =
                    [
                        new("/", "pi pi-home"),
                        new("/specs/detail", "pi pi-list-check") { Title = "Detail"}
                    ]
                })
                {
                    Data = new ComputedData("useRoute")
                },
                Header = new ComponentDescriptorAttribute<Header>(new()
                {
                    Sitemap = new()
                    {
                        { "/", new("/") { Icon =  "pi pi-home"}},
                        { "/specs", new("/specs") { Icon = "pi pi-list-check", Title = "Specs"} },
                        { "/specs/detail", new("/specs/detail") { Title = "Detail", ParentRoute = "/specs"}},
                    }
                })
                {
                    Data = new ComputedData("useRoute")
                }
            }));
        });

        configurator.ConfigurePageDescriptors(pages =>
        {
            configurator.UsingDomainModel(domain =>
            {
                var route = domain.Types[typeof(RichTransientWithData)].GetActionModel().GetRoute();

                pages.Add("index", new ComponentDescriptorAttribute<Detail>(new("Dashboard")
                {
                    Header = Components.Menu(new object[]
                    {
                        new
                        {
                            Label = "Rich Transients Menu",
                            Items = new object[]
                            {
                                new { Label = "Rich Transient w/ Data 1", Route = $"/{route.Replace("{id}", "test1")}" },
                                new { Label = "Rich Transient w/ Data 2", Route = $"/{route.Replace("{id}", "test2")}" },
                                new { Label = "Rich Transient w/ Data 3", Route = $"/{route.Replace("{id}", "test3")}" },
                            }
                        }
                    })
                }));
            });
        });
    }
}