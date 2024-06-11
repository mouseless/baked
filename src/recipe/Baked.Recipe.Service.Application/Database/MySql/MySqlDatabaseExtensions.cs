using Baked.Configuration;
using Baked.Database;
using Baked.Database.MySql;

namespace Baked;

public static class MySqlDatabaseExtensions
{
    public static MySqlDatabaseFeature MySql(this DatabaseConfigurator _,
        Setting<string>? connectionString = default,
        Setting<bool>? autoUpdateSchema = default
    ) => new(
        connectionString ?? Settings.Required<string>("Database:MySql:ConnectionString"),
        autoUpdateSchema ?? Settings.Optional("Database:MySql:AutoUpdateSchema", false)
    );
}