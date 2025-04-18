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
                Name = "Behavior",
                Links = new[]
                {
                    new { Title = "Bake", Description = "The core component that renders a dynamic component using given descriptor" },
                    new { Title = "Custom CSS", Description = "Allow custom configuration to define custom css and more" },
                    new { Title = "Locale", Description = "Allow locale customization and language support" },
                    new { Title = "Parameters", Description = "Manage parameters through emits" },
                    new { Title = "Query Parameters", Description = "Sync and manage parameters in query string" },
                    new { Title = "Toast", Description = "Render alert messages" }
                }
            },
            new
            {
                Name = "Display",
                Links = new[]
                {
                    new { Title = "Card Link", Description = "Renders a link as a big card-like button" },
                    new { Title = "Data Table", Description = "View list data in a table" },
                    new { Title = "Nav Link", Description = "A component to give a link to a domain object" },
                    new { Title = "Icon", Description = "Displays built-in icons" },
                    new { Title = "Message", Description = "A component to display message" },
                    new { Title = "Money", Description = "Render money values" },
                    new { Title = "Rate", Description = "Render rate values as percentage" },
                    new { Title = "String", Description = "Render string values" }
                }
            },
            new
            {
                Name = "Input",
                Links = new[]
                {
                    new { Title = "Select", Description = "Allow select from given options using drow down" },
                    new { Title = "Select Button", Description = "Allow select from given options using buttons" }
                }
            },
            new
            {
                Name = "Layout",
                Links = new[]
                {
                    new { Title = "Data Panel", Description = "Lazy load and view a data within a panel" },
                    new { Title = "Header", Description = "Renders a breadcrumb" },
                    new { Title = "Page Title", Description = "Render page title, desc and actions" },
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
                Name = "Plugins",
                Links = new[]
                {
                    new { Title = "Auth", Description = "Authorized routing and client" },
                    new { Title = "Error Handling", Description = "Handling errors" },
                }
            },
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
                    footer: String(data: Inline("FT"))
                ),
                header: Header(
                    siteMap:
                    [
                        HeaderItem("/", icon: "pi pi-home"),
                        HeaderItem("/report", icon: "pi pi-file", title: "Report"),
                        HeaderItem("/specs", icon: "pi pi-list-check", title: "Specs"),
                        .. specs.SelectMany(section =>
                            section.Links.Select(link =>
                                HeaderItem($"/specs/{link.Title.Kebaberize()}", title: link.Title, parentRoute: "/specs")
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
                    CardLink($"/data-table", "DataTable",
                        icon: "pi pi-list-check",
                        description: "Showcase Datatable component with scrollable and footer options"
                    ),
                    CardLink($"/report", "Report",
                        icon: "pi pi-file",
                        description: "Showcases a report layout with tabs and data panels"
                    ),
                    CardLink($"/specs", "Specs",
                        icon: "pi pi-list-check",
                        description: "All UI Specs are listed here"
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
                                        content: String(
                                            data: Remote($"/{wide.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                headers: headers,
                                                query: Computed(Composables.UseQuery)
                                            )
                                        ),
                                        collapsed: false
                                    )
                                ),
                                ReportPageTabContent(
                                    component: DataPanel(left.Name.Humanize(),
                                        content: String(
                                            data: Remote($"/{left.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                headers: headers,
                                                query: Computed(Composables.UseQuery)
                                            )
                                        ),
                                        collapsed: true
                                    ),
                                    narrow: true
                                ),
                                ReportPageTabContent(
                                    component: DataPanel(right.Name.Humanize(),
                                        content: String(
                                            data: Remote($"/{right.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                headers: headers,
                                                query: Computed(Composables.UseQuery)
                                            )
                                        ),
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
                filterPageContextKey: "menu-page",
                header: PageTitle(
                  title: "Specs",
                  description: "All UI Specs are listed here",
                  actions: [Filter(placeholder: "Filter", pageContextKey: "menu-page")]
                ),
                sections:
                [
                    .. specs.Select(section =>
                        MenuPageSection(
                            title: section.Name,
                            links:
                            [
                                .. section.Links.Select(l =>
                                    Filterable(
                                        title: l.Title,
                                        component: CardLink($"/specs/{l.Title.Kebaberize()}", l.Title,
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

            configurator.UsingDomainModel(domain =>
            {
                pages.Add(ReportPage("data-table", PageTitle("DataTable Demo"),
                    tabs:
                    [
                        ReportPageTab(string.Empty, string.Empty,
                            contents:
                            [
                                ReportPageTabContent(
                                    DataPanel("DataPanel",
                                        parameters:
                                        [
                                            Parameter("count", Select("Count", Inline(new string[]{ "10","20" })),
                                                defaultValue: "10"
                                            )
                                        ],
                                        content: DataTable(
                                            columns: [.. domain.Types[typeof(TableRow)].GetMembers().Properties.Where(p => p.IsPublic).Select(p => DataTableColumn(p.Name.ToLower(), title: p.Name))],
                                            footerTemplate: DataTableFooter("Total",
                                                columns:
                                                [
                                                    DataTableColumn(nameof(TableWithFooter.FooterColumn1).ToLower(), Conditional()),
                                                    DataTableColumn(nameof(TableWithFooter.FooterColumn2).ToLower(), Conditional())
                                                ]
                                            ),
                                            dataKey: nameof(TableRow.Label).ToLower(),
                                            itemsProp: "items",
                                            scrollHeight: "500px",
                                            data: Remote(domain.Types[typeof(Theme.DataTable)].GetMembers().Methods[nameof(Theme.DataTable.GetTableDataWithFooter)].GetSingle<ActionModelAttribute>().GetRoute(),
                                                query: Injected(custom: true)
                                            )
                                        )
                                    )
                                )
                            ]
                        )
                    ]
                ));
            });
        });
    }
}