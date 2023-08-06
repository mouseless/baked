# Unreleased

## Improvements

- Added singleton access helper to Stubber, `GiveMe.The<TService>()`.
- `GiveMe.Create()` is renamed to `GiveMe.A()`, `GiveMe.An()`.
- `GiveMe.A()` and `GiveMe.An()` are renamed to `GiveMe.AnInstanceOf()`
- Configurators now have ability to switch based on environments
  - `developmentDatabase` is removed, you can use environment switcher
- Added `Uri` and `object` to `string` mapping support for `Orm` feature
