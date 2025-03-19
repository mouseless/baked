# Unreleased

## Features

- Beta features are available in `baked-recipe-admin` package;
  - `DataPanel` is introduced where you can lazy load your data within a panel
  - `ErrorHandling` plugin is introduced for handling errors and alert or full
    page error info display
  - `ErrorPage` schema is added to create descriptor for custom error page
  - `DataTable` page component is added
  - `ReportPage` schema is added to create report like pages
    - `DeferredTabContent` component is added to load tab contents lazily in
      report pages
  - `Icon`, `Money`, `Rate`, `Link` page components are added
  - `useFormat` composable is added for number formatting

## Improvements

- `baked-recipe-admin` package size is reduced
- remove bottom margin from `PageTitle` and add space between header and content
  in `MenuPage`
- `ComputedData` now accepts args to be passed from backend to frontend
- `Bake.vue` now provides `uiContext` for a component to have a unique key
  within a page
- `Bake.vue` now manages `loading` state, making it possible for components to
  show a skeleton during loding
- `SideMenu`, `PageTitle`, `Header` now supports skeleton
- `DetailPage` and its conventions are removed
