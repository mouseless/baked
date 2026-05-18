# Unreleased

## Feetures

- `FormPage` uses `useValidateDefault` composable for validation out of the
  box, with support for a custom form validation logic `ValidateComposable`
  when needed
- `Labeler` introduces `ValidateLabel` to indicate required or optional fields
- `Select`, `SelectButton`, `InputText`, and `InputNumber` now accept a
  validator prop to display validation messages below the component
- `FormPage` validation messages are now displayed in the submit button's
  tooltip. To disable this behavior, set `ValidationOnTooltip = false`

## Improvements

- `DataContainer` header not displayed when no inputs exist
- `Select` was not having correct height when label was not provided, fixed
- `DataContainer` border color in dark mode, fixed
- `Paginator` was resetting to zero when page reload, fixed
- `InputText` was not displaying initial model value, fixed
- `SelectButton` was not displaying correctly in FormPage sections, fixed

## Bugfixes

- `Ado` exceptions was returning 200 result, fixed
