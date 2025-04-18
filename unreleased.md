# Unreleased

## Features

- Beta features are available in `baked-recipe-admin`;
  - `Message` component is with icon and severity support
  - `Number` component is introduced to display numbers in formatted with
    tooltips

## Improvements

- The `QueryParameter` component now supports subcomponents that manage its own
  default value. It can be managed with the `defaultSelfManaged` parameter.
- Changed breadcrumb last item from `<span>` to `link`.
- Removed `DeclaredOnly` binding flag from properties.
- `ConditionalComponent` was renamed to `Conditional` and moved under the
  namespace `Baked.Theme.Admin`.
- `String` component now has max length property, which will truncate the
  text ending with ellipsis and show full text with a tooltip
- `IReportContext`, now allows nulls in parameter dictionary.
- New `ToBase64Url` and `FromBase64Url` extensions added.
- `showUnhandled` flag is added to exception handling, and enabled by default in
  staging environment
- `Datatable` component now has footer support
- `Datatable` component now has scrollable toggle
