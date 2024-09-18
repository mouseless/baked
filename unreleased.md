# Unreleased

## Features

- Beta features are available in Baked.Recipe.Service package;
  - `PostgreSql` implementation is added to `Database` feature

## Improvements

- Built-in configs couldn't be overridden in `appsettings.json` and
  `appsettings.[Environment].json` files, fixed
- `IQueryContext` now provides `whereIf:` to allow dynamic where clause building
- `*By` methods were causing parents to be fetched lazily, fixed
- Records were not rendered in api endpoints, fixed

## Library Upgrades

| Package  | Old Version | New Version |
| -------- | ----------- | ----------- |
| Npgsql   | new         | 8.0.4       |
