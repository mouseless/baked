# Unreleased

## Improvements

- `useFormat` wasn't using locale info from i18n, fixed
- `locale.en.restext` format wasn't supported for default language when
  generating missing keys, fixed
  - `locale.en.{restext|json}` is now the standard format for locale files, do
    NOT use `locale.restext` in backend
- `ServiceSpec` and `DataSourceSpec` were missing localization feature, fixed
- `ILocalizer` interface was removed, `Baked.Service.Application` now depend on
  `Microsoft.AspNetCore.Localization` and may use `IStringLocalizer` directly
- `ExceptionHandler` wasn't setting details when there is no localizer, fixed

## Library Upgrades

| NuGet Package                                  | Old Version | New Version |
| ---                                            | ---         | ---         |
| Microsoft.AspNetCore.Localization              | 2.3.0       | removed     |
| Microsoft.Extensions.Localization              | new         | 9.0.7       |
| Microsoft.Extensions.Localization.Abstractions | new         | 9.0.7       |
