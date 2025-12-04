# Unreleased

## Features

- Dynamic routing is now supported and can be used when;
  - navigating through pages
  - fetching data from backend
- `Bake` now executes given `Action` and `PostAction` defined in
  `ComponentDescriptor` implementations
  ```javascript
  async function onClick() {
    emit("submit");
  }
  ```
- `Button` component is now added
- `useActionExecuter` is now added which is a composable that executes `Emit`,
  `Local`, `Remote` or `Composite` actions with given configuration

## Breaking Changes

- `[...baked].vue` page is now not used, `*.page.json` file paths are used as
  route patterns and rendered directly with `Page.vue`
- `useRoute` composable now accepts property name as parameter to access
  `params`, `query`
  - `useQuery` composable is now deprecated and will be removed in
  further releases
  ```csharp
  {
    // Obsolete
    data = Computed(Composables.UseQuery)

    // Use `UseRoute` with args
    data = Computed(Composables.UseRoute, options: o => o.Args.Add("params"))
  }
  ```
- `baseURL` is renamed to `apiBaseUrl` and config is now set in root of `bake`
  module options and no longer awailable through `dataFetcher`
- `Parameter` schema is renamed to `Inputs`
  - `ParameterParameter` domain component helper is renamed to `ParameterInput`
  - `Parameter` component helper is renamed to `Input`
- `DataPanel.Parameters` property is renamed to `Inputs`
- `ReportPage.QueryParameters` property is renamed to `Inputs`
- `Parameters.vue` is renamed to `Inputs.vue`
- `QueryParameters.vue` is renamed to `QueryBoundInputs.vue`

## Improvements

- `Parameters` now accept parameter class attribute for each parameter
- `RemoveComponent` and `RemoveSchema` helper extensions are now added
- `AwaitLoading` utility component is now added which contains slots to help
  rendering skeleton and content according to `loading` state
- `UiLayer` now has `MinConventionOrder` and `MaxConventionOrder` to allow
  inserting conventions before or after all conventions
- `None` component is now added as a default when no component was configured
- `None` component now contains a sample code to help developer add the missing
  component to the path
