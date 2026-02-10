# Unreleased

## Improvements

- Computed data and composite data now support sync load
  - If there is no async in the parts of composite data, it performs a sync load
  - Computed data does a sync load unless specified otherwise
- `IData` now has a `IsAsync` property
  - It is set to `true` in `RemoteData`
  - It is set to `false` in `InlineData` and `ContextData`
  - In `CompositeData`, it is set to `false` if all of the parts are not async
  - In `ComputedData`, it is set to `false` value unless it is provided
    externally or `Options` is async
- `Datas.Composables` now provide `UseLoginRedirect` extension
- `useDataMounter` composable is now added for a more convenient data fetching

## Breaking Changes

- All `computeSync` renamed to `compute`, in composables
- `IData` now has a `IsAsync` property