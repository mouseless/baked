# Unreleased

## Feetures

- `FormPage` uses `useValidateDefault` composable for validation out of the
  box, with support for a custom form validation logic `ValidateComposable`
  when needed
- `Labeler` introduces `ShowOptionality` to indicate required or optional fields
- `Select`, `SelectButton`, `InputText`, and `InputNumber` now accept a
  validator prop to display validation messages below the component
- `FormPage` validation messages are now displayed in the submit button's
  tooltip. To disable this behavior, set `ValidationOnTooltip = false`

## Breaking Changes

- `ILabeler` properties are now grouped under `Label` schema
  - `Label` -> `Label.Text`
  - `LabelMode` -> `Label.Mode`
  - `LabelVariant` -> `Label.Variant`
  - You must change your label conventions from `Input` to `Label`
    ```csharp
    // old
    builder.Conventions.AddParameterSchemaConfiguration<Input>( where: cc => cc.Path.EndsWith(...)
        schema: (i, c, cc) =>
        {
            if (i.Component.Schema is not ILabeler labeler) { return; }

            labeler.LabelIfta(labeler.Label ?? "...");
        }
    );
    // new
    builder.Conventions.AddParameterSchemaConfiguration<Label>(
        where: cc => cc.Path.EndsWith(..., "*", nameof(ILabeler.Label))
        schema: (label, c, cc) =>
        {
            label.Ifta(() => "...");
        }
    );
    ```
- `ISelect.LocalizeLabel` is renamed as `ISelect.LocalizeOptionLabels` for
  clarity
