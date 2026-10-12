using Baked.Database.PostgreSql;
using Baked.Runtime;

namespace Baked;

public static class PostgreSqlDatabaseExtensions
{
    extension(DatabaseFeature.Configurator _)
    {
        public PostgreSqlDatabaseFeature PostgreSql(
            Setting<string>? connectionString = default,
            Setting<bool>? autoUpdateSchema = default
        ) => new(
            connectionString ?? Settings.Required<string>("Database:PostgreSql:ConnectionString"),
            autoUpdateSchema ?? Settings.Optional("Database:PostgreSql:AutoUpdateSchema", false)
        );
    }
}