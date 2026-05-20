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