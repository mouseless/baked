# Unreleased

## Features

- Beta features are available in `baked-recipe-admin` package;
  - `DataPanel` is introduced where you can lazy load your data within a panel

## Improvements

- `baked-recipe-admin` package size is reduced
- remove bottom margin from `PageTitle` and add space between header and content
  in `MenuPage` and `DetailPage`
- `ComputedData` now accepts args to be passed from backend to frontend
- `Bake.vue` now provides `uiContext` for a component to have a unique key
  within a page
- `Bake.vue` now manages `loading` state, making it possible for components to
  show a skeleton during loding
- `SideMenu`, `PageTitle`, `Header` now supports skeleton
