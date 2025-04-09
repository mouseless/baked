using Baked.Architecture;
using Baked.ExceptionHandling;
using Baked.RestApi.Model;
using Baked.Test.Authentication;
using Baked.Test.Business;
using Baked.Test.ExceptionHandling;
using Baked.Test.Orm;
using Baked.Test.Theme;
using Baked.Theme.Admin;
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
            new
            {
                Name = "Layout",
                Links = new[]
                {
                    new { Title = "Header", Description = "Renders a breadcrumb" },
                    new { Title = "Side Menu", Description = "Renders application menu" }
                }
            },
            new
            {
                Name = "Page",
                Links = new[]
                {
                    new { Title = "Error Page", Description = "Display errors in full page" },
                    new { Title = "Menu Page", Description = "Render navigation pages" },
                    new { Title = "Report Page", Description = "Render report pages" }
                }
            },
            new
            {
                Name = "Container",
                Links = new[]
                {
                    new { Title = "Card Link", Description = "Renders a link as a big card-like button" },
                    new { Title = "Data Panel", Description = "Lazy load and view a data within a panel" },
                }
            },
            new
            {
                Name = "Display",
                Links = new[]
                {
                    new { Title = "Data Table", Description = "View list data in a table" },
                    new { Title = "Nav Link", Description = "A component to give a link to a domain object" },
                    new { Title = "Icon", Description = "Displays built-in icons" },
                    new { Title = "Money", Description = "Render money values" },
                    new { Title = "Rate", Description = "Render rate values as percentage" }
                }
            },
            new
            {
                Name = "Input",
                Links = new[]
                {
                    new { Title = "Page Title", Description = "Render page title, desc and actions" },
                    new { Title = "Query Parameters", Description = "Sync and manage parameters in query string" },
                    new { Title = "Select", Description = "Allow select from given options using drow down" },
                    new { Title = "Select Button", Description = "Allow select from given options using buttons" }
                }
            },
            new
            {
                Name = "Plugins",
                Links = new[]
                {
                    new { Title = "Auth", Description = "Authorized routing and client" },
                    new { Title = "Error Handling", Description = "Handling errors" },
                    new { Title = "Locale", Description = "Allow locale customization and language support" },
                }
            },
            new
            {
                Name = "Behavior",
                Links = new[]
                {
                    new { Title = "Bake", Description = "The core component that renders a dynamic component using given descriptor" },
                    new { Title = "Custom CSS", Description = "Allow custom configuration to define custom css and more" },
                    new { Title = "Parameters", Description = "Manage parameters through emits" },
                    new { Title = "Toast", Description = "Render alert messages" }
                }
            }
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
            layouts.Add(DefaultLayout("default",
                sideMenu: SideMenu(
                    menu:
                    [
                        SideMenuItem("/", "pi pi-home"),
                        SideMenuItem("/report", "pi pi-file", title: "Report"),
                        SideMenuItem("/specs", "pi pi-list-check", title: "Specs")
                    ],
                    footer: String(Inline("FT"))
                ),
                header: Header(
                    siteMap:
                    [
                        HeaderItem("/", icon: "pi pi-home"),
                        HeaderItem("/report", icon: "pi pi-file", title: "Report"),
                        HeaderItem("/specs", icon: "pi pi-list-check", title: "Specs"),
                        .. specs.SelectMany(section =>
                            section.Links.Select(link =>
                                HeaderItem($"/specs/{section.Name.ToLower()}/{link.Title.Kebaberize()}", title: link.Title, parentRoute: "/specs")
                            )
                        )
                    ]
                )
            ));

            layouts.Add(ModalLayout("modal"));
        });

        configurator.ConfigurePageDescriptors(pages =>
        {
            var headers = Inline(new { Authorization = "token-admin-ui" });

            pages.Add(MenuPage("index",
                links:
                [
                    CardLink($"/report", "Report",
                        icon: "pi pi-file",
                        description: "Showcases a report layout with tabs and data panels"
                    ),
                    CardLink($"/specs", "Specs",
                        icon: "pi pi-list-check",
                        description: "All UI Specs are listed here"
                    ),
                    CardLink($"/page/with/route/pageWithRoute", "Page With Route",
                        icon: "pi pi-list-check",
                        description: "Demo for route support"
                    )
                ]
            ));

            pages.Add(CustomPage<Login>("login", layout: "modal"));
            pages.Add(CustomPage<PageWithRoute>("page/with/route/pageWithRoute", layout: "default"));

            configurator.UsingDomainModel(domain =>
            {
                var report = domain.Types[typeof(Report)].GetMembers();
                var wide = report.Methods[nameof(Report.GetWide)];
                var left = report.Methods[nameof(Report.GetLeft)];
                var right = report.Methods[nameof(Report.GetRight)];
                var first = report.Methods[nameof(Report.GetFirst)];
                var second = report.Methods[nameof(Report.GetSecond)];

                pages.Add(ReportPage("report",
                    title: PageTitle("Report", description: "Showcases a report layout with tabs and data panels"),
                    queryParameters:
                    [
                        Parameter(
                            "requiredWithDefault",
                            Select("Required w/ Default",
                                data: Inline(new[]
                                {
                                  new { text = "Required w/ Default 1", value = "rwd-1" },
                                  new { text = "Required w/ Default 2", value = "rwd-2" }
                                }),
                                optionLabel: "text",
                                optionValue: "value"
                            ),
                            defaultValue: "rwd-1",
                            required: true
                        ),
                        Parameter("required", Select("Required", data: Inline(new[] { "Required 1", "Required 2" })),
                            required: true
                        ),
                        Parameter("optional", SelectButton(Inline(new[] { "Optional 1", "Optional 2" }), allowEmpty: true))
                    ],
                    tabs:
                    [
                        ReportPageTab("single-value", "Single Value",
                            icon: Icon("pi-box"),
                            contents:
                            [
                                ReportPageTabContent(
                                    component: DataPanel(wide.Name.Humanize(),
                                        content: String(Remote($"/{wide.GetSingle<ActionModelAttribute>().GetRoute()}",
                                            headers: headers,
                                            query: Computed(Composables.UseQuery)
                                        )),
                                        collapsed: false
                                    )
                                ),
                                ReportPageTabContent(
                                    component: DataPanel(left.Name.Humanize(),
                                        content: String(Remote($"/{left.GetSingle<ActionModelAttribute>().GetRoute()}",
                                            headers: headers,
                                            query: Computed(Composables.UseQuery)
                                        )),
                                        collapsed: true
                                    ),
                                    narrow: true
                                ),
                                ReportPageTabContent(
                                    component: DataPanel(right.Name.Humanize(),
                                        content: String(Remote($"/{right.GetSingle<ActionModelAttribute>().GetRoute()}",
                                            headers: headers,
                                            query: Computed(Composables.UseQuery)
                                        )),
                                        collapsed: true
                                    ),
                                    narrow: true
                                )
                            ]
                        ),
                        ReportPageTab("data-table", "Data Table",
                            icon: Icon("pi-table"),
                            contents:
                            [
                                ReportPageTabContent(
                                    component: DataPanel(first.Name.Humanize(),
                                        parameters:
                                        [
                                            Parameter("count", Select("Count", data: Inline(Enum.GetNames<CountOptions>()), stateful: true),
                                                defaultValue: CountOptions.Default
                                            )
                                        ],
                                        content: DataTable(
                                            columns:
                                            [
                                                DataTableColumn("label", title: "Label", minWidth: true),
                                                DataTableColumn("column1", title: "Column 1"),
                                                DataTableColumn("column2", title: "Column 2"),
                                                DataTableColumn("column3", title: "Column 3")
                                            ],
                                            dataKey: "label",
                                            paginator: true,
                                            rows: 5,
                                            data: Remote($"/{first.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                headers: headers,
                                                query: Composite([Computed(Composables.UseQuery), Injected()])
                                            )
                                        )
                                    )
                                ),
                                ReportPageTabContent(
                                    component: DataPanel(second.Name.Humanize(),
                                        parameters:
                                        [
                                            Parameter("count", SelectButton(Inline(Enum.GetNames<CountOptions>()), stateful: true),
                                                defaultValue: CountOptions.Default
                                            )
                                        ],
                                        content: DataTable(
                                            columns:
                                            [
                                                DataTableColumn("label", title: "Label", minWidth: true),
                                                DataTableColumn("column1", title: "Column 1"),
                                                DataTableColumn("column2", title: "Column 2"),
                                                DataTableColumn("column3", title: "Column 3")
                                            ],
                                            dataKey: "label",
                                            paginator: true,
                                            rows: 5,
                                            data: Remote($"/{second.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                headers: headers,
                                                query: Composite([Computed(Composables.UseQuery), Injected()])
                                            )
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
                pageContextKey: "menuPageContextKey",
                header: PageTitle(
                  title: "Specs",
                  description: "All UI Specs are listed here",
                  actions: [Filter(placeholder: "Ara", contextKey: "menuPageContextKey")]
                ),
                sections:
                [
                    .. specs.Select(section =>
                        MenuPageSection(
                            title: section.Name,
                            filterables:
                            [
                                .. section.Links.Select(l =>
                                    Filterable(
                                        title: l.Title,
                                        link: CardLink($"/specs/{l.Title.Kebaberize()}", l.Title,
                                            icon: "pi pi-microchip",
                                            description: l.Description
                                        )
                                    )
                                )
                            ]
                        )
                    )
                ]
            ));
        });
    }
}