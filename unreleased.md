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
