# Theme

This feature implementations configures `DomainModel` metadata attributes and
extracts component descriptors for ui theme pages and components of an
application.

## Admin

Admin Theme feature introduces many components and sets defaults for the domain
model. It also configures error pages, layouts, and pages.

```csharp
c => c.Admin(
    index: ...,
    routes: [...],
    errorPageOptions: ep => ...,
    sideMenuOptions: sm => ...,
    headerOptions: h => ...
)
```

### Defaults

- Property
  - All public properties get a `DataAttribute` with a camelized name and
    titleized label
  - Default component is `None`
  - `string` and `Guid` properties render with `String`
- Method
  - All actions with `ActionModelAttribute` get a `TabAttribute`
  - Each action is wired as a remote method with `MethodRemote`
- Parameter
  - Parameters with `ParameterModelAttribute` use `Parameter` schema
  - Required and default values are taken from the attribute
- Enum
  - Enum types render inline with `EnumInline`
- Data Table
  - Default rows set to 5
  - Paginator enabled
  - Export option added for methods with `DataTable`
  - Properties with `DataAttribute` render as `DataTable.Column`
  - Column titles use the label of the property and are exportable
- Error Page
  - Error page includes safe links for routes with `ErrorSafeLink`
  - Predefined error messages:
    - `403` Access Denied
    - `404` Page Not Found
    - `500` Unexpected Error
  - Messages can be customized with `errorPageOptions:`
- Layouts
  - `DefaultLayout` includes:
    - Side menu with routes marked for `SideMenu`
    - Header sitemap with enabled routes
  - Both side menu and header can be customized with `sideMenuOptions:` and
    `headerOptions:`
  - `ModalLayout` also included
- Pages
  - Builds pages from routes using domain model and localization
  - Each route becomes a page if it can be resolved
