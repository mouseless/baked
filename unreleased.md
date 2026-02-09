# Unreleased

## Improvements

- `ComputedSync` datas added and computed data now supports sync load
- `CompositeSync` datas added and composite data now supports sync load
- `IData` now has a `IsAsync` property
  - It is set to `true` in `RemoteData`
  - It is set to `false` in `InlineData` and `ContextData`
  - In `CompositeData`, it is set to `false` if any of the parts are not async
  - In `ComputedData`, it is set to `false` value unless it is provided
  externally or `Options` is async
- `Datas.Composables` now provide `UseLoginRedirect` extension
- `useDataMounter` composable added for `dataFetcher`'s recurring get and fetch
  ritual

## Breaking Changes

- All `computeSync` renamed to `compute`, in composables
- `IData` now has a `IsAsync` property