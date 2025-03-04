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
                        new("/specs", "pi pi-list-check") { Title = "Specs"}
                    ]
                })
                {
                    Data = new ComputedData("useNuxtRoute")
                },
                Header = new ComponentDescriptorAttribute<Header>(new()
                {
                    Sitemap = new()
                    {
                        { "/", new("/") { Icon =  "pi pi-home"}},
                        { "/specs", new("/specs") { Icon = "pi pi-list-check", Title = "Specs"} },
                        { "/specs/card-link", new("/specs/card-link") { Title = "Card Link", ParentRoute = "/specs"}},
                        { "/specs/detail-page", new("/specs/detail-page") { Title = "Detail Page", ParentRoute = "/specs"}},
                        { "/specs/header", new("/specs/header") { Title = "Header", ParentRoute = "/specs"}},
                        { "/specs/menu-page", new("/specs/menu-page") { Title = "Menu Page", ParentRoute = "/specs"}},
                        { "/specs/page-title", new("/specs/page-title") { Title = "Page Title", ParentRoute = "/specs"}},
                        { "/specs/side-menu", new("/specs/side-menu") { Title = "Side Menu", ParentRoute = "/specs"}},
                    }
                })
                {
                    Data = new ComputedData("useNuxtRoute")
                }
            }));
        });

        configurator.ConfigurePageDescriptors(pages =>
        {
            configurator.UsingDomainModel(domain =>
            {
                var route = domain.Types[typeof(RichTransientWithData)].GetActionModel().GetRoute();

                pages.Add("index", new ComponentDescriptorAttribute<MenuPage>(new()
                {
                    Links =
                    [
                        new ComponentDescriptorAttribute<CardLink>(new($"/{route.Replace("{id}", "test1")}", "Rich Transient w/ Data 1")),
                        new ComponentDescriptorAttribute<CardLink>(new($"/{route.Replace("{id}", "test2")}", "Rich Transient w/ Data 2")),
                        new ComponentDescriptorAttribute<CardLink>(new($"/{route.Replace("{id}", "test3")}", "Rich Transient w/ Data 3")),
                    ]
                }));
            });

            pages.Add("specs", new ComponentDescriptorAttribute<MenuPage>(new()
            {
                Title = "Specs",
                Description = "All UI Specs are listed here",
                Links =
                [
                    new ComponentDescriptorAttribute<CardLink>(
                        new($"/specs/card-link", "Card Link")
                        {
                            Icon = "pi pi-microchip",
                            Description = "A big card link component to render links in menu-like pages"
                        }
                    ),
                    new ComponentDescriptorAttribute<CardLink>(
                        new($"/specs/detail-page", "Detail Page")
                        {
                            Icon = "pi pi-microchip",
                            Description = "A page component suitable for rendering entities and rich transients"
                        }
                    ),
                    new ComponentDescriptorAttribute<CardLink>(
                        new($"/specs/header", "Header")
                        {
                            Icon = "pi pi-microchip",
                            Description = "A layout component that renders a breadcrumb"
                        }
                    ),
                    new ComponentDescriptorAttribute<CardLink>(
                        new($"/specs/menu-page", "Menu Page")
                        {
                            Icon = "pi pi-microchip",
                            Description = "A page component suitable for rendering navigation pages"
                        }
                    ),
                    new ComponentDescriptorAttribute<CardLink>(
                        new($"/specs/page-title", "Page Title")
                        {
                            Icon = "pi pi-microchip",
                            Description = "A page component to render page title, desc and actions"
                        }
                    ),
                    new ComponentDescriptorAttribute<CardLink>(
                        new($"/specs/side-menu", "Side Menu")
                        {
                            Icon = "pi pi-microchip",
                            Description = "A layout component to render application menu"
                        }
                    ),
                ]
            }));
        });
    }
}