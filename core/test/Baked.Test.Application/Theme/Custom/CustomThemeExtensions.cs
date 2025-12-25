using Baked.Test.Caching;
using Baked.Test.Orm;
using Baked.Test.Theme;
using Baked.Test.Theme.Custom;
using Baked.Theme;
using Baked.Ui;

namespace Baked;

public static class CustomThemeExtensions
{
    public static CustomThemeFeature Custom(this ThemeConfigurator _) =>
        new(
        [
            r => r.Index() with { Page = p => p.Described(d => d.Menu()) },
            r => r.Root("/cache-samples", "Cache", "pi pi-database") with { Page = p => p.Generated(g => g.Type<CacheSamples, TabbedPage>()), Description = "Showcases the cache behavior" },
            r => r.Root("/form-sample", "Form", "pi pi-file-edit") with { Page = p => p.Generated(g => g.Type<FormSample, SimplePage>()), Description = "Showcases action, form components and events" },
            r => r.Child("/form-sample/new-parent", "New Parent", "/form-sample") with { Page = p => p.Generated(g => g.Method<FormSample, FormPage>(nameof(FormSample.NewParent))), Description = "Fill out the form and hit save to create new parent" },
            r => r.Dynamic("/parents/[id]", "Parent") with { Page = p => p.Generated(g => g.Type<Parent, SimplePage>()), SideMenu = false },
            r => r.Root("/report-page-sample", "Report", "pi pi-file") with { Page = p => p.Generated(g => g.Type<ReportPageSample, TabbedPage>()), Description = "Showcases a report layout with tabs and data panels"},
            r => r.Dynamic("/route-parameters-sample/[id]", "Route Parameters") with { Page = p => p.Generated(g => g.Type<RouteParametersSample, SimplePage>()), Description = "Showcases the route parameter support" },
            r => r.Root("/specs", "Specs", "pi pi-list-check") with { Page = p => p.Described(d => d.Menu()), Description = "All UI Specs are listed here" },

            // Behavior
            r => r.Child("/specs/bake", "Bake", "/specs") with { Icon = "pi pi-microchip", Description = "The core component that renders a dynamic component using given descriptor", Section = "Behavior" },
            r => r.Child("/specs/conditional", "Conditional", "/specs") with { Icon = "pi pi-microchip", Description = "Allows rendering component based on configured condition", Section = "Behavior" },
            r => r.Child("/specs/contents", "Contents", "/specs") with { Icon = "pi pi-microchip", Description = "Allows multiple components to be rendered in a simple grid", Section = "Behavior" },
            r => r.Child("/specs/custom-css", "Custom CSS", "/specs") with { Icon = "pi pi-microchip", Description = "Allows custom configuration to define custom css and more", Section = "Behavior" },
            r => r.Child("/specs/inputs", "Inputs", "/specs") with { Icon = "pi pi-microchip", Description = "Manages inputs through emits", Section = "Behavior" },
            r => r.Child("/specs/inputs--query-bound", "Inputs - Query Bound", "/specs") with { Icon = "pi pi-microchip", Description = "Manages inputs in sync with query string", Section = "Behavior" },
            r => r.Child("/specs/layout", "Layout", "/specs") with { Icon = "pi pi-microchip", Description = "Selects specified layout for pages", Section = "Behavior" },
            r => r.Child("/specs/routing", "Routing", "/specs") with { Icon = "pi pi-microchip", Description = "Uses page descriptors as route template and extract parameters", Section = "Behavior" },
            r => r.Child("/specs/toast", "Toast", "/specs") with { Icon = "pi pi-microchip", Description = "Renders alert messages", Section = "Behavior" },

            // Display
            r => r.Child("/specs/card-link", "Card Link", "/specs") with { Icon = "pi pi-microchip", Description = "Renders a link as a big card-like button", Section = "Display" },
            r => r.Child("/specs/data-table", "Data Table", "/specs") with { Icon = "pi pi-microchip", Description = "Shows list data as a table", Section = "Display" },
            r => r.Child("/specs/icon", "Icon", "/specs") with { Icon = "pi pi-microchip", Description = "Displays built-in icons", Section = "Display" },
            r => r.Child("/specs/message", "Message", "/specs") with { Icon = "pi pi-microchip", Description = "A component to display message", Section = "Display" },
            r => r.Child("/specs/missing-component", "Missing Component", "/specs") with { Icon = "pi pi-microchip", Description = "Renders a helper in a place where a component was expected", Section = "Display" },
            r => r.Child("/specs/money", "Money", "/specs") with { Icon = "pi pi-microchip", Description = "Shortens and renders money values with the full value shown as tooltip", Section = "Display" },
            r => r.Child("/specs/nav-link", "Nav Link", "/specs") with { Icon = "pi pi-microchip", Description = "A component to give a link to a domain object", Section = "Display" },
            r => r.Child("/specs/number", "Number", "/specs") with { Icon = "pi pi-microchip", Description = "Shortens and renders numbers with the full value shown as tooltip", Section = "Display" },
            r => r.Child("/specs/rate", "Rate", "/specs") with { Icon = "pi pi-microchip", Description = "Renders rate values as percentage", Section = "Display" },
            r => r.Child("/specs/text", "Text", "/specs") with { Icon = "pi pi-microchip", Description = "Renders string values", Section = "Display" },

            // Form
            r => r.Child("/specs/simple-form", "Simple Form", "/specs") with { Icon = "pi pi-microchip", Description = "Showcases simple form component with inputs", Section = "Form" },

            // Input
            r => r.Child("/specs/button", "Button", "/specs") with { Icon = "pi pi-microchip", Description = "Showcases interactions using button component", Section = "Input" },
            r => r.Child("/specs/input-text", "Input Text", "/specs") with { Icon = "pi pi-microchip", Description = "Allows regular text input", Section = "Input" },
            r => r.Child("/specs/input-number", "Input Number", "/specs") with { Icon = "pi pi-microchip", Description = "Allows number input", Section = "Input" },
            r => r.Child("/specs/language-switcher", "Language Switcher", "/specs") with { Icon = "pi pi-microchip", Description = "Allows change site language", Section = "Input" },
            r => r.Child("/specs/select", "Select", "/specs") with { Icon = "pi pi-microchip", Description = "Allows select from given options using drow down", Section = "Input" },
            r => r.Child("/specs/select-button", "Select Button", "/specs") with { Icon = "pi pi-microchip", Description = "Allows select from given options using buttons", Section = "Input" },

            // Layout
            r => r.Child("/specs/data-panel", "Data Panel", "/specs") with { Icon = "pi pi-microchip", Description = "Lazy loads and views a data within a panel", Section = "Layout" },
            r => r.Child("/specs/header", "Header", "/specs") with { Icon = "pi pi-microchip", Description = "Renders a breadcrumb", Section = "Layout" },
            r => r.Child("/specs/dialog", "Dialog", "/specs") with { Icon = "pi pi-microchip", Description = "Renders a dialog with given contents and a button if given an action", Section = "Layout" },
            r => r.Child("/specs/page-title", "Page Title", "/specs") with { Icon = "pi pi-microchip", Description = "Renders page title, desc and actions", Section = "Layout" },
            r => r.Child("/specs/side-menu", "Side Menu", "/specs") with { Icon = "pi pi-microchip", Description = "Renders application menu", Section = "Layout" },

            // Page
            r => r.Child("/specs/error-page", "Error Page", "/specs") with { Icon = "pi pi-microchip", Description = "Displays errors in full page", Section = "Page" },
            r => r.Child("/specs/menu-page", "Menu Page", "/specs") with { Icon = "pi pi-microchip", Description = "Renders navigation pages", Section = "Page" },
            r => r.Child("/specs/simple-page", "Simple Page", "/specs") with { Icon = "pi pi-microchip", Description = "Renders a basic page with a title and contents", Section = "Page" },
            r => r.Child("/specs/tabbed-page", "Tabbed Page", "/specs") with { Icon = "pi pi-microchip", Description = "Renders more complicated pages with input in title and tab panel support", Section = "Page" },

            // Plugins
            r => r.Child("/specs/auth", "Auth", "/specs") with { Icon = "pi pi-microchip", Description = "Authorized routing and client", Section = "Plugins" },
            r => r.Child("/specs/cache", "Cache", "/specs") with { Icon = "pi pi-microchip", Description = "Caches api responses in local storage", Section = "Plugins" },
            r => r.Child("/specs/locale", "Locale", "/specs") with { Icon = "pi pi-microchip", Description = "Allows locale customization and language support", Section = "Plugins" },
            r => r.Child("/specs/error-handling", "Error Handling", "/specs") with { Icon = "pi pi-microchip", Description = "Handling errors", Section = "Plugins" },
        ]);
}