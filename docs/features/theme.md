# Theme

This feature implementations configures `DomainModel` metadata attributes and
extracts component descriptors for ui theme pages and components of an
application.

## Default

Default Theme feature introduces many components and sets defaults for the
domain model. It also configures error pages, layouts, and pages.

To use this theme directly without any update use this;

```csharp
c => c.Default(
    index: ...,
    routes: [...],
    errorPageOptions: ep => ...,
    sideMenuOptions: sm => ...,
    headerOptions: h => ...
)
```

Or you may prefere to create your custom theme on top of this default theme;

```csharp
public static class CustomThemeExtensions
{
    public static CustomThemeFeature Custom(this ThemeConfigurator _) =>
        new(
        [
            // All app routes
        ]);
}

public class CustomThemeFeature(IEnumerable<Func<Router, Route>> _routes)
    : DefaultThemeFeature(_routes.Select(r => r(new())))
{
    public override void Configure(LayerConfigurator configurator)
    {
        // Applies default theme rules
        base.Configure(configurator);

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // Your custom conventions and page overrides
        });

        configurator.ConfigureComponentExports(c =>
        {
            // Add your component exports using your own `Components` extensions
            // c.AddFromExtensions(typeof(Components));
        });

        configurator.ConfigurePageDescriptors(pages =>
        {
            // Add other pages like `auth/login.vue` etc.
        });
    }
}
```

Below you can find the rules that comes with this theme. For other user
experiences, see [UX Feature](ux.md)

| Group      | Rules                                                                                      |
| ---        | ---                                                                                        |
| Property   | - All public properties get a `DataAttribute` with a camelized name and titleized label    |
|            | - Default component is `None`                                                              |
|            | - `string` and `Guid` properties render with `String`                                      |
| Method     | - All actions with `ActionModelAttribute` get a `TabAttribute`                             |
|            | - Each action is wired as a remote method with `MethodRemote`                              |
| Parameter  | - Parameters with `ParameterModelAttribute` use `Parameter` schema                         |
|            | - Required and default values are taken from the attribute                                 |
| Enum       | - Enum types render inline with `EnumInline`                                               |
| Data Table | - Default rows set to 5                                                                    |
|            | - Paginator enabled                                                                        |
|            | - Export option added for methods with `DataTable`                                         |
|            | - Properties with `DataAttribute` render as `DataTable.Column`                             |
|            | - Column titles use the label of the property and are exportable                           |
| Error Page | - Error page includes safe links for routes with `ErrorSafeLink`                           |
|            | - Predefined error messages:                                                               |
|            |   - `403` Access Denied                                                                    |
|            |   - `404` Page Not Found                                                                   |
|            |   - `500` Unexpected Error                                                                 |
|            | - Messages can be customized with `errorPageOptions:`                                      |
| Layouts    | - `DefaultLayout` includes                                                                 |
|            |   - Side menu with routes marked for `SideMenu`                                            |
|            |   - Header sitemap with enabled routes                                                     |
|            | - Both side menu and header can be customized with `sideMenuOptions:` and `headerOptions:` |
|            | - `ModalLayout` also included                                                              |
| Pages      | - Builds pages from routes using domain model and localization                             |
|            | - Each route becomes a page if it can be resolved                                          |
