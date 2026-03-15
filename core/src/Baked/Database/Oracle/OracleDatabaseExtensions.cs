using Baked.Database;
using Baked.Database.Oracle;
using Baked.Runtime;

namespace Baked;

public static class OracleDatabaseExtensions
{
    extension(DatabaseConfigurator _)
    {
        public OracleDatabaseFeature Oracle(
            Setting<string>? connectionString = default,
            Setting<bool>? autoUpdateSchema = default
        ) => new(
            connectionString ?? Settings.Required<string>("Database:Oracle:ConnectionString"),
            autoUpdateSchema ?? Settings.Optional("Database:Oracle:AutoUpdateSchema", false)
        );
    }
}