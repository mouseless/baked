# Unreleased

## Improvements

- `DataTable` styles were not loading due to an error in primevue, styles
  included in the package and fixed.
- `DataTableColumn` now has `alignRight:` option
- `DataTable` export button was added to last column, causing misalignment,
  fixed by adding actions column
- `DataTableColumn` title was exported as `label` when no label was given, fixed
- `DataPanel` autoscroll is triggered each time it is loaded
