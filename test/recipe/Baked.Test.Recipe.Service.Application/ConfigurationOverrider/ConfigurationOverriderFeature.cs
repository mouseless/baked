using Baked.Architecture;
using Baked.ExceptionHandling;
using Baked.RestApi.Model;
using Baked.Test.Authentication;
using Baked.Test.Business;
using Baked.Test.Core;
using Baked.Test.ExceptionHandling;
using Baked.Test.Orm;
using Baked.Theme.Admin;
using Baked.Ui;
using Humanizer;
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

        var specs = new[]
        {
            new { Title = "Auth", Description = "A plugin for authorized routing and client" },
            new { Title = "Card Link", Description = "A component that renders a link as a big card-like button" },
            new { Title = "Custom CSS", Description = "Allow custom configuration to define custom css and more" },
            new { Title = "Data Panel", Description = "A component to lazy load and view a data within a panel" },
            new { Title = "Data Table", Description = "A component to view list data in a table" },
            new { Title = "Detail Page", Description = "A page component suitable for rendering entities and rich transients" },
            new { Title = "Error Handling", Description = "A plugin for handling errors" },
            new { Title = "Error Page", Description = "A page component to display errors in full page" },
            new { Title = "Header", Description = "A layout component that renders a breadcrumb" },
            new { Title = "Link", Description = "A component to give a link to a domain object" },
            new { Title = "Icon", Description = "A component that displays built-in icons" },
            new { Title = "Locale", Description = "Allow locale customization and language support" },
            new { Title = "Menu Page", Description = "A page component suitable for rendering navigation pages" },
            new { Title = "Money", Description = "A component to render money values" },
            new { Title = "Page Title", Description = "A component to render page title, desc and actions" },
            new { Title = "Rate", Description = "A component to render rate values as percentage" },
            new { Title = "Report Page", Description = "A page component to render report pages" },
            new { Title = "Side Menu", Description = "A layout component to render application menu" },
            new { Title = "Toast", Description = "A behavioral component to render alert messages" }
        };

        configurator.ConfigureAppDescriptor(app =>
        {
            app.Error = ErrorPage(
                safeLinks:
                [
                    CardLink("/", "Home", icon: "pi pi-home"),
                    CardLink("/specs", "Specs", icon: "pi pi-list-check"),
                ],
                errorInfos:
                [
                    ErrorPageInfo(403, "Access Denied", "You do not have the permision to view the address or data specified." ),
                    ErrorPageInfo(404, "Page Not Found", "The page you want to view is etiher deleted or outdated."),
                    ErrorPageInfo(500, "Unexpected Error", "Please contact system administrator.")
                ],
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
                        footer: new ComponentDescriptor("Logout")
                    ),
                    header: Header(
                        siteMap:
                        [
                            HeaderItem("/", icon: "pi pi-home"),
                            HeaderItem("/specs", icon: "pi pi-list-check", title: "Specs"),
                            .. specs.Select(spec => HeaderItem($"/specs/{spec.Title.Kebaberize()}", title: spec.Title, parentRoute: "/specs"))
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
                    .. specs.Select(spec => CardLink($"/specs/{spec.Title.Kebaberize()}", spec.Title,
                        icon: "pi pi-microchip",
                        description: spec.Description
                    ))
                ]
            ));
        });
    }
}