using Baked.Architecture;
using Baked.ExceptionHandling;
using Baked.RestApi.Model;
using Baked.Test.Authentication;
using Baked.Test.Business;
using Baked.Test.Caching;
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
                    new { Title = "Money", Description = "Shortens and renders money values with the full value shown as tooltip" },
                    new { Title = "Number", Description = "Shortens and renders numbers with the full value shown as tooltip" },
                    new { Title = "Rate", Description = "Render rate values as percentage" },
                    new { Title = "String", Description = "Render string values" }
                }
            },
            new
            {
                Name = "Input",
                Links = new[]
                {
                    new { Title = "Language Switcher", Description = "Allow change site language" },
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
                    new { Title = "Cache", Description = "Caches api responses in local storage" },
                    new { Title = "Locale", Description = "Allow locale customization and language support" },
                    new { Title = "Error Handling", Description = "Handling errors" },
                }
            },
        };

        configurator.ConfigureAppDescriptor(app =>
        {
            configurator.UsingLocalization(l =>
            {
                app.Error = ErrorPage(
                    safeLinks:
                    [
                        CardLink("/", l("Home"), icon: "pi pi-home"),
                        CardLink("/cache", title: l("Cache"), "pi pi-database"),
                        CardLink("/data-table", l("Data Table"), "pi pi-table"),
                        CardLink("/report", l("Report"), icon: "pi pi-file"),
                        CardLink("/specs", l("Specs"), icon: "pi pi-list-check"),
                    ],
                    errorInfos:
                    [
                        ErrorPageInfo(403, l("Access Denied"), l("You do not have the permision to view the address or data specified.") ),
                        ErrorPageInfo(404, l("Page Not Found"), l("The page you want to view is etiher deleted or outdated.")),
                        ErrorPageInfo(500, l("Unexpected Error"), l("Please contact system administrator."))
                    ],
                    data: Computed(Composables.UseError)
                );
            });
        });

        configurator.ConfigureLayoutDescriptors(layouts =>
        {
            configurator.UsingLocalization(l =>
            {
                layouts.Add(DefaultLayout("default",
                    sideMenu: SideMenu(
                        menu:
                        [
                            SideMenuItem("/", "pi pi-home"),
                            SideMenuItem("/cache", "pi pi-database", title: l("Cache")),
                            SideMenuItem("/data-table", "pi pi-table", title: l("Data Table")),
                            SideMenuItem("/report", "pi pi-file", title: l("Report")),
                            SideMenuItem("/specs", "pi pi-list-check", title: l("Specs"))
                        ],
                        footer: LanguageSwitcher()
                    ),
                    header: Header(
                        siteMap:
                        [
                            HeaderItem("/", icon: "pi pi-home"),
                            HeaderItem("/cache", icon: "pi pi-database", title: l("Cache")),
                            HeaderItem("/data-table", icon: "pi pi-table", title: l("Data Table")),
                            HeaderItem("/report", icon: "pi pi-file", title: l("Report")),
                            HeaderItem("/specs", icon: "pi pi-list-check", title: l("Specs")),
                            .. specs.SelectMany(section =>
                                section.Links.Select(link =>
                                    HeaderItem($"/specs/{link.Title.Kebaberize()}", title: l(link.Title), parentRoute: "/specs")
                                )
                            )
                        ]
                    )
                ));
            });

            layouts.Add(ModalLayout("modal"));
        });

        configurator.ConfigurePageDescriptors(pages =>
        {
            configurator.UsingLocalization(l =>
            {
                var headers = Inline(new { Authorization = "token-admin-ui" });

                pages.Add(MenuPage("index",
                    links:
                    [
                        CardLink($"/cache", l("Cache"),
                            icon: "pi pi-database",
                            description: l("Showcases the cache behavior")
                        ),
                        CardLink($"/data-table", l("Data Table"),
                            icon: "pi pi-table",
                            description: l("Showcase DataTable component with scrollable and footer options")
                        ),
                        CardLink($"/report", l("Report"),
                            icon: "pi pi-file",
                            description: l("Showcases a report layout with tabs and data panels")
                        ),
                        CardLink($"/specs", l("Specs"),
                            icon: "pi pi-list-check",
                            description: l("All UI Specs are listed here")
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
                        title: PageTitle(l("Report"), description: l("Showcases a report layout with tabs and data panels")),
                        queryParameters:
                        [
                            Parameter(
                            "requiredWithDefault",
                                Select(l("Required w/ Default"),
                                    data: Inline(new[]
                                    {
                                      new { text = l("Required w/ Default 1"), value = l("rwd-1") },
                                      new { text = l("Required w/ Default 2"), value = l("rwd-2") }
                                    }),
                                    optionLabel: "text",
                                    optionValue: "value"
                                ),
                                defaultValue: "rwd-1",
                                required: true
                            ),
                            Parameter("required", Select(l("Required"), data: Inline(new[] { l("Required 1"), l("Required 2") })),
                                required: true
                            ),
                            Parameter("optional", SelectButton(Inline(new[] { l("Optional 1"), l("Optional 2") }), allowEmpty: true))
                        ],
                        tabs:
                        [
                            ReportPageTab("single-value", l("Single Value"),
                                icon: Icon("pi-box"),
                                contents:
                                [
                                    ReportPageTabContent(
                                        component: DataPanel(l(wide.Name),
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
                                        component: DataPanel(l(left.Name),
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
                                        component: DataPanel(l(right.Name),
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
                            ReportPageTab("data-table", l("Data Table"),
                                icon: Icon("pi-table"),
                                contents:
                                [
                                    ReportPageTabContent(
                                        component: DataPanel(l(first.Name),
                                            parameters:
                                            [
                                                Parameter("count",
                                                    component: Select(l("Count"), Inline(Enum.GetNames<CountOptions>().Select(name => l(name))), stateful: true),
                                                    defaultValue: CountOptions.Default
                                                )
                                            ],
                                            content: DataTable(
                                                columns:
                                                [
                                                    DataTableColumn("label", title: l("Label"), minWidth: true, exportable: true),
                                                    DataTableColumn("column1", title: l("Column 1"), exportable: true),
                                                    DataTableColumn("column2", title: l("Column 2"), exportable: true),
                                                    DataTableColumn("column3", title: l("Column 3"), exportable: true)
                                                ],
                                                dataKey: "label",
                                                paginator: true,
                                                rows: 5,
                                                exportOptions: DataTableExport(";", l("first"),
                                                    formatter: "useCsvFormatter",
                                                    buttonLabel: l("Export as CSV"),
                                                    appendParameters: true,
                                                    localizeParameters: true,
                                                    parameterSeparator: "_"
                                                ),
                                                data: Remote($"/{first.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                    headers: headers,
                                                    query: Composite([Computed(Composables.UseQuery), Injected()])
                                                )
                                            )
                                        )
                                    ),
                                    ReportPageTabContent(
                                        component: DataPanel(l(second.Name),
                                            parameters:
                                            [
                                                Parameter("count",
                                                    component: SelectButton( data: Inline(Enum.GetNames<CountOptions>().Select(name => l(name))), stateful: true),
                                                    defaultValue: CountOptions.Default
                                                )
                                            ],
                                            content: DataTable(
                                                columns:
                                                [
                                                    DataTableColumn("label", title: l("Label"), minWidth: true),
                                                    DataTableColumn("column1", title: l("Column 1")),
                                                    DataTableColumn("column2", title: l("Column 2")),
                                                    DataTableColumn("column3", title: l("Column 3"))
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

                configurator.UsingDomainModel(domain =>
                {
                    pages.Add(ReportPage("data-table", PageTitle(l("Data Table Demo")),
                        tabs:
                        [
                            ReportPageTab(string.Empty, string.Empty,
                                contents:
                                [
                                    ReportPageTabContent(
                                        DataPanel(l("Data Panel"),
                                            parameters:
                                            [
                                                Parameter("count", Select(l("Count"), Inline(new string[]{ "10", "20", "100", "1000", "10000" }, requireLocalization: false)),
                                                    defaultValue: "10"
                                                )
                                            ],
                                            content: DataTable(
                                                columns:
                                                [
                                                  .. domain.Types[typeof(TableRow)].GetMembers().Properties.Where(p => p.IsPublic).Select((p, i) =>
                                                      DataTableColumn(p.Name.Camelize(),
                                                          title: l(p.Name.Humanize(LetterCasing.Title)),
                                                          exportable: true,
                                                          alignRight: p.PropertyType.Is<string>() ? null : true,
                                                          frozen: i == 0 ? true : null,
                                                          minWidth: i == 0 ? true : null
                                                      )
                                                    )
                                                ],
                                                footerTemplate: DataTableFooter(l("Total"),
                                                    columns:
                                                    [
                                                        DataTableColumn(nameof(TableWithFooter.FooterColumn1).Camelize(), Conditional(), alignRight: true),
                                                        DataTableColumn(nameof(TableWithFooter.FooterColumn2).Camelize(), Conditional(), alignRight: true)
                                                    ]
                                                ),
                                                dataKey: nameof(TableRow.Label).Camelize(),
                                                itemsProp: "items",
                                                scrollHeight: "500px",
                                                virtualScrollerOptions: DataTableVirtualScroller(45),
                                                exportOptions: DataTableExport(";", l("data-table-export"),
                                                    formatter: "useCsvFormatter",
                                                    buttonLabel: l("Export as CSV"),
                                                    appendParameters: true
                                                ),
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

                configurator.UsingDomainModel(domain =>
                {
                    var report = domain.Types[typeof(CacheSamples)].GetMembers();
                    var getScoped = report.Methods[nameof(CacheSamples.GetScoped)];
                    var getApplication = report.Methods[nameof(CacheSamples.GetApplication)];

                    pages.Add(ReportPage("cache",
                        title: PageTitle("Cache", description: l("Showcases the cache behavior")),
                        queryParameters:
                        [
                            Parameter("parameter", Select(l("Parameter"), Inline(new[] { "value_a", "value_b" }, requireLocalization: false)),
                                required: true,
                                defaultValue: "value_a"
                            )
                        ],
                        tabs:
                        [
                            ReportPageTab("default", string.Empty,
                                contents:
                                [
                                    ReportPageTabContent(
                                        component: DataPanel(l(getScoped.Name),
                                            content: String(
                                                data: Remote($"/{getScoped.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                    headers: headers,
                                                    query: Computed(Composables.UseQuery),
                                                    attributes: [("client-cache", "user")]
                                                )
                                            )
                                        ),
                                        narrow: true
                                    ),
                                    ReportPageTabContent(
                                        component: DataPanel(l(getApplication.Name),
                                            content: String(
                                                data: Remote($"/{getApplication.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                    headers: headers,
                                                    query: Computed(Composables.UseQuery),
                                                    attributes: [("client-cache", "application")]
                                                )
                                            )
                                        ),
                                        narrow: true
                                    )
                                ]
                            )
                        ]
                    ));
                });

                pages.Add(MenuPage("specs",
                    filterPageContextKey: "menu-page",
                    header: PageTitle(
                      title: l("Specs"),
                      description: l("All UI Specs are listed here"),
                      actions: [Filter(placeholder: l("Filter"), pageContextKey: "menu-page")]
                    ),
                    sections:
                    [
                        .. specs.Select(section =>
                            MenuPageSection(
                                title: l(section.Name),
                                links:
                                [
                                    .. section.Links.Select(link =>
                                        Filterable(
                                            title: l(link.Title),
                                            component: CardLink($"/specs/{link.Title.Kebaberize()}", l(link.Title),
                                                icon: "pi pi-microchip",
                                                description: l(link.Description)
                                            )
                                        )
                                    )
                                ]
                            )
                        )
                    ]
                ));
            });
        });
    }
}