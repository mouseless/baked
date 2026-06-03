using Baked.Playground.Caching;
using Baked.Playground.Orm;
using Baked.Playground.Theme;
using Baked.Playground.Theme.Custom;
using Baked.Theme;
using Baked.Ui;

namespace Baked;

public static class CustomThemeExtensions
{
    extension(ThemeConfigurator _)
    {
        public CustomThemeFeature Custom() =>
            new(
            [
                r => r.Index() with { Page = p => p.Described(d => d.Menu()) },
                r => r.Root<CacheSamples, TabbedPage>("/cache-samples", "pi pi-database"),
                r => r.Root<FormSample, SimplePage>("/form-sample", "pi pi-file-edit"),
                r => r.Child<FormSample, FormPage>("/form-sample/parents/new", "/form-sample", nameof(FormSample.NewParent)),
                r => r.Root<ReportPageSample, TabbedPage>("/report-page-sample", "pi pi-file"),
                r => r.Root("/specs", "pi pi-list-check") with { Page = p => p.Described(d => d.Menu()) },

                r => r.RootDynamic<Parent, SimplePage>("/parents/[id]"),
                r => r.RootDynamic<RouteParametersSample, SimplePage>("/route-parameters-sample/[id]"),

                // Behavior
                r => r.Child("/specs/await-loading", "/specs") with { Icon = "pi pi-microchip", Section = "Behavior" },
                r => r.Child("/specs/bake", "/specs") with { Icon = "pi pi-microchip", Section = "Behavior" },
                r => r.Child("/specs/composite", "/specs") with { Icon = "pi pi-microchip", Section = "Behavior" },
                r => r.Child("/specs/contents", "/specs") with { Icon = "pi pi-microchip", Section = "Behavior" },
                r => r.Child("/specs/custom-css", "/specs") with { Icon = "pi pi-microchip", Section = "Behavior" },
                r => r.Child("/specs/inputs", "/specs") with { Icon = "pi pi-microchip", Section = "Behavior" },
                r => r.Child("/specs/inputs--form-mode", "/specs") with { Icon = "pi pi-microchip", Section = "Behavior" },
                r => r.Child("/specs/inputs--query-bound", "/specs") with { Icon = "pi pi-microchip", Section = "Behavior" },
                r => r.Child("/specs/layout", "/specs") with { Icon = "pi pi-microchip", Section = "Behavior" },
                r => r.Child("/specs/redirect", "/specs") with { Icon = "pi pi-microchip", Section = "Behavior" },
                r => r.Child("/specs/routing", "/specs") with { Icon = "pi pi-microchip", Section = "Behavior" },
                r => r.Child("/specs/toast", "/specs") with { Icon = "pi pi-microchip", Section = "Behavior" },

                // Display
                r => r.Child("/specs/card-link", "/specs") with { Icon = "pi pi-microchip", Section = "Display" },
                r => r.Child("/specs/data-table", "/specs") with { Icon = "pi pi-microchip", Section = "Display" },
                r => r.Child("/specs/fieldset", "/specs") with { Icon = "pi pi-microchip", Section = "Display" },
                r => r.Child("/specs/icon", "/specs") with { Icon = "pi pi-microchip", Section = "Display" },
                r => r.Child("/specs/message", "/specs") with { Icon = "pi pi-microchip", Section = "Display" },
                r => r.Child("/specs/missing-component", "/specs") with { Icon = "pi pi-microchip", Section = "Display" },
                r => r.Child("/specs/money", "/specs") with { Icon = "pi pi-microchip", Section = "Display" },
                r => r.Child("/specs/nav-link", "/specs") with { Icon = "pi pi-microchip", Section = "Display" },
                r => r.Child("/specs/number", "/specs") with { Icon = "pi pi-microchip", Section = "Display" },
                r => r.Child("/specs/rate", "/specs") with { Icon = "pi pi-microchip", Section = "Display" },
                r => r.Child("/specs/text", "/specs") with { Icon = "pi pi-microchip", Section = "Display" },

                // Form
                r => r.Child("/specs/simple-form", "/specs") with { Icon = "pi pi-microchip", Section = "Form" },
                r => r.Child("/specs/form-validation", "/specs") with { Icon = "pi pi-microchip", Section = "Form" },

                // Input
                r => r.Child("/specs/button", "/specs") with { Icon = "pi pi-microchip", Section = "Input" },
                r => r.Child("/specs/input-text", "/specs") with { Icon = "pi pi-microchip", Section = "Input" },
                r => r.Child("/specs/input-number", "/specs") with { Icon = "pi pi-microchip", Section = "Input" },
                r => r.Child("/specs/labeler", "/specs") with { Icon = "pi pi-microchip", Section = "Input" },
                r => r.Child("/specs/language-switcher", "/specs") with { Icon = "pi pi-microchip", Section = "Input" },
                r => r.Child("/specs/paginator", "/specs") with { Icon = "pi pi-microchip", Section = "Input" },
                r => r.Child("/specs/select", "/specs") with { Icon = "pi pi-microchip", Section = "Input" },
                r => r.Child("/specs/select-button", "/specs") with { Icon = "pi pi-microchip", Section = "Input" },

                // Layout
                r => r.Child("/specs/data-container", "/specs") with { Icon = "pi pi-microchip", Section = "Layout" },
                r => r.Child("/specs/data-panel", "/specs") with { Icon = "pi pi-microchip", Section = "Layout" },
                r => r.Child("/specs/header", "/specs") with { Icon = "pi pi-microchip", Section = "Layout" },
                r => r.Child("/specs/dialog", "/specs") with { Icon = "pi pi-microchip", Section = "Layout" },
                r => r.Child("/specs/page-title", "/specs") with { Icon = "pi pi-microchip", Section = "Layout" },
                r => r.Child("/specs/side-menu", "/specs") with { Icon = "pi pi-microchip", Section = "Layout" },

                // Page
                r => r.Child("/specs/error-page", "/specs") with { Icon = "pi pi-microchip", Section = "Page" },
                r => r.Child("/specs/form-page", "/specs") with { Icon = "pi pi-microchip", Section = "Page" },
                r => r.Child("/specs/menu-page", "/specs") with { Icon = "pi pi-microchip", Section = "Page" },
                r => r.Child("/specs/simple-page", "/specs") with { Icon = "pi pi-microchip", Section = "Page" },
                r => r.Child("/specs/tabbed-page", "/specs") with { Icon = "pi pi-microchip", Section = "Page" },

                // Plugins
                r => r.Child("/specs/auth", "/specs") with { Icon = "pi pi-microchip", Section = "Plugins" },
                r => r.Child("/specs/cache", "/specs") with { Icon = "pi pi-microchip", Section = "Plugins" },
                r => r.Child("/specs/locale", "/specs") with { Icon = "pi pi-microchip", Section = "Plugins" },
                r => r.Child("/specs/error-handling", "/specs") with { Icon = "pi pi-microchip", Section = "Plugins" },
            ]);
    }

    extension(List<ValidationComposable> validations)
    {
        public void AddFormSampleValidation() =>
            validations.Add(new("useFormSampleValidation"));
    }
}