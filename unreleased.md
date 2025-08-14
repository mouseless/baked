# Unreleased

## Improvements

- `Select` and `SelectButton` components now add selected values to the page
  context
- `Bake.vue` now provides data descriptor to the component, inject it using
  `context.dataDescriptor()`
- `useDataFetcher` now provides a way to get parameter values from data
  descriptor via `fetchParameters` method
- `DataTable.vue` now includes parameter values in export file names
- `RemoteData.Options` has changed to `RemoteData.Attribute` to avoid confusion
  with `ofetch`'s options parameter
- The select input on the cache page was not working properly, improved
- The QueryParameters component setDefaults was not working properly, improved
- The `app.json` file is now automatically added to module.ts
- Clear cache on teardown was missing for `InMemoryCachingFeature`, fixed
- Improved `Layout`, `Input`, `MenuPage`, `ReportPage`, `QueryParameters`,
  `Parameters` components watchers