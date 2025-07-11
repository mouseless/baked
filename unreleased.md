# Unreleased

## Features

- Beta features are available in `Baked.Recipe.Service.Application`;
  - `RateLimiter` feature is now added with `ConcurrencyLimiter` implementation
  - `localization` feature is now added.
- Beta feature is available in `baked-recipe-admin`;
  - `localization` plugin has been introduced along with the `useLocalization`
    composable to manage texts according to culture.

## Improvements

- `useFormat` was clearing text when number has two trailing zeros, fixed
- `useFormat` shorteners was not working for negative values, fixed

## Library Upgrades

| NuGet Package                                  | Old Version | New Version |
| ---                                            | ---         | ---         |
| Microsoft.AspNetCore.Localization              | new         | 2.3.0       |

| npm Package          | Old Version | New Version |
| ---                  | ---         | ---         |
| @nuxtjs/i18n         | new         | 9.5.4       |
