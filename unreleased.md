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

## Improvements

- `Bake.vue` now inlines 400 responses for data and action errors with a
  to help user relate the error and the place where error has occured
  - Other status codes are still handled via the existing `errorHandling` plugin
- `context.injectError` and `context.provideError` are introduced to pass a data
  or an action error to child
  - Injecting error automatically means it is handled so that parent doesn't
    have to display the error message
  - `useBakeError` composable is introduced to handle and format a `Bake.vue`
    data or action error
    ```vue
    <template>
      <span v-if="error.raw">
        {{ error.formatted.summary }} - {{ error.formatted.detail }}
      </span>
    </template>
    <script setup>
    const { handle: handleError } = useBakeError();

    const error = handleError();
    </script>
    ```
- `IComponentDescriptor.Error` and `AppDescriptor.InlineError` are introduced
  to customize error display for components that don't handle their inline
  errors by injecting
- `AwaitLoading` is now mult-slot and handles error displays
  - Has an `#error` slot that allows you customize data loading errors
  - Automatically handles an error, to disable this behavior set the `no-error`
    flag
  - You can now pass multiple children to `#default`, `#loading` and `#error`
    slots to be wrapped automatically by a `div` when there are more than one
    children
