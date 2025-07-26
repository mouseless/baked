# Unreleased

## Features

- Beta features are available in `Baked.Recipe.Service.Application`;
  - `RateLimiter` feature is now added with `ConcurrencyLimiter` implementation
  - `Localization` feature is now added
  - `Caching.InMemory` is introduced to allow you to cache data in memory for
    application-wide caching
- Beta feature is available in `baked-recipe-admin`;
  - `localization` plugin has been introduced along with the `useLocalization`
    composable to manage texts according to culture.
  - `cacheApplication` and `cacheUser` plugins are introduced to cache api
    responses in local storage with a configured expiration value

## Improvements

- `useFormat` was clearing text when number has two trailing zeros, fixed
- `useFormat` shorteners was not working for negative values, fixed
- `UiLayer` now provides `UsingLocaleDictionary` and `UsingNewLocale` helpers
  for adding and tracking locale keys for generated page descriptors
- UI components from different page but same route was having state conflicts,
  fixed
- `Parameters` component now emits `onChanged` before `onReady` to fix values
  inconsistency
- Local storage was getting cleared on logout, fixed
- Csv formatter now including column and row based format
- Removed `(Object)` from the project name in the descriptions

## Library Upgrades

| NuGet Package                     | Old Version | New Version |
| ---                               | ---         | ---         |
| Microsoft.AspNetCore.Localization | new         | 2.3.0       |

| npm Package      | Old Version | New Version |
| ---              | ---         | ---         |
| @nuxtjs/i18n     | new         | 9.5.6       |
| @primeuix/themes | 1.1.1       | 1.2.1       |
| primevue         | 4.3.4       | 4.3.6       |
