# Unreleased

## Features

- Dynamic routing is now supported and can be used when;
  - navigating through pages
  - fetching data from backend
- `Bake` now executes given `Action` defined in `ComponentDescriptor`
  implementations upon model change or `submit` event
- `Button` component is now added
- `useActionExecuter` is now added which is a composable that executes `Emit`,
  `PageContext`, `Local`, `Remote` or `Composite` actions with given
  configuration
- `Page` component now provides an event bus to publish page-wide events
- `SimpleForm` component is now added for rendering a basic form with inputs
- `Bake` now supports reload and show/hide reactions
  - Use `ReloadOn` and `ShowOn` to bind them to an `Emit` action event
  - Use `ReloadWhen` and `ShowWhen` to bind them to a `PageContext` value change

## Breaking Changes

- `[...baked].vue` page is now not used, `*.page.json` file paths are used as
  route patterns and rendered directly with `Page.vue`
- `baseURL` is renamed to `apiBaseUrl` and config is now set in root of `bake`
  module options and no longer awailable through `dataFetcher`
- `Parameter` schema is renamed to `Inputs`
  - `ParameterParameter` domain component helper is renamed to `ParameterInput`
  - `Parameter` component helper is renamed to `Input`
- `DataPanel.Parameters` property is renamed to `Inputs`
- `ReportPage.QueryParameters` property is renamed to `Inputs`
- `Parameters.vue` is renamed to `Inputs.vue`
- `QueryParameters.vue` is renamed to `QueryBoundInputs.vue`
- `TypeWithOnlyGetIsReportPage` UX feature is removed, and adding `ReportPage`
  component to a type is moved to `DefaultThemeFeature`
- `InjectedData` is renamed to `ContextData`
  - `Injected()` is now removed, use `Parent` and `Model` factory methods
    - `Parent` injected data now has `data` and `parameters` properties
    ```csharp
    Context.Parent(options: cd => cd.Prop = "parameters")
    ```
  - Data keys are removed
  - `Prop` now supports property chaining
  - `useContext` methods;
    - `injectData` is renamed to `injectParentContext`
     - `provideData` is renamed to `provideParentContext`

- `ComputedData.Args` is now changed to `Options` with `IData` type
  - Built-in composables now have object parameters with named fields
- `Composables` now provide helpers instead of ui composable file keys
  ```csharp
  data: Computed(Composables.UseError) // old usage
  data: Composables.UseError() // new usage
  ```
- `useRoute` composable now accepts property name as parameter to access
  `params`, `query`
- `useQuery` composable is now removed, use `useRoute` composable with `query`
  option
  ```csharp
  {
    data = Computed(Composables.UseQuery) // old usage
    data = Computed.UseRoute("query") // new usage
  }
  ```
- `None` is renamed to `MissingComponent`
- `*PageContextKey` properties are now removed from components and schemas
  - use the new page context action to publish values to page context
    ```csharp
    component.PageContextKey = "key"
    component.Action = PageContext("key");
    ```
  - use the new reaction system to subscribe to page context changes
    ```csharp
    content.ShowWhen = "key"; // old usage
    content.ShowWhen("key"); // new usage

    content.ShowWhen = "key:value"; // old usage
    content.ShowWhen("key", constraint: Is("value")); // new usage

    content.ShowWhen = "!key:value"; // old usage
    content.ShowWhen("key", constraint: IsNot("value")); // new usage
    ```

## Improvements

- `Parameters` now accept parameter class attribute for each parameter
- `RemoveComponent` and `RemoveSchema` helper extensions are now added
- `AwaitLoading` utility component is now added which contains slots to help
  rendering skeleton and content according to `loading` state
- `InjectedData`, now `ContextData` now has `TargetProp` property to map give
  `Prop` key value to corresponding property
- `UiLayer` now has `MinConventionOrder` and `MaxConventionOrder` to allow
  inserting conventions before or after all conventions
- `MissingComponent` component is now added when a component is required but
  none was configured
  - It also leaves a post-build warning that includes the domain source name and
    the component path
- `MissingComponent` component now contains a sample code to help developer add
  the missing component to the path
