using Baked.Architecture;
using Baked.ExceptionHandling;
using Baked.RestApi.Model;
using Baked.Test.Authentication;
using Baked.Test.Business;
using Baked.Test.Core;
using Baked.Test.ExceptionHandling;
using Baked.Test.Orm;
using Baked.Theme.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using static Baked.Theme.Admin.Components;
using static Baked.Ui.Datas;

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
            services.AddHostedService<SeedDataTrigger>();
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

        configurator.ConfigureAppDescriptor(app =>
        {
            app.Error = ErrorPage(
                safeLinks:
                [
                    CardLink("/", "Home", icon: "pi pi-home"),
                    CardLink("/specs", "Specs", icon: "pi pi-list-check"),
                ],
                errorInfos: new()
                {
                    { 403, new("Access Denied", "You do not have the permision to view the address or data specified." ) },
                    { 404, new("Page Not Found", "The page you want to view is etiher deleted or outdated.") },
                    { 500, new("Unexpected Error", "Please contact system administrator.") }
                },
                data: Computed(Composables.UseError)
            );
        });

        configurator.ConfigureLayoutDescriptors(layouts =>
        {
            configurator.UsingDomainModel(domain =>
            {
                layouts.Add(DefaultLayout("default",
                    sideMenu: SideMenu(
                        menu:
                        [
                            SideMenuItem("/", "pi pi-home"),
                            SideMenuItem("/specs", "pi pi-list-check", title: "Specs")
                        ],
                        footer: String(Inline("FT"))
                    ),
                    header: Header(
                        siteMap:
                        [
                            HeaderItem("/", icon: "pi pi-home"),
                            HeaderItem("/specs", icon: "pi pi-list-check", title: "Specs"),
                            HeaderItem("/specs/card-link", title: "Card Link", parentRoute: "/specs"),
                            HeaderItem("/specs/custom-css", title: "Custom CSS", parentRoute: "/specs"),
                            HeaderItem("/specs/data-panel", title: "Data Panel", parentRoute: "/specs"),
                            HeaderItem("/specs/data-table", title: "Data Table", parentRoute: "/specs"),
                            HeaderItem("/specs/detail-page", title: "Detail Page", parentRoute: "/specs"),
                            HeaderItem("/specs/error-handling", title: "Error Handling", parentRoute: "/specs"),
                            HeaderItem("/specs/error-page", title: "Error Page", parentRoute: "/specs"),
                            HeaderItem("/specs/header", title: "Header", parentRoute: "/specs"),
                            HeaderItem("/specs/icon", title: "Icon", parentRoute: "/specs"),
                            HeaderItem("/specs/locale", title: "Locale", parentRoute: "/specs"),
                            HeaderItem("/specs/menu-page", title: "Menu Page", parentRoute: "/specs"),
                            HeaderItem("/specs/money", title: "Money", parentRoute: "/specs"),
                            HeaderItem("/specs/page-title", title: "Page Title", parentRoute: "/specs"),
                            HeaderItem("/specs/rate", title: "Rate", parentRoute: "/specs"),
                            HeaderItem("/specs/report-page", title: "Report Page", parentRoute: "/specs"),
                            HeaderItem("/specs/side-menu", title: "Side Menu", parentRoute: "/specs"),
                            HeaderItem("/specs/toast", title: "Toast", parentRoute: "/specs")
                        ]
                    )
                ));
            });
        });

        configurator.ConfigurePageDescriptors(pages =>
        {
            var headers = Inline(new { Authorization = "token-admin-ui" });

            configurator.UsingDomainModel(domain =>
            {
                var timeProviderSamples = domain.Types[typeof(TimeProviderSamples)].GetMembers();
                var now = timeProviderSamples.Methods[nameof(TimeProviderSamples.GetNow)].GetSingle<ActionModelAttribute>().GetRoute();
                var localNow = timeProviderSamples.GetMembers().Methods[nameof(TimeProviderSamples.GetLocalNow)].GetSingle<ActionModelAttribute>().GetRoute();
                var utcNow = timeProviderSamples.GetMembers().Methods[nameof(TimeProviderSamples.GetUtcNow)].GetSingle<ActionModelAttribute>().GetRoute();

                var entities = domain.Types[typeof(Entities)].GetMembers().Methods[nameof(Entities.By)].GetSingle<ActionModelAttribute>().GetRoute();
                var parents = domain.Types[typeof(Parents)].GetMembers().Methods[nameof(Parents.By)].GetSingle<ActionModelAttribute>().GetRoute();

                pages.Add(ReportPage("index",
                    title: PageTitle("Sample Report", description: "Showcases a  report layout with tabs and data panels"),
                    tabs:
                    [
                        ReportPageTab("time", "Time",
                            icon: Icon("pi-clock"),
                            contents:
                            [
                                ReportPageTabContent(
                                    component: DataPanel("Server time",
                                        content: String(Remote($"/{now}", headers: headers)),
                                        collapsed: false
                                    )
                                ),
                                ReportPageTabContent(
                                    component: DataPanel("Server time (local)",
                                        content: String(Remote($"/{localNow}", headers: headers)),
                                        collapsed: true
                                    ),
                                    narrow: true
                                ),
                                ReportPageTabContent(
                                    component: DataPanel("Server time (utc)",
                                        content: String(Remote($"/{utcNow}", headers: headers)),
                                        collapsed: true
                                    ),
                                    narrow: true
                                )
                            ]
                        ),
                        ReportPageTab("from-db", "From DB",
                            icon: Icon("pi-database"),
                            contents:
                            [
                                ReportPageTabContent(
                                    component: DataPanel("Entities",
                                        content: DataTable(
                                            columns:
                                            [
                                                DataTableColumn("id", "ID", minWidth: true),
                                                DataTableColumn("unique", "Unique"),
                                                DataTableColumn("dateTime", "DateTime")
                                            ],
                                            data: Remote($"/{entities}", headers: headers)
                                        )
                                    )
                                ),
                                ReportPageTabContent(
                                    component: DataPanel("Parents",
                                        content: DataTable(
                                            columns:
                                            [
                                                DataTableColumn("id", "ID", minWidth: true),
                                                DataTableColumn("name", "Name")
                                            ],
                                            data: Remote($"/{parents}", headers: headers)
                                        ),
                                        collapsed: true
                                    )
                                )
                            ]
                        )
                    ]
                ));
            });

            pages.Add(MenuPage("specs",
                header: PageTitle(
                  title: "Specs",
                  description: "All UI Specs are listed here"
                ),
                links:
                [
                    CardLink("/specs/card-link", "Card Link",
                        icon: "pi pi-microchip",
                        description: "A big card link component to render links in menu-like pages"
                    ),
                    CardLink("/specs/custom-css", "Custom CSS",
                        icon: "pi pi-microchip",
                        description: "Allow custom configuration to define custom css and more"
                    ),
                    CardLink("/specs/data-panel", "Data Panel",
                        icon: "pi pi-microchip",
                        description: "A page component to lazy load and view a data within a panel"
                    ),
                    CardLink("/specs/data-table", "Data Table",
                        icon: "pi pi-microchip",
                        description: "A page component to view list data in a table"
                    ),
                    CardLink("/specs/detail-page", "Detail Page",
                        icon: "pi pi-microchip",
                        description: "A page component suitable for rendering entities and rich transients"
                    ),
                    CardLink("/specs/error-handling", "Error Handling",
                        icon: "pi pi-microchip",
                        description: "A plugin for handling errors"
                    ),
                    CardLink("/specs/error-page", "Error Page",
                        icon: "pi pi-microchip",
                        description: "A page component to display errors in full page"
                    ),
                    CardLink("/specs/header", "Header",
                        icon: "pi pi-microchip",
                        description: "A layout component that renders a breadcrumb"
                    ),
                    CardLink("/specs/icon", "Icon",
                        icon: "pi pi-microchip",
                        description: "A page component that displays built-in icons"
                    ),
                    CardLink("/specs/locale", "Locale",
                        icon: "pi pi-microchip",
                        description: "Allow locale customization and language support"
                    ),
                    CardLink("/specs/menu-page", "Menu Page",
                        icon: "pi pi-microchip",
                        description: "A page component suitable for rendering navigation pages"
                    ),
                    CardLink("/specs/money", "Money",
                        icon: "pi pi-microchip",
                        description: "A page component to render money values"
                    ),
                    CardLink("/specs/page-title", "Page Title",
                        icon: "pi pi-microchip",
                        description: "A page component to render page title, desc and actions"
                    ),
                    CardLink("/specs/rate", "Rate",
                        icon: "pi pi-microchip",
                        description: "A page component to render rate values as percentage"
                    ),
                    CardLink("/specs/report-page", "Report Page",
                        icon: "pi pi-microchip",
                        description: "A page component to render report pages"
                    ),
                    CardLink("/specs/side-menu", "Side Menu",
                        icon: "pi pi-microchip",
                        description: "A layout component to render application menu"
                    ),
                    CardLink("/specs/toast", "Toast",
                        icon: "pi pi-microchip",
                        description: "A behavioral component to render alert messages"
                    )
                ]
            ));
        });
    }
}