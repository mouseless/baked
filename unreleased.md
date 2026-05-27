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
- `Bake.vue` now inlines 400 responses of data and action requests to make it
  easier to relate the error and the component that causes it
  - Other status codes are still handled via the existing `errorHandling` plugin
  - `context.injectError` and `context.provideError` are introduced to pass a
    data or an action error to child
  - `useBakeError` composable is introduced to handle and format a data or
    action error occured in `Bake.vue`
    ```vue
    <template>
      <span v-if="error">
        {{ error.title }} - {{ error.detail }}
      </span>
    </template>
    <script setup>
    const { handle: handleError } = useBakeError();

    const { error } = handleError();
    </script>
    ```
  - `useDataMounter` now supports inline error display via `useDataMounter{
    defaultInlineError: true }` option to enable it page-wide, or `mount({
    inlineError: true })` to enable it individually
  - `AppDescriptor.InlineError` is introduced to customize error display for
    components that don't handle their inline errors by injecting
    - `InlineError` component is introduced to help rendering inline error
      schema in app descriptor
  - `ErrorPopover` component is introduced to show an error within a popover
    when there is no place to show the error in the baked component, e.g.,
    buttons, forms

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

- `AwaitLoading` now supports multi-children and handles inlined errors
  automatically
  - You can now pass multiple children to `#default`, `#loading` and `#error`
    slots to be wrapped automatically by a `div` when there are more two or more
    children
  - Has an `#error` slot that allows you customize data loading errors
  - Automatically sets error as handled, to disable this behavior set the
    `no-error` flag
- `FormPage` and `SimpleForm` (when in dialog mode) now displays 400 errors at
  the top of the form
- `Message` was not loading styles due to transition, fixed
- `Message` now supports `content` slot for additional content rendering
- `Toast` is wider and sticky by default in `errorHandling` plugin
