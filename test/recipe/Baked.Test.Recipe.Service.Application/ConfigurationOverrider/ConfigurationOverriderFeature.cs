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
                    new { Title = "Bake", Description = "The_core_component_that_renders_a_dynamic_component_using_given_descriptor" },
                    new { Title = "Custom CSS", Description = "Allow_custom_configuration_to_define_custom_css_and_more" },
                    new { Title = "Parameters", Description = "Manage_parameters_through_emits" },
                    new { Title = "Query Parameters", Description = "Sync_and_manage_parameters_in_query_string" },
                    new { Title = "Toast", Description = "Render_alert_messages" }
                }
            },
            new
            {
                Name = "Display",
                Links = new[]
                {
                    new { Title = "Card Link", Description = "Renders_a_link_as_a_big_card_like_button" },
                    new { Title = "Data Table", Description = "View_list_data_in_a_table" },
                    new { Title = "Nav Link", Description = "A_component_to_give_a_link_to_a_domain_object" },
                    new { Title = "Icon", Description = "Displays_built_in_icons" },
                    new { Title = "Message", Description = "A_component_to_display_message" },
                    new { Title = "Money", Description = "Shortens_and_renders_money_values_with_the_full_value_shown_as_tooltip" },
                    new { Title = "Number", Description = "Shortens_and_renders_numbers_with_the_full_value_shown_as_tooltip" },
                    new { Title = "Rate", Description = "Render_rate_values_as_percentage" },
                    new { Title = "String", Description = "Render_string_values" }
                }
            },
            new
            {
                Name = "Input",
                Links = new[]
                {
                    new { Title = "Language Switcher", Description = "Allow_change_site_language" },
                    new { Title = "Select", Description = "Allow_select_from_given_options_using_drow_down" },
                    new { Title = "Select Button", Description = "Allow_select_from_given_options_using_buttons" }
                }
            },
            new
            {
                Name = "Layout",
                Links = new[]
                {
                    new { Title = "Data Panel", Description = "Lazy_load_and_view_a_data_within_a_panel" },
                    new { Title = "Header", Description = "Renders_a_breadcrumb" },
                    new { Title = "Page Title", Description = "Render_page_title_desc_and_actions" },
                    new { Title = "Side Menu", Description = "Renders_application_menu" }
                }
            },
            new
            {
                Name = "Page",
                Links = new[]
                {
                    new { Title = "Error Page", Description = "Display_errors_in_full_page" },
                    new { Title = "Menu Page", Description = "Render_navigation_pages" },
                    new { Title = "Report Page", Description = "Render_report_pages" }
                }
            },
            new
            {
                Name = "Plugins",
                Links = new[]
                {
                    new { Title = "Auth", Description = "Authorized_routing_and_client" },
                    new { Title = "Cache", Description = "Caches_api_responses_in_local_storage" },
                    new { Title = "Locale", Description = "Allow_locale_customization_and_language_support" },
                    new { Title = "Error Handling", Description = "Handling_errors" },
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
                        CardLink("/", "Home", icon: "pi pi-home"),
                        CardLink("/cache", title: l("Cache"), "pi pi-database"),
                        CardLink("/data-table", l("DataTable"), "pi pi-table"),
                        CardLink("/report", l("Report"), icon: "pi pi-file"),
                        CardLink("/specs", l("Specs"), icon: "pi pi-list-check"),
                    ],
                    errorInfos:
                    [
                        ErrorPageInfo(403, l("Access_Denied"), l("You_do_not_have_the_permision_to_view_the_address_or_data_specified") ),
                        ErrorPageInfo(404, l("Page_Not_Found"), l("The_page_you_want_to_view_is_etiher_deleted_or_outdated")),
                        ErrorPageInfo(500, l("Unexpected_Error"), l("Please_contact_system_administrator"))
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
                            SideMenuItem("/data-table", "pi pi-table", title: l("DataTable")),
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
                            HeaderItem("/data-table", icon: "pi pi-table", title: l("DataTable")),
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
                            description: l("Showcases_the_cache_behavior")
                        ),
                        CardLink($"/data-table", l("DataTable"),
                            icon: "pi pi-table",
                            description: l("Showcase_DataTable_component_with_scrollable_and_footer_options")
                        ),
                        CardLink($"/report", l("Report"),
                            icon: "pi pi-file",
                            description: l("Showcases_a_report_layout_with_tabs_and_data_panels")
                        ),
                        CardLink($"/specs", l("Specs"),
                            icon: "pi pi-list-check",
                            description: l("All_ui_specs_are_listed_here")
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
                        title: PageTitle("Report", description: l("Showcases_a_report_layout_with_tabs_and_data_panels")),
                        queryParameters:
                        [
                            Parameter(
                            "requiredWithDefault",
                                Select(l("Required_w_default"),
                                    data: Inline(new[]
                                    {
                                      new { text = l("Required_w_default_1"), value = "rwd-1" },
                                      new { text = l("Required_w_default_2"), value = "rwd-2" }
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
                            ReportPageTab("single-value", l("Single_value"),
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
                        ReportPageTab("data-table", l("Data_table"),
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
                                    component: DataPanel(l(second.Name.Humanize()),
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
                                                Parameter("count", Select("Count", Inline(new string[]{ "10", "20", "100", "1000", "10000" })),
                                                    defaultValue: "10"
                                                )
                                            ],
                                            content: DataTable(
                                                columns:
                                                [
                                                  .. domain.Types[typeof(TableRow)].GetMembers().Properties.Where(p => p.IsPublic).Select((p, i) =>
                                                      DataTableColumn(p.Name.Camelize(),
                                                          title: l(p.Name),
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
                                                exportOptions: DataTableExport(";", "data-table-export", formatter: "useCsvFormatter", buttonLabel: "Export as CSV"),
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
                        title: PageTitle("Cache", description: l("Showcases_the_cache_behavior")),
                        queryParameters:
                        [
                            Parameter("parameter", Select("Parameter", Inline(new[] { "value_a", "value_b" })),
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
                                        component: DataPanel(getScoped.Name.Humanize(),
                                            content: String(
                                                data: Remote($"/{getScoped.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                    headers: headers,
                                                    query: Computed(Composables.UseQuery),
                                                    options: [("client-cache", "user")]
                                                )
                                            )
                                        ),
                                        narrow: true
                                    ),
                                    ReportPageTabContent(
                                        component: DataPanel(getApplication.Name.Humanize(),
                                            content: String(
                                                data: Remote($"/{getApplication.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                    headers: headers,
                                                    query: Computed(Composables.UseQuery),
                                                    options: [("client-cache", "application")]
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
                      description: l("All_ui_specs_are_listed_here"),
                      actions: [Filter(placeholder: "Filter", pageContextKey: "menu-page")]
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