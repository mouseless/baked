using Baked.Test.Caching;
using Baked.Test.Theme;
using Baked.Test.Theme.Custom;
using Baked.Theme;

namespace Baked;

public static class CustomThemeExtensions
{
    public static CustomThemeFeature Custom(this ThemeConfigurator _) =>
        new(
        [
            r => r.Index() with { Page = p => p.Described(d => d.Menu()) },
            r => r.Root("/cache-samples", "Cache", "pi pi-database") with { Page = p => p.Generated(g => g.From<CacheSamples>()), Description = "Showcases the cache behavior" },
            r => r.Root("/data-table-sample", "Data Table", "pi pi-table") with { Page = p => p.Generated(d => d.From<DataTableSample>()), Description = "Showcase DataTable component with scrollable and footer options" },
            r => r.Root("/form-sample", "Form", "pi pi-file-edit") with { Page = p => p.Generated(d => d.From<FormSample>()), Description = "Vibe coding form components" },
            r => r.Root("/report-page-sample", "Report", "pi pi-file") with { Page = p => p.Generated(g => g.From<ReportPageSample>()), Description = "Showcases a report layout with tabs and data panels"},
            r => r.Root("/specs", "Specs", "pi pi-list-check") with { Page = p => p.Described(d => d.Menu()), Description = "All UI Specs are listed here" },

            // Behavior
            r => r.Child("/specs/bake", "Bake", "/specs") with { Icon = "pi pi-microchip", Description = "The core component that renders a dynamic component using given descriptor", Section = "Behavior" },
            r => r.Child("/specs/custom-css", "Custom CSS", "/specs") with { Icon = "pi pi-microchip", Description = "Allow custom configuration to define custom css and more", Section = "Behavior" },
            r => r.Child("/specs/parameters", "Parameters", "/specs") with { Icon = "pi pi-microchip", Description = "Manage parameters through emits", Section = "Behavior" },
            r => r.Child("/specs/query-parameters", "Query Parameters", "/specs") with { Icon = "pi pi-microchip", Description = "Sync and manage parameters in query string", Section = "Behavior" },
            r => r.Child("/specs/toast", "Toast", "/specs") with { Icon = "pi pi-microchip", Description = "Render alert messages", Section = "Behavior" },

            // Display
            r => r.Child("/specs/card-link", "Card Link", "/specs") with { Icon = "pi pi-microchip", Description = "Renders a link as a big card-like button", Section = "Display" },
            r => r.Child("/specs/data-table", "Data Table", "/specs") with { Icon = "pi pi-microchip", Description = "View list data in a table", Section = "Display" },
            r => r.Child("/specs/nav-link", "Nav Link", "/specs") with { Icon = "pi pi-microchip", Description = "A component to give a link to a domain object", Section = "Display" },
            r => r.Child("/specs/icon", "Icon", "/specs") with { Icon = "pi pi-microchip", Description = "Displays built-in icons", Section = "Display" },
            r => r.Child("/specs/message", "Message", "/specs") with { Icon = "pi pi-microchip", Description = "A component to display message", Section = "Display" },
            r => r.Child("/specs/money", "Money", "/specs") with { Icon = "pi pi-microchip", Description = "Shortens and renders money values with the full value shown as tooltip", Section = "Display" },
            r => r.Child("/specs/number", "Number", "/specs") with { Icon = "pi pi-microchip", Description = "Shortens and renders numbers with the full value shown as tooltip", Section = "Display" },
            r => r.Child("/specs/rate", "Rate", "/specs") with { Icon = "pi pi-microchip", Description = "Render rate values as percentage", Section = "Display" },
            r => r.Child("/specs/text", "Text", "/specs") with { Icon = "pi pi-microchip", Description = "Render string values", Section = "Display" },

            // Input
            r => r.Child("/specs/language-switcher", "Language Switcher", "/specs") with { Icon = "pi pi-microchip", Description = "Allow change site language", Section = "Input" },
            r => r.Child("/specs/select", "Select", "/specs") with { Icon = "pi pi-microchip", Description = "Allow select from given options using drow down", Section = "Input" },
            r => r.Child("/specs/select-button", "Select Button", "/specs") with { Icon = "pi pi-microchip", Description = "Allow select from given options using buttons", Section = "Input" },

            // Layout
            r => r.Child("/specs/data-panel", "Data Panel", "/specs") with { Icon = "pi pi-microchip", Description = "Lazy load and view a data within a panel", Section = "Layout" },
            r => r.Child("/specs/header", "Header", "/specs") with { Icon = "pi pi-microchip", Description = "Renders a breadcrumb", Section = "Layout" },
            r => r.Child("/specs/page-title", "Page Title", "/specs") with { Icon = "pi pi-microchip", Description = "Render page title, desc and actions", Section = "Layout" },
            r => r.Child("/specs/side-menu", "Side Menu", "/specs") with { Icon = "pi pi-microchip", Description = "Renders application menu", Section = "Layout" },

            // Page
            r => r.Child("/specs/error-page", "Error Page", "/specs") with { Icon = "pi pi-microchip", Description = "Display errors in full page", Section = "Page" },
            r => r.Child("/specs/menu-page", "Menu Page", "/specs") with { Icon = "pi pi-microchip", Description = "Render navigation pages", Section = "Page" },
            r => r.Child("/specs/report-page", "Report Page", "/specs") with { Icon = "pi pi-microchip", Description = "Render report pages", Section = "Page" },

            // Plugins
            r => r.Child("/specs/auth", "Auth", "/specs") with { Icon = "pi pi-microchip", Description = "Authorized routing and client", Section = "Plugins" },
            r => r.Child("/specs/cache", "Cache", "/specs") with { Icon = "pi pi-microchip", Description = "Caches api responses in local storage", Section = "Plugins" },
            r => r.Child("/specs/locale", "Locale", "/specs") with { Icon = "pi pi-microchip", Description = "Allow locale customization and language support", Section = "Plugins" },
            r => r.Child("/specs/error-handling", "Error Handling", "/specs") with { Icon = "pi pi-microchip", Description = "Handling errors", Section = "Plugins" },
        ]);
}