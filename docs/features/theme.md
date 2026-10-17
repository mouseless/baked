# Theme

This feature implementations configures `DomainModel` metadata attributes and
extracts component descriptors for ui theme pages and components of an
application.

Add this feature using `AddTheme()` extension;

```csharp
app.Features.AddTheme(...);
```

This feature abstraction provides following extensions to
`DomainModelConventionCollection`;

- Executes after building index and `Order` is defaulted to `Theme.Defaults.Add`
  ```csharp
  conventions.AddTypeComponent(...);
  conventions.AddPropertyComponent(...);
  conventions.AddMethodComponent(...);
  conventions.AddParameterComponent(...);

  conventions.AddTypeSchema(...);
  conventions.AddPropertySchema(...);
  conventions.AddMethodSchema(...);
  conventions.AddParameterSchema(...);

  conventions.RemoveTypeComponent(...);
  conventions.RemovePropertyComponent(...);
  conventions.RemoveMethodComponent(...);
  conventions.RemoveParameterComponent(...);

  conventions.RemoveTypeSchema(...);
  conventions.RemovePropertySchema(...);
  conventions.RemoveMethodSchema(...);
  conventions.RemoveParameterSchema(...);
  ```
- Executes after building index and `Order` is defaulted to
  `Theme.Defaults.Configure`
  ```csharp
  conventions.EditTypeComponent(...);
  conventions.EditPropertyComponent(...);
  conventions.EditMethodComponent(...);
  conventions.EditParameterComponent(...);

  conventions.EditTypeSchema(...);
  conventions.EditPropertySchema(...);
  conventions.EditMethodSchema(...);
  conventions.EditParameterSchema(...);
  ```

> [!TIP]
>
> See [Layers / Domain / Ordering Conventions](../layers/domain.md#ordering-conventions)
> for more information on convention order mechanism

Below you can find sample for adding convention using extensions;

```csharp
configurator.Domain.ConfigureConventions(conventions =>
{
    // Adding component via extensions
    conventions.AddMethodComponent<Button>(
        when: c => c.Method.Name is "Login"
    );

    // Adding component configuration via extensions
    conventions.EditMethodComponent<Button>(
        when: c => c.Method.Name is "Login",
        component: b => ...
    );

    // Adding schema via extensions
    conventions.AddPropertySchema<Input>(
        when: c => c.Property.Name is "Id"
    );

    // Adding schema configuration via extensions
    conventions.EditPropertySchema<Input>(
        when: c => c.Property.Name is "Id",
        schema: i => ...
    );
}
```

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
    headerOptions: h => ...,
    debugComponentPaths: ...,
    configure: configurator => ...
)
```

Or you may prefer to create your custom theme on top of this default theme. Give
your custom configuration to `configure`, it runs after default theme rules;

```csharp
public static class CustomThemeFeature
{
    public static IFeature<ThemeFeature.Configurator> Custom(this ThemeFeature.Configurator x) => x.Default(
        index: r => r.Index(),
        routes:
        [
            // All app routes
        ],
        configure: Configure
    );

    static void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            // Your custom conventions and page overrides
        });

        configurator.Ui.ConfigureComponentExports(exports =>
        {
            // Add component exports from your own theme assembly
            exports.AddFromAssembly(typeof(CustomThemeFeature));
        });

        configurator.Ui.ConfigurePageDescriptors(pages =>
        {
            // Add other pages like `auth/login.vue` etc.
        });
    }
}
```

### Default Rules

Below you can find the rules that come with this theme. For other user
experiences, see [UX Feature](ux.md).

#### Displaying Properties

| Property Type                                    | Component                                    |
| ---                                              | ---                                          |
| `string`, `Guid`, `MailAddress`                  | `Text`                                       |
| types with `Locatable` or `Primitive` attribute  | `Text`                                       |
| enums                                            | `Tag`, with localized text and severity      |
| `Uri`                                            | `TextLink`                                   |
| `bool`                                           | `Check`                                      |
| `DateOnly`                                       | `Date`, as `dd-MM-yyyy`                      |
| `DateTime`                                       | `Date`, as `dd-MM-yyyy HH:mm:ss`             |

#### Editing Parameters

| Parameter Type                                   | Input                                        |
| ---                                              | ---                                          |
| `string`, types with `Primitive` attribute       | `InputText`                                  |
| `int`, `long`                                    | `InputNumber`                                |
| `decimal`                                        | `InputMoney`                                 |
| `double`                                         | `InputRate`                                  |
| `bool`                                           | `InputCheckbox`, indeterminate when nullable |
| `Uri`                                            | `InputUrl`                                   |
| `MailAddress`                                    | `InputMailAddress`                           |
| `DateOnly`                                       | `InputDate`, with a date picker              |

#### Types

- `SimplePage` and `TabbedPage` are added for any type at component path
  `/page/*`
- Tab titles are localized and titleized when there are more than one tabs
- `[id]` route parameter is mapped to first data property with `IdProperty`
  attribute
- `Fieldset` title is taken from the property with `Label` attribute
- `NavLink` path is taken from `UiRoute` attribute of the type
- Enum types render inline with `InlineData` using localized labels, and their
  severities are exported to `severities.json` to color their `Tag`

#### Properties

- All public properties get a `UiData` attribute with a camelized name and
  titleized label
- Properties with `IdProperty` attribute are hidden
- Data table columns and fieldset fields render the component of their property
- Property dialogs use titleized property name as their header

#### Methods

- All actions with `ApiAction` attribute get `UiAction` attribute
- `FormPage` is added for any method at component path `/page/*/*`
- `Content` schema is added to any method, and `SimpleForm` is added for
  non-`GET` actions under contents
- Each method is wired as a remote data, and each action as a remote action
  - Remote data of locatable types include `route.params` in their route
  - `Context.Model` is the body of remote actions with parameters
  - Remote actions publish an event named after the method with the response
    as data
  - `GET`, `DELETE` and `TRACE` remote actions are not allowed to have a body
- Buttons are labeled with titleized method names, their dialogs use the same
  as header and render a form or a content
- Buttons in `DataContainer` actions are small
- Data table export uses `;` as CSV separator, `{Method}.ExportFileName` and
  `{Method}.FooterLabel` are the localization keys of file name and footer

#### Parameters

- Parameters are rendered as `Input` list under `SimpleForm` and `FormPage`,
  parameters with `ApiParameter` attribute use `Input` schema
- Non-nullable parameters are required, default values are taken from
  parameters and enum defaults are camelized
- Numeric parameters are marked as numeric
- Input labels show optionality under `SimpleForm` and `FormPage`

#### Selects

- `Select` shows clear and `SelectButton` allows empty for nullable parameters
- Data is taken from the parameter or its type, e.g., enum inline data
- Required selects and select buttons under `DataPanel` select their first item
  automatically
- `SelectButton` labels are hidden unless they are in `ifta` mode

#### Page Title

- `PageTitle` is added for any type at component path `/page/*/*page/title`,
  and for any method at `/page/*/*/*page/title`
- Actions of type, except `GET` and initializers, are added to page title
  actions, including `route.params` for locatable types
- Route icon is rendered as an `Avatar` for locatable and singleton types

#### Error Page

- Error page includes safe links for routes with `ErrorSafeLink`
- Predefined error messages are `403` Access Denied, `404` Page Not Found, `500`
  Unexpected Error and `999` Application Error, they can be customized with
  `errorPageOptions:`

#### Layouts and Pages

- `DefaultLayout` includes a side menu with routes marked for `SideMenu` and a
  header sitemap with enabled routes, customizable with `sideMenuOptions:` and
  `headerOptions:`
- `ModalLayout` is also included
- Pages are built from routes using domain model and localization, each route
  becomes a page if it can be resolved

### Menu and Routes

`routes:` list is a list of builder functions that takes `Router` as a parameter
and returns `Route` instance. Three types of route is supported out of the box.

```csharp
r => r.Index() with { ... },
r => r.Root("/my-parent", "MyParent", "pi pi-user") with { ... },
r => r.Child("/my-parent/my-child", "MyChild", "/my-parent") with { ... },
```

A route only defines a page. To render a type as a link to its page, or a method
as a button that navigates to a page, give them `UiRoute` attribute using below
extensions;

```csharp
configurator.Domain.ConfigureConventions(conventions =>
{
    conventions.SetTypeRoute<MyParent>("/my-parent/[id]");
    conventions.SetMethodRoute<MyParents>(nameof(MyParents.New), "/my-parent/new");
});
```

> [!TIP]
>
> See [Routed Types as Nav Links](ux.md#routed-types-as-nav-links) and
> [Actions as Buttons](ux.md#actions-as-buttons)

### Page Types

There are three types of page, `.Implemented()`, `.Described(d => ...)` and
`.Generated(g => ...)`. Each route has a `Page` property that defines what page
to render at that route.

By default pages use `.Implemented()`, meaning you need to have a `.vue` page at
the given route, e.g. for `/my-parent` route to work you need to have
`/pages/my-parent.vue` file in the UI project.

```csharp
r => r.Root("/my-parent", "MyParent", "pi pi-user") with { Page = p => p.Implemented() }

// or

r => r.Root("/my-parent", "MyParent", "pi pi-user")
```

`.Described(d => ...)` is appropriate when you decide to fully describe the page
manually from your theme feature. Add an extension method to `Page.Describer`
for your custom page and use it in routes.

```csharp
r => r.Root("/my-parent", "MyParent", "pi pi-user") with { Page = p => p.Described(d => d.MyParent()) }
```

`.Generated(g => ...)` is appropriate when you have necessary conventions to
generate a page descriptor out of a domain model.

```csharp
r => r.Root("/my-parent", "MyParent", "pi pi-user") with { Page = p => p.Generated(g => g.Type<MyParent, SimplePage>()) }
```

### Debugging Page Generation

We provide two tools to debug page generation process during a generate phase.

#### Debugging Component Paths

Enable path debugging to see which component paths get hit during page
generation.

To enable;

```csharp
debugComponentPaths: true
```

To filter a specfic path;

```csharp
debugComponentPaths: new() { Filter = path => path.EndsWith(...) }
```

To include full paths next to each node;

```csharp
debugComponentPaths: new() { IncludeFullPaths = true }
```

Combined;

```csharp
debugComponentPaths: new()
{
    Filter = path => path.EndsWith(...),
    IncludeFullPaths = true
}
```

#### Inspecting Values

> [!WARNING]
>
> This feature is still in experimentation and might print false-negative
> output, meaning it might not capture every change of the inspected component
> or schema.
>
> E.g., A described page or component will not be inspected. If you describe a
> child component or schema from the convention of parent component or schema it
> won't be inspected.

Configure an inspect to watch a property value of a component or a schema so
that you can see which UX or Theme feature sets what value and in which order.

```csharp
configurator.Domain.ConfigureBuilder(builder =>
{
    // To inspect a component or a schema on types
    builder.Inspect.TypeComponent<MyComponent>( // or inspect.TypeSchema
        when: c => c.Type..., // optional to inspect specific type models
        where: cc => cc.Path..., // optional to inspect specific component paths
        component: mc => mc.Value // optional to inspect just this value
    );

    // To inspect a component or a schema on properties
    builder.Inspect.PropertyComponent<MyComponent>( // or inspect.PropertySchema
        when: c => c.Property..., // optional to inspect specific property models
        where: cc => cc.Path..., // optional to inspect specific component paths
        component: mc => mc.Value // optional to inspect just this value
    );

    // To inspect a component or a schema on methods
    builder.Inspect.MethodComponent<MyComponent>( // or inspect.MethodSchema
        when: c => c.Method..., // optional to inspect specific method models
        where: cc => cc.Path..., // optional to inspect specific component paths
        component: mc => mc.Value // optional to inspect just this value
    );

    // To inspect a component or a schema on parameters
    builder.Inspect.ParameterComponent<MyComponent>( // or inspect.ParameterSchema
        when: c => c.Parameter..., // optional to inspect specific parameter models
        where: cc => cc.Path..., // optional to inspect specific component path
        component: mc => mc.Value // optional to inspect just this value
    );

    // To inspect a component or a schema on any member
    builder.Inspect.Component<MyComponent>( // or inspect.Schema
        when: c => c..., // optional to inspect specific members
        where: cc => cc.Path..., // optional to inspect specific component paths
        component: mc => mc.Value // optional to inspect just this value
    );
});
```

> [!NOTE]
>
> Only one inspect is allowed. If you configure more than one,
> `InvalidOperationException` will be thrown
