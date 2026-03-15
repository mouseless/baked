using Baked.Database;
using Baked.Database.MySql;
using Baked.Runtime;

namespace Baked;

public static class MySqlDatabaseExtensions
{
    extension(DatabaseConfigurator _)
    {
        public MySqlDatabaseFeature MySql(
            Setting<string>? connectionString = default,
            Setting<bool>? autoUpdateSchema = default
        ) => new(
            connectionString ?? Settings.Required<string>("Database:MySql:ConnectionString"),
            autoUpdateSchema ?? Settings.Optional("Database:MySql:AutoUpdateSchema", false)
        );
    }
}