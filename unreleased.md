# Unreleased

## Improvements

- Add `setupBaked` plugin to module
- Fix inputtext style in dev mode not working
- Add support for subdirs in .baked pages
- Add `trailingSlash` plugin to admin recipe
- `Auth` plugin `LoginPageRoute` isno added to anonymous page routes 
  automatically
- Add `Custom` to `Components` helpers to create `new ComponentDescriptor(...)`
- `Parameter``@default` data type is changed to be `IData
- Fixed `SelectButton` and `Select` doesn't set selected when data is not `Inline`
- Fixed `DataPanel` doesn't load title because it calls `shouldLoad` with
  `titleData` where it should use `titleData.type`
