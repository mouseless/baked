# Unreleased

## Features

- Dynamic routing is now supported and can be used when;
  - navigating through pages
  - fetching data from backend
- `Bake` now executes given `Action` defined in `ComponentDescriptor`
  implementations upon model change or `submit` event
- `Button` component is now added
- `useActionExecuter` composable is now added to execute `Composite`, `Local`,
  `Publish` and `Remote` actions with given configuration
  - `Page` component now provides an event bus to publish page-wide events
- `SimpleForm` component is now added for rendering a basic form with inputs
- `DataTable` now supports row based actions via `ActionTemplate` property
- `Bake` now supports reload and show/hide reactions
  - Use `ReloadOn` and `ShowOn` to bind them to an event
  - Use `ReloadWhen` and `ShowWhen` to bind them to a page context value
- `Constraints` now allows you to define constraints on values of triggers so
  that reactions can happen only on certain conditions
- `InputText` and `InputNumber` components are now added to be used in
  `SimpleForm`

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
  - `Injected()` is now removed, use `Context` property
    - Use `Context.Model(...)` to access model data during actions
    - `Parent` injected data now has `data` and `parameters` properties
      ```csharp
      Context.Parent(options: cd => cd.Prop = "parameters")
      ```
    - Use `Context.Page(...)` to access to page context values
    - Use `Context.Response(...)` to access to remote action's response which is
      available only for the post action of a remote action
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
- `DataTable.Column.Prop` is renamed to `Key`
- `DataTable.Component` type is changed to `IComponentDescriptor`
- `Conditional` is changed from `Schema` to `Component`
- `*PageContextKey` properties are now removed from components and schemas
  - use the new publish action to publish values to page context
    ```csharp
    component.Schema.PageContextKey = "key" // old usage
    component.Action = Publish.PageContextValue("key"); // new usage
    ```
  - use the new reaction system to subscribe to page context changes
    ```csharp
    component.Schema.ShowWhen = "key"; // old usage
    component.ShowWhen("key"); // new usage

    component.Schema.ShowWhen = "key:value"; // old usage
    component.ShowWhen("key", Is("value")); // new usage

    component.Schema.ShowWhen = "!key:value"; // old usage
    component.ShowWhen("key", IsNot("value")); // new usage
    ```
- `ReportPage.Tab.Content` support for `showWhen` is completely removed, use its
  component's reaction system to hide a content
  - `lg:col-span-2` class is now passed directly to content's component instead
    of a wrapper `div`
- In `useContext` composable, `injectPage` and `providePage` are renamed to
  `injectPageContext` and `providePageContext` respectively
- `Inputs` now doesn't have a layout styling, any component that uses it should
  wrap it and introduce `flex` styling

## Improvements

- `Parameters` now accept parameter class attribute for each parameter
- `RemoveComponent` and `RemoveSchema` helper extensions are now added
- `AwaitLoading` utility component is now added which contains slots to help
  rendering skeleton and content according to `loading` state
- `InjectedData`, now `ContextData`, now has `TargetProp` property to map give
  `Prop` key value to corresponding property
- `UiLayer` now has `MinConventionOrder` and `MaxConventionOrder` to allow
  inserting conventions before or after all conventions
- `MissingComponent` component is now added when a component is required but
  none was configured
  - It also leaves a post-build warning that includes the domain source name and
    the component path
- `MissingComponent` component now contains a sample code to help developer add
  the missing component to the path
- `useContext` now has `injectContextData` helper to get all the default context
  data `useDataFetcher` requires, so that you can pass
  `context.injectContextData()` directly to `contextData` option when fetching
  data using `useDataFetcher`
- `Layout` now supports app-wide `pageContext` and `events` that are different
  from those coming from `Page` which are page-wide
- `IAction` and `IData` now implement `+` operator to easily convert them into a
  `CompositeAction` and `CompositeData`
  ```csharp
  component.Data += Context.Parent(options: cd => cd.Prop = "parameters");
  ```
- `Inputs` has become a pure utility component after removing wrapper div and
  `flex` styling
