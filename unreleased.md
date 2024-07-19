# Unreleased

## Improvements

- `InMemory` and `Sqlite` data base features were not working under ARM
  processors, fixed
- Automatic schema update is removed from `Sqlite` database because
  `Microsoft.Data.Sqlite.Core` does not support `SchemaUpdate`
- `autoExportSchema` parameter is introduced to `Sqlite` database with default
  value `true` to use automatic schema for development databases which clears
  data each time your application runs. You may set it to false if you want your
  data to retain.

## Library Upgrades

| Package                       | Old Version | New Version |
| ----------------------------- | ----------- | ----------- |
| Microsoft.Data.Sqlite.Core    | new         | 8.0.7       |
| NHibernate.Extensions.Sqlite  | new         | 8.0.8       |
| SQLitePCLRaw.bundle_e_sqlite3 | new         | 2.1.8       |
| System.Data.SQLite            | 1.0.118     | removed     |
