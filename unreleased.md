# Unreleased

## Features

- `useValidation` composable is introduced to allow custom form validations
  - `useDefaultValidation` composable is included out of the box that checks for
    required values
  - `FormPage` and `SimpleForm` utilizes `useValidation` to enable form
    validation, you may add custom validations through `FormPage.Validations`
    and `SimpleForm.Validations` respectively
- `FormPage` and `SimpleForm` validation messages can now be displayed in the
  submit button's tooltip
  - To enable/disable this behavior, use `ShowValidationSummary` property
- `Validation` utility component is now available to render an input with a
  validation message component
  - `message` slot is available to override the message component underneath
    ```vue
    <template #message="{ validation }">{{ validation.message }}</template>
    ```
- `Label` introduces `ShowOptionality` to indicate required or optional fields
- `Labeler` now renders with red border when inner input is not valid

## Breaking Changes

- `ILabeler` properties are now grouped under `Label` schema
  - `Label` -> `Label.Text`
  - `LabelMode` -> `Label.Mode`
  - `LabelVariant` -> `Label.Variant`
  - You must change your label conventions from `Input` to `Label`
    ```csharp
    // old
    builder.Conventions.AddParameterSchemaConfiguration<Input>(
        where: cc => cc.Path.EndsWith(...),
        schema: (i, c, cc) =>
        {
            if (i.Component.Schema is not ILabeler labeler) { return; }

            labeler.LabelIfta(labeler.Label ?? "...");
        }
    );
    // new
    builder.Conventions.AddParameterSchemaConfiguration<Label>(
        where: cc => cc.Path.EndsWith(..., "*", nameof(ILabeler.Label)),
        schema: (label, c, cc) => label.Ifta(() => "...")
    );
    ```
- `ISelect.LocalizeLabel` is renamed as `ISelect.LocalizeOptionLabels` for
  clarity
