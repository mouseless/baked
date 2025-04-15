# Unreleased

## Improvements

- Rich transients were causing build error when their initializer `With` is
  async, fixed
- `ActionModelAttribute` now has a separate `InvocationIsAsync` and
  `ReturnIsAsync` to make it possible for the action to be `async` while its
  backing method is not
- The `QueryParameter` component now supports subcomponents that manage its own
  default value. It can be managed with the `selfManagedDefault` parameter.
- Changed breadcrumb last item from `<span>` to `link`.
