using Do.Configuration;
using Do.Database;
using Do.Database.MySql;

namespace Do;

public static class MySqlDatabaseExtensions
{
    public static MySqlDatabaseFeature MySql(this DatabaseConfigurator _,
        Setting<string>? connectionString = default,
        Setting<bool>? autoUpdateSchema = default,
        Setting<bool>? showSql = default
    ) => new(
        connectionString ?? Settings.Required<string>("Database:MySql:ConnectionString"),
        autoUpdateSchema ?? Settings.Optional("Database:MySql:AutoUpdateSchema", false),
        showSql ?? Settings.Optional("Database:MySql:ShowSql", false)
    );
}