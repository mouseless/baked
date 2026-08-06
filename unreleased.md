# Unreleased

## Improvements

- `useRedirect` now supports query
  - Also, support include and exclude options
- `usePathBuilder` now has a `buildDetailed` overload

## Breaking Changes

- The order of the remote action configuration convention for
  `DataTable.Actions` in `DataTableDefaultsUxFeature` was increased by 10
- `ListIsDataTableUxFeature` now detects async methods when generating actions
  for data tables
- `EnumInline` enum values use `camelCase`
- `EnumParameterIsSelectUxFeature` configured input `DefaultValue` uses
  `camelCase`