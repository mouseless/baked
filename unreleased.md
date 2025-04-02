# Unreleased

## Features

- Beta features are available in `baked-recipe-admin`;
  - `DataPanel` is introduced where you can lazy load your data within a panel
    - `Parameters` is added to render input parameters in `icon` template of
      `Panel`
  - `ErrorHandling` plugin is introduced for handling errors and alert or full
    page error info display
  - `ErrorPage` schema is added to create descriptor for custom error page
  - `DataTable` page component is added
  - `ReportPage` schema is added to create report like pages
    - `DeferredTabContent` component is added to load tab contents lazily in
      report pages
    - `QueryParameters` component is added to render input parameters in
      `actions` template of `PageTitle`
  - `Icon`, `Money`, `Rate`, `Link` page components are added
  - `useFormat` composable is added for number formatting
  - `Auth` plugin is now introduced for authorized routing and requests with
    jwt support
  - `AuthorizedContent` component is now introduced for to display/hide
    content
  - `Select` input component is added
  - `useQuery` composable is added as a computed data to use query parameters in
    place for a data
    - When used in query data of a remote data, it forwards current page's all
      query parameters to a remote call, allowing to use an endpoint of a rich
      transient in a data panel
  - `useContext` composable is added to manage bake context in ui components
  - `InjectedData` is introduced for components to provide values, e.g.
    parameter data, to its child commponents
  - `CompositeData` is introduced to combine data from different sources
  - `ModalLayout` is introduced for pages like login
  - `CustomPage` is introduced to allow custom pages through baked ui
  - `SelectButton` input component is added
- Beta features are available in `Baked.Recipe.Service.Application`;
  - `Jwt` authentication feature implementation is now added with
    `JwtTokenBuilder` implementation of `ITokenBuilder` service

## Improvements

- `baked-recipe-admin` package size is reduced
- Remove bottom margin from `PageTitle` and add space between header and content
  in `MenuPage`
- `ComputedData` now accepts args to be passed from backend to frontend
- `RemoteData` now accepts query
- `Bake.vue` now provides a baked component path under `useContext().path()` to
  be used as a unique key within a page
- `Bake.vue` now manages `loading` state, making it possible for components to
  show a skeleton during loading
- `SideMenu`, `PageTitle`, `Header` now supports skeleton
- `DetailPage` and its conventions are removed
- `Layout` now automatically resolves and changes the page layout when the page
  schema has `layout` property, which allows you to specify layout of a page
  through its descriptor from backend
- `baseURL` and `retryFetch` are moved from `baked.components.Bake` to
  `baked.composables.useDataFetcher` to make them reusable across the project
- `route-params` is removed from `Page` and fixed to `baked`, since there is no
  value in making that something configurable
- Page and layout descriptors are now loaded in script setup to avoid flickers
  on page load

## Library Upgrades

| NuGet Package                                 | Old Version | New Version |
| ---                                           | ---         | ---         |
| Microsoft.AspNetCore.Authentication.JwtBearer | new         | 9.0.3       |
| System.IdentityModel.Tokens.Jwt               | new         | 8.7.0       |

| npm Package                                   | Old Version | New Version |
| ---                                           | ---         | ---         |
| @nuxt/kit                                     | 3.15.4      | 3.16.1      |
| @nuxtjs/tailwindcss                           | 6.13.1      | 6.13.2      |
| @primeuix/themes                              | new         | 1.0.0       |
| @primevue/themes                              | 4.2.5       | removed     |
| async-mutex                                   | new         | 0.5.0       |
| primevue                                      | 4.2.5       | 4.3.3       |
| vue-deepunref                                 | new         | 1.0.1       |
