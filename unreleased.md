# Unreleased

## Features

- Dynamic routing is now supported and can be used when;
  - navigating through pages, `r => r.ChildDynamic(...)`
  - fetching data from backend, `rd.Params = Computed.UseRoute("params");`
  - posting data to backend, `ra.Params = Computed.UseRoute("params");`
- Two new page generator functions are implemented
  - `Type<TDomainType, TPageSchema>()`: Renders given domain type as a page
    using given page schema
  - `Method<TDomainType, TPageSchema>(methodName:)`: Renders given domain method
    as a page using given page schema
- `Bake` now executes given `Action` defined in `ComponentDescriptor`
  implementations upon model change or `submit` event
- `Button` component is now added
- `useActionExecuter` composable is now added to execute `Composite`, `Local`,
  `Publish` and `Remote` actions with given configuration
  - `Page` component now provides an event bus to publish page-wide events
- `SimpleForm` component is now added for rendering a basic form with inputs
  with dialog support
- `DataTable` now supports row based actions via `Actions` property
  - `ListIsDataTableUxFeature` and `ObjectWithListIsDataTableUxFeature` both now
    add and fill actions column automatically
- `Bake` now supports reload and show/hide reactions
  - Use `ReloadOn` and `ShowOn` to bind them to an event
  - Use `ReloadWhen` and `ShowWhen` to bind them to a page context value
- `Constraints` now allows you to define constraints on values of triggers so
  that reactions can happen only on certain conditions
- `InputText` and `InputNumber` components are now introduced along with their
  basic conventions
- `SimplePage` is now added to render simple pages with title and contents along
  with its basic conventions
- `FormPage` is now added to render action methods as full pages
- `Contents` utility component is now added that renders `List<Content>` with
  responsive styling
  - `ActionsAsDataPanelsUxFeature` is modified to add data panel to any content
    in any page, expect `Get` methods to be rendered as data panel under any
    page's content list
- `Dialog` component is now added which displays given content in dialog
  with action support
- `Composite` component is now added to add multiple components to a single
  component slot
- New UX features are introduced in `Monolith` recipe
  - **Actions as Buttons**: to render non-`GET` actions as buttons
  - **Actions are Contents**: to render `GET` actions as contents
  - **Data Table defaults**: to set defaults for all `DataTable` components

## Breaking Changes

- `[...baked].vue` page is now not used, `*.page.json` file paths are used as
  route patterns and rendered directly with `Page.vue`
- `baseURL` is renamed to `apiBaseUrl` and config is now set in root of `bake`
  module options and no longer awailable through `dataFetcher`
- `FromType<TDomainType>()` page generator is removed, now you need to specify
  page schema upfront in `Type<TDomainType, TPageSchema>()`
- `ReportPage` is renamed to `TabbedPage`
  - All `Components` and `DomainComponents` helpers are updated accordingly
  - `.b-ReportPage--grid` class is now removed, use `.b-Contents` to override
    css for that element
- `ReportPage.Tab` and `ReportPage.Tab.Content` is now `Tab` and `Content`
  respectively
  - All `Components` and `DomainComponents` helpers are updated accordingly
- `Parameter` schema is renamed to `Inputs`
  - `ParameterParameter` domain component helper is renamed to `ParameterInput`
  - `Parameter` component helper is renamed to `Input`
- `DataPanel.Parameters` property is renamed to `Inputs`
- `QueryParameters` property of `TabbedPage` (former `ReportPage`) is renamed to
  `Inputs`
- `Parameters.vue` is renamed to `Inputs.vue`
- `QueryParameters.vue` is now removed, use `Inputs` with all of its inputs'
  `queryBound` set to `true` to get the same behavior
  - Unlike `QueryParameters`, `Inputs` pass an event object `{ uniqueKey, values
    }` to its `onChanged` event
- `TypeWithOnlyGetIsReportPage` UX feature is removed, and adding `TabbedPage`
  (former `ReportPage`) component to a type is moved to `DefaultThemeFeature`
- `InjectedData` is renamed to `ContextData`
  - `Injected()` is now removed, use `Context` property
    - Use `Context.Model(...)` to access model data during actions
    - Parent data access has changed
      ```csharp
      Injected(options: i => i.DataKey = InjectedData.DataKey.ParentData) // old usage
      Context.Parent(options: cd => cd.Prop = "data") // new usage
      ```
    - Custom injected data is now removed, `DataPanel` provides `"parameters"`
      key in parent context to provide its parameter options
      ```csharp
      Injected(options: i => i.DataKey = InjectedData.DataKey.Custom) // old usage
      Context.Parent(options: cd => cd.Prop = "parameters") // new usage
      ```
    - `DataTable` now injects row data using parent context under `"row"` key
      ```csharp
      // previously data table was injecting row data in an hard-coded way
      Context.Parent(options: cd => cd.Prop  = "row") // new usage
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
  data: Computed.UseError() // new usage
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
- `Content` (former `ReportPage.Tab.Content`) support for `showWhen` is
  completely removed, use its component's reaction system to hide a content
  - `lg:col-span-2` class is now passed directly to content's component instead
    of a wrapper `div`
- `DeferredTabContent` had a div to show/hide child, it is now removed and
  `hidden` prop is passed directly to the grid div
- In `useContext` composable, `injectPage` and `providePage` are renamed to
  `injectPageContext` and `providePageContext` respectively
- `Inputs` now doesn't have a layout styling, any component that uses it should
  wrap it and introduce `flex` styling
- `EnumSelect` and `EnumSelectButton` in `DomainComponents` are renamed to
  `ParameterSelect` and `ParameterSelectButton`
  - They still require an `InlineData` schema on the parameter type
- Data composables `compute` is renamd to `computeSync`
  and `computeAsync` is renamed to `compute`
- `useFormat.format()` is now removed, which was used for route building, use
  `usePathBuilder` with named route params instead
- `ActionsAreGroupedAsTabsUxFeature` is now removed
  - `Monolith` recipe now uses `ActionsAreContentsUxFeature`

## Improvements

- `Parameters` now accept parameter class attribute for each parameter
- `RemoveComponent` and `RemoveSchema` helper extensions are now added
- `GetRequiredComponent<T>(...)` and `GetComponent<T>(...)` extensions are now
  added to query a specific component type at a given path
- `AwaitLoading` utility component is now added which contains slots to help
  rendering skeleton and content according to `loading` state
- `ContextData`, former `InjectedData`, now has `TargetProp` property to map
  given `Prop` key value to the corresponding property
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
- `IAction`, `IData` and `IComponentDescriptor` now implement `+` operator to
  easily convert them into a `CompositeAction`, `CompositeData` and
  `CompontentDescriptor<Composite>`
  ```csharp
  component.Data += Context.Parent(options: cd => cd.Prop = "parameters");
  ```
- `Inputs` has become a pure utility component after removing wrapper div and
  `flex` styling
- `NavLink` now supports named route parameters and query which both can be
  provided from schema as `IData`
- `NavLink` now has `Icon` property
- `PageContext.Sitemap` is now `IReadOnlyCollection`
- `AddRemoveChildCodingStyleFeature` now removes `New` prefix in addition to
  `Add` and `Create`
- `Add/Remove...Attribute` conventions now provide `requiresIndex:` parameter to
  allow postponing add/remove attribute conventions that won't require index
  (such as UI component and schema attributes) after building indices
- UI conventions are now added after all API attributes get configured and are
  safe to use API configurations such as HTTP method of `ActionModelAttribute`
- Unlike schema conventions, UI component conventions were allowed to be added
  to non API method and parameters, fixed
