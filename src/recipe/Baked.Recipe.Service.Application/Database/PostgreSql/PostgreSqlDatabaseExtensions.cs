using Baked.Configuration;
using Baked.Database;
using Baked.Database.PostgreSql;

namespace Baked;

public static class PostgreSqlDatabaseExtensions
{
    public static PostgreSqlDatabaseFeature PostgreSql(this DatabaseConfigurator _,
        Setting<string>? connectionString = default,
        Setting<bool>? autoUpdateSchema = default
    ) => new(
        connectionString ?? Settings.Required<string>("Database:PostgreSql:ConnectionString"),
        autoUpdateSchema ?? Settings.Optional("Database:PostgreSql:AutoUpdateSchema", false)
    );
}