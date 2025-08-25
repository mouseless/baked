using Baked.Test.Orm;
using Baked.Test.Theme.Custom;

Bake.New
    .Service(
        business: c => c.DomainAssemblies(typeof(Entity).Assembly,
            baseNamespace: "Baked.Test",
            setNamespaceWhen: t => t.Namespace is not null && t.Namespace.StartsWith("Baked.Test.CodingStyle.NamespaceAsRoute")
        ),
        authentications: [
            c => c.Jwt(
                configurePlugin: plugin =>
                {
                    plugin.AnonymousPageRoutes.Add("^(?!.*auth).*$");
                    plugin.LoginPageRoute = "login";
                    plugin.RefreshApiRoute = "authentication-samples/refresh";
                }
            ),
            c => c.FixedBearerToken(
                tokens =>
                {
                    tokens.Add("AdminUI", claims: ["BaseA", "BaseB"]);
                    tokens.Add("Jane", claims: ["User", "BaseA", "BaseB"]);
                    tokens.Add("John", claims: ["User", "Admin", "BaseA", "BaseB"]);
                    tokens.Add("Postman", claims: ["User", "Admin", "BaseA", "BaseB"]);
                    tokens.Add("System", claims: ["BaseA", "BaseB"]);

                    tokens.Add("Authenticated", claims: []);
                    tokens.Add("BaseClaims", claims: ["BaseA", "BaseB"]);
                    tokens.Add("GivenClaims", claims: ["GivenA", "GivenB"]);
                    tokens.Add("GivenAndBaseClaims", claims: ["GivenA", "GivenB", "BaseA", "BaseB"]);
                    tokens.Add("ClassClaims", claims: ["GivenA", "GivenB", "BaseA", "BaseB"]);
                    tokens.Add("MethodOverClassClaims", claims: ["GivenC"]);
                },
                formPostParameters: ["additional"],
                documentNames: ["samples"]
            ),
            c => c.ApiKey()
        ],
        authorization: c => c.ClaimBased(
            claims: ["User", "Admin", "BaseA", "BaseB", "GivenA", "GivenB", "GivenC", "Refresh"],
            baseClaims: ["BaseA", "BaseB"]
        ),
        core: c => c.Dotnet(baseNamespace: "Baked.Test"),
        cors: c => c.AspNetCore(Settings.Required<string>("CorsOrigin:0"), Settings.Required<string>("CorsOrigin:1")),
        database: c => c
            .Sqlite()
            .ForProduction(c.PostgreSql()),
        exceptionHandling: c => c.ProblemDetails(typeUrlFormat: "https://baked.mouseless.codes/errors/{0}"),
        localization: c => c.Dotnet(language: new("en"), otherLanguages: [new("tr")]),
        theme: c => c.Custom(
            indexPage: Page.CreateIndex() with { Build = PageBuilders.Menu },
            pages:
            [
                Page.CreateRoot("/cache", "Cache", "pi pi-database") with { Build = PageBuilders.Cache, Description = "Showcases the cache behavior" },
                Page.CreateRoot("/data-table", "Data Table", "pi pi-table") with { Build = PageBuilders.DataTable, Description = "Showcase DataTable component with scrollable and footer options" },
                Page.CreateRoot("/report", "Report", "pi pi-file") with { Build = PageBuilders.Report, Description = "Showcases a report layout with tabs and data panels"},
                Page.CreateRoot("/specs", "Specs", "pi pi-list-check") with { Build = PageBuilders.Menu, Description = "All UI Specs are listed here" },

                // Behavior
                Page.CreateChild("/specs/bake", "Bake", "/specs") with { Icon = "pi pi-microchip", Description = "The core component that renders a dynamic component using given descriptor", Section = "Behavior" },
                Page.CreateChild("/specs/custom-css", "Custom CSS", "/specs") with { Icon = "pi pi-microchip", Description = "Allow custom configuration to define custom css and more", Section = "Behavior" },
                Page.CreateChild("/specs/parameters", "Parameters", "/specs") with { Icon = "pi pi-microchip", Description = "Manage parameters through emits", Section = "Behavior" },
                Page.CreateChild("/specs/query-parameters", "Query Parameters", "/specs") with { Icon = "pi pi-microchip", Description = "Sync and manage parameters in query string", Section = "Behavior" },
                Page.CreateChild("/specs/toast", "Toast", "/specs") with { Icon = "pi pi-microchip", Description = "Render alert messages", Section = "Behavior" },

                // Display
                Page.CreateChild("/specs/card-link", "Card Link", "/specs") with { Icon = "pi pi-microchip", Description = "Renders a link as a big card-like button", Section = "Display" },
                Page.CreateChild("/specs/data-table", "Data Table", "/specs") with { Icon = "pi pi-microchip", Description = "View list data in a table", Section = "Display" },
                Page.CreateChild("/specs/nav-link", "Nav Link", "/specs") with { Icon = "pi pi-microchip", Description = "A component to give a link to a domain object", Section = "Display" },
                Page.CreateChild("/specs/icon", "Icon", "/specs") with { Icon = "pi pi-microchip", Description = "Displays built-in icons", Section = "Display" },
                Page.CreateChild("/specs/message", "Message", "/specs") with { Icon = "pi pi-microchip", Description = "A component to display message", Section = "Display" },
                Page.CreateChild("/specs/money", "Money", "/specs") with { Icon = "pi pi-microchip", Description = "Shortens and renders money values with the full value shown as tooltip", Section = "Display" },
                Page.CreateChild("/specs/number", "Number", "/specs") with { Icon = "pi pi-microchip", Description = "Shortens and renders numbers with the full value shown as tooltip", Section = "Display" },
                Page.CreateChild("/specs/rate", "Rate", "/specs") with { Icon = "pi pi-microchip", Description = "Render rate values as percentage", Section = "Display" },
                Page.CreateChild("/specs/string", "String", "/specs") with { Icon = "pi pi-microchip", Description = "Render string values", Section = "Display" },

                // Input
                Page.CreateChild("/specs/language-switcher", "Language Switcher", "/specs") with { Icon = "pi pi-microchip", Description = "Allow change site language", Section = "Input" },
                Page.CreateChild("/specs/select", "Select", "/specs") with { Icon = "pi pi-microchip", Description = "Allow select from given options using drow down", Section = "Input" },
                Page.CreateChild("/specs/select-button", "Select Button", "/specs") with { Icon = "pi pi-microchip", Description = "Allow select from given options using buttons", Section = "Input" },

                // Layout
                Page.CreateChild("/specs/data-panel", "Data Panel", "/specs") with { Icon = "pi pi-microchip", Description = "Lazy load and view a data within a panel", Section = "Layout" },
                Page.CreateChild("/specs/header", "Header", "/specs") with { Icon = "pi pi-microchip", Description = "Renders a breadcrumb", Section = "Layout" },
                Page.CreateChild("/specs/page-title", "Page Title", "/specs") with { Icon = "pi pi-microchip", Description = "Render page title, desc and actions", Section = "Layout" },
                Page.CreateChild("/specs/side-menu", "Side Menu", "/specs") with { Icon = "pi pi-microchip", Description = "Renders application menu", Section = "Layout" },

                // Page
                Page.CreateChild("/specs/error-page", "Error Page", "/specs") with { Icon = "pi pi-microchip", Description = "Display errors in full page", Section = "Page" },
                Page.CreateChild("/specs/menu-page", "Menu Page", "/specs") with { Icon = "pi pi-microchip", Description = "Render navigation pages", Section = "Page" },
                Page.CreateChild("/specs/report-page", "Report Page", "/specs") with { Icon = "pi pi-microchip", Description = "Render report pages", Section = "Page" },

                // Plugins
                Page.CreateChild("/specs/auth", "Auth", "/specs") with { Icon = "pi pi-microchip", Description = "Authorized routing and client", Section = "Plugins" },
                Page.CreateChild("/specs/cache", "Cache", "/specs") with { Icon = "pi pi-microchip", Description = "Caches api responses in local storage", Section = "Plugins" },
                Page.CreateChild("/specs/locale", "Locale", "/specs") with { Icon = "pi pi-microchip", Description = "Allow locale customization and language support", Section = "Plugins" },
                Page.CreateChild("/specs/error-handling", "Error Handling", "/specs") with { Icon = "pi pi-microchip", Description = "Handling errors", Section = "Plugins" },
            ]
        ),
        configure: app =>
        {
            app.Features.AddReporting(c => c
                .NativeSql(basePath: "Reporting/Sqlite")
                .ForProduction(c.NativeSql(basePath: "Reporting/PostgreSql"))
            );
            app.Features.AddConfigurationOverrider();
        }
    )
    .Run();