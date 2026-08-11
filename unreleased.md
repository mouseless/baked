# Unreleased

## Improvements

- `useRedirect` now supports query
  - Also, support include and exclude options
- `usePathBuilder` now has a `buildDetailed` overload
- `ListIsDataTableUxFeature` now detects async methods when generating actions
  for data tables
- The file-based routing feature is set to `true` by default. A page under
  `/pages` is no longer required

## Bugfixes

- Inconsistent enum casing between response values and enum data, fixed
- `OpenAPI` security definitions are not displayed on endpoints, fixed

## Breaking Changes

- The order of the remote action configuration convention for
  `DataTable.Actions` in `DataTableDefaultsUxFeature` was increased by 10