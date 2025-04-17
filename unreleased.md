# Unreleased

## Improvements

- The `QueryParameter` component now supports subcomponents that manage its own
  default value. It can be managed with the `defaultSelfManaged` parameter.
- Changed breadcrumb last item from `<span>` to `link`.
- Removed `DeclaredOnly` binding flag from properties.
- `ConditionalComponent` was renamed to `Conditional` and moved under the
  namespace `Baked.Theme.Admin`.
- `IReportContext`, now allows nulls in parameter dictionary.
- New `ToBase64Url` and `FromBase64Url` extensions added.
