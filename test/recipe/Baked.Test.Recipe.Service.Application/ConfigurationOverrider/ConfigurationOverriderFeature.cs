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
                    options: s =>
                    {
                        s.SafeLinks.AddRange(
                        [
                            CardLink("/", l("Home"), options: s => s.Icon = "pi pi-home"),
                            CardLink("/cache", l("Cache"), options: s => s.Icon = "pi pi-database"),
                            CardLink("/data-table", l("Data Table"), options: s => s.Icon = "pi pi-table"),
                            CardLink("/report", l("Report"), options: s => s.Icon =  "pi pi-file"),
                            CardLink("/specs", l("Specs"), options: s => s.Icon = "pi pi-list-check"),
                        ]);
                        s.ErrorInfos[403] = ErrorPageInfo(l("Access Denied"), l("You do not have the permision to view the address or data specified."));
                        s.ErrorInfos[404] = ErrorPageInfo(l("Page Not Found"), l("The page you want to view is etiher deleted or outdated."));
                        s.ErrorInfos[500] = ErrorPageInfo(l("Unexpected Error"), l("Please contact system administrator."));
                    },
                    data: Computed(Composables.UseError)
                );
            });
        });

        configurator.ConfigureLayoutDescriptors(layouts =>
        {
            configurator.UsingLocalization(l =>
            {
                layouts.Add(DefaultLayout("default", options: s =>
                {
                    s.SideMenu = SideMenu(
                        options: s =>
                        {
                            s.Menu.AddRange(
                            [
                                SideMenuItem("/", "pi pi-home"),
                                SideMenuItem("/cache", "pi pi-database", options: s => s.Title = l("Cache")),
                                SideMenuItem("/data-table", "pi pi-table", options: s => s.Title = l("Data Table")),
                                SideMenuItem("/report", "pi pi-file", options: s => s.Title = l("Report")),
                                SideMenuItem("/specs", "pi pi-list-check", options: s => s.Title = l("Specs"))
                            ]);
                            s.Footer = LanguageSwitcher();
                        }
                    );
                    s.Header = Header(options: s =>
                    {
                        s.Sitemap["/"] = HeaderItem("/", options: s => s.Icon = "pi pi-home");
                        s.Sitemap["/cache"] = HeaderItem("/cache", options: s =>
                        {
                            s.Icon = "pi pi-database";
                            s.Title = l("Cache");
                        });
                        s.Sitemap["/data-table"] = HeaderItem("/data-table", options: s =>
                        {
                            s.Icon = "pi pi-table";
                            s.Title = l("Data Table");
                        });
                        s.Sitemap["/report"] = HeaderItem("/report", options: s =>
                        {
                            s.Icon = "pi pi-file";
                            s.Title = l("Report");
                        });
                        s.Sitemap["/specs"] = HeaderItem("/specs", options: s =>
                        {
                            s.Icon = "pi pi-list-check";
                            s.Title = l("Specs");
                        });

                        foreach (var spec in specs)
                        {
                            foreach (var link in spec.Links)
                            {
                                s.Sitemap[$"/specs/{link.Title.Kebaberize()}"] =
                                    HeaderItem($"/specs/{link.Title.Kebaberize()}", options: s =>
                                    {
                                        s.Title = l(link.Title);
                                        s.ParentRoute = "/specs";
                                    });
                            }
                        }
                    });
                }));
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
                        CardLink($"/cache", l("Cache"), options: s =>
                        {
                            s.Icon = "pi pi-database";
                            s.Description = l("Showcases the cache behavior");
                        }),
                        CardLink($"/data-table", l("Data Table"), options: s =>
                        {
                            s.Icon= "pi pi-table";
                            s.Description = l("Showcase DataTable component with scrollable and footer options");
                        }),
                        CardLink($"/report", l("Report"), options: s =>
                        {
                            s.Icon = "pi pi-file";
                            s.Description = l("Showcases a report layout with tabs and data panels");
                        }),
                        CardLink($"/specs", l("Specs"), options: s =>
                        {
                            s.Icon = "pi pi-list-check";
                            s.Description = l("All UI Specs are listed here");
                        })
                    ]
                ));

                pages.Add(new ComponentDescriptorAttribute<LoginPage>(new("login") { Layout = "modal" }));
                pages.Add(new ComponentDescriptorAttribute<RoutedPage>(new("page/with/route/pageWithRoute") { Layout = "default" }));

                configurator.UsingDomainModel(domain =>
                {
                    var report = domain.Types[typeof(Report)].GetMembers();
                    var wide = report.Methods[nameof(Report.GetWide)];
                    var left = report.Methods[nameof(Report.GetLeft)];
                    var right = report.Methods[nameof(Report.GetRight)];
                    var first = report.Methods[nameof(Report.GetFirst)];
                    var second = report.Methods[nameof(Report.GetSecond)];

                    pages.Add(ReportPage("report", PageTitle(l("Report"), options: s => s.Description = l("Showcases a report layout with tabs and data panels")),
                        options: s =>
                        {
                            s.QueryParameters.AddRange(
                            [
                                Parameter("requiredWithDefault",
                                    component: Select(l("Required w/ Default"),
                                        options: s =>
                                        {
                                            s.OptionLabel = "text";
                                            s.OptionValue = "value";
                                        },
                                        data: Inline(new[]
                                        {
                                          new { text = l("Required w/ Default 1"), value = l("rwd-1") },
                                          new { text = l("Required w/ Default 2"), value = l("rwd-2") }
                                        })
                                    ),
                                    options: s =>
                                    {
                                        s.DefaultValue = "rwd-1";
                                        s.Required = true;
                                    }
                                ),
                                Parameter("required",
                                    component: Select(l("Required"), data: Inline(new[] { l("Required 1"), l("Required 2") })),
                                    options: s => s.Required = true
                                ),
                                Parameter("optional",
                                    component: SelectButton(Inline(new[] { l("Optional 1"), l("Optional 2") }), options: s => s.AllowEmpty = true)
                                )
                            ]);
                            s.Tabs.AddRange(
                            [
                                ReportPageTab("single-value",
                                    options: s =>
                                    {
                                        s.Title = l("Single Value");
                                        s.Icon = Icon("pi-box");
                                        s.Contents.AddRange(
                                        [
                                            ReportPageTabContent(
                                                component: DataPanel(l(wide.Name),
                                                    content: String(
                                                        data: Remote($"/{wide.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                            headers: headers,
                                                            query: Computed(Composables.UseQuery)
                                                        )
                                                    ),
                                                    options: s => s.Collapsed = false
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
                                                    options: s => s.Collapsed = true
                                                ),
                                                options: s => s.Narrow = true
                                            ),
                                            ReportPageTabContent(
                                                component: DataPanel(l(right.Name),
                                                    content: String(
                                                        data: Remote($"/{right.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                            headers: headers,
                                                            query: Computed(Composables.UseQuery)
                                                        )
                                                    ),
                                                    options: s => s.Collapsed = true
                                                ),
                                                options: s => s.Narrow = true
                                            )
                                        ]);
                                    }
                                ),
                                ReportPageTab("data-table",
                                    options: s =>
                                    {
                                        s.Title = l("Data Table");
                                        s.Icon = Icon("pi-table");
                                        s.Contents.AddRange(
                                        [
                                            ReportPageTabContent(
                                                component: DataPanel(l(first.Name),
                                                    content: DataTable(
                                                        options: s =>
                                                        {
                                                            s.Columns.AddRange(
                                                            [
                                                                DataTableColumn("label", options: s =>
                                                                {
                                                                    s.Title = l("Label");
                                                                    s.MinWidth = true;
                                                                    s.Exportable = true;
                                                                }),
                                                                DataTableColumn("column1", options: s =>
                                                                {
                                                                    s.Title = l("Column 1");
                                                                    s.Exportable = true;
                                                                }),
                                                                DataTableColumn("column2", options: s =>
                                                                {
                                                                    s.Title = l("Column 2");
                                                                    s.Exportable = true;
                                                                }),
                                                                DataTableColumn("column3", options: s =>
                                                                {
                                                                    s.Title = l("Column 3");
                                                                    s.Exportable = true;
                                                                })
                                                            ]);
                                                            s.DataKey = "label";
                                                            s.Paginator = true;
                                                            s.Rows = 5;
                                                            s.ExportOptions = DataTableExport(";", l("first"), options: s =>
                                                            {
                                                                s.Formatter = "useCsvFormatter";
                                                                s.ButtonLabel = l("Export as CSV");
                                                                s.AppendParameters = true;
                                                                s.ParameterSeparator = "_";
                                                                s.ParameterFormatter = "useLocaleParameterFormatter";
                                                            });
                                                        },
                                                        data: Remote($"/{first.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                            headers: headers,
                                                            query: Composite([Computed(Composables.UseQuery), Injected()])
                                                        )
                                                    ),
                                                    options: s => s.Parameters.Add(
                                                        Parameter("count",
                                                            component: Select(l("Count"), Inline(Enum.GetNames<CountOptions>().Select(name => l(name))), options: s => s.Stateful = true),
                                                            options: s => s.DefaultValue = CountOptions.Default
                                                        )
                                                    )
                                                )
                                            ),
                                            ReportPageTabContent(
                                                component: DataPanel(l(second.Name),
                                                    content: DataTable(
                                                        options: s =>
                                                        {
                                                            s.Columns.AddRange(
                                                            [
                                                                DataTableColumn("label", options: s =>
                                                                {
                                                                    s.Title = l("Label");
                                                                    s.MinWidth = true;
                                                                }),
                                                                DataTableColumn("column1", options: s => s.Title = l("Column 1")),
                                                                DataTableColumn("column2", options: s => s.Title = l("Column 2")),
                                                                DataTableColumn("column3", options: s => s.Title = l("Column 3"))
                                                            ]);
                                                            s.DataKey = "label";
                                                            s.Paginator = true;
                                                            s.Rows = 5;
                                                        },
                                                        data: Remote($"/{second.GetSingle<ActionModelAttribute>().GetRoute()}",
                                                            headers: headers,
                                                            query: Composite([Computed(Composables.UseQuery), Injected()])
                                                        )
                                                    ),
                                                    options: s =>
                                                    {
                                                        s.Parameters.Add(
                                                            Parameter("count",
                                                                component: SelectButton(Inline(Enum.GetNames<CountOptions>().Select(name => l(name))), options: s => s.Stateful = true),
                                                                options: s => s.DefaultValue = CountOptions.Default
                                                            )
                                                        );
                                                        s.Collapsed = true;
                                                    }
                                                )
                                            )
                                        ]);
                                    }
                                )
                            ]);
                        }
                    ));
                });

                configurator.UsingDomainModel(domain =>
                {
                    pages.Add(ReportPage("data-table", PageTitle(l("Data Table Demo")),
                        options: s =>
                        {
                            s.Tabs.AddRange(
                            [
                                ReportPageTab("default",
                                    options: s =>
                                    {
                                        s.Contents.AddRange(
                                        [
                                            ReportPageTabContent(
                                                DataPanel(l("Data Panel"),
                                                    content: DataTable(
                                                        options: s =>
                                                        {
                                                            s.Columns.AddRange(
                                                            [
                                                                .. domain.Types[typeof(TableRow)].GetMembers().Properties.Where(p => p.IsPublic).Select((p, i) =>
                                                                    DataTableColumn(p.Name.Camelize(), options: s =>
                                                                    {
                                                                        s.Title = l(p.Name.Humanize(LetterCasing.Title));
                                                                        s.Exportable = true;
                                                                        s.AlignRight = p.PropertyType.Is<string>() ? null : true;
                                                                        s.Frozen = i == 0 ? true : null;
                                                                        s.MinWidth = i == 0 ? true : null;
                                                                    })
                                                                )
                                                            ]);
                                                            s.FooterTemplate = DataTableFooter(l("Total"),
                                                                options: s =>
                                                                {
                                                                    s.Columns.AddRange(
                                                                    [
                                                                        DataTableColumn(nameof(TableWithFooter.FooterColumn1).Camelize(), options: s => s.AlignRight = true),
                                                                        DataTableColumn(nameof(TableWithFooter.FooterColumn2).Camelize(), options: s => s.AlignRight = true)
                                                                    ]);
                                                                }
                                                            );
                                                            s.DataKey = nameof(TableRow.Label).Camelize();
                                                            s.ItemsProp = "items";
                                                            s.ScrollHeight = "500px";
                                                            s.VirtualScrollerOptions = DataTableVirtualScroller(options: s => s.ItemSize = 45);
                                                            s.ExportOptions = DataTableExport(";", l("data-table-export"), options: s =>
                                                            {
                                                                s.Formatter = "useCsvFormatter";
                                                                s.ButtonLabel = l("Export as CSV");
                                                                s.AppendParameters = true;
                                                            });
                                                        },
                                                        data: Remote(domain.Types[typeof(Theme.DataTable)].GetMembers().Methods[nameof(Theme.DataTable.GetTableDataWithFooter)].GetSingle<ActionModelAttribute>().GetRoute(),
                                                            query: Injected(custom: true)
                                                        )
                                                    ),
                                                    options: s => s.Parameters.Add(
                                                        Parameter("count",
                                                            component: Select(l("Count"), Inline(new string[]{ "10", "20", "100", "1000", "10000" }, requireLocalization: false)),
                                                            options: s => s.DefaultValue = "10"
                                                        )
                                                    )
                                                )
                                            )
                                        ]);
                                    }
                                )
                            ]);
                        }
                    ));
                });

                configurator.UsingDomainModel(domain =>
                {
                    var report = domain.Types[typeof(CacheSamples)].GetMembers();
                    var getScoped = report.Methods[nameof(CacheSamples.GetScoped)];
                    var getApplication = report.Methods[nameof(CacheSamples.GetApplication)];

                    pages.Add(ReportPage("cache", PageTitle("Cache", options: s => s.Description = l("Showcases the cache behavior")),
                        options: s =>
                        {
                            s.QueryParameters.Add(
                                Parameter("parameter",
                                    component: Select(l("Parameter"), Inline(new[] { "value_a", "value_b" }, requireLocalization: false)),
                                    options: s =>
                                    {
                                        s.Required = true;
                                        s.DefaultValue = "value_a";
                                    }
                                )
                            );
                            s.Tabs.Add(
                                ReportPageTab("default",
                                    options: s =>
                                    {
                                        s.Contents.AddRange(
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
                                                options: s => s.Narrow = true
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
                                                options: s => s.Narrow = true
                                            )
                                        ]);
                                    }
                                )
                            );
                        }
                    ));
                });

                pages.Add(MenuPage("specs",
                    options: s =>
                    {
                        s.FilterPageContextKey = "menu-page";
                        s.Header = PageTitle(
                            title: l("Specs"),
                            options: s =>
                            {
                                s.Description = l("All UI Specs are listed here");
                                s.Actions.Add(Filter(pageContextKey: "menu-page", options: s => s.Placeholder = l("Filter")));
                            }
                        );
                        s.Sections.AddRange(
                            specs.Select(section =>
                                MenuPageSection(
                                    options: s =>
                                    {
                                        s.Title = l(section.Name);
                                        s.Links.AddRange(
                                             section.Links.Select(link =>
                                                Filterable(
                                                    component: CardLink($"/specs/{link.Title.Kebaberize()}", l(link.Title), options: s =>
                                                    {
                                                        s.Icon = "pi pi-microchip";
                                                        s.Description = l(link.Description);
                                                    }),
                                                    options: s => s.Title = l(link.Title)
                                                )
                                            )
                                        );
                                    }
                                )
                            )
                        );
                    }
                ));
            });
        });
    }
}