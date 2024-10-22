using Baked.Database;
using Baked.Database.Oracle;
using Baked.Runtime;

namespace Baked;

public static class OracleDatabaseExtensions
{
    public static OracleDatabaseFeature Oracle(this DatabaseConfigurator _,
        Setting<string>? connectionString = default,
        Setting<bool>? autoUpdateSchema = default
    ) => new(
        connectionString ?? Settings.Required<string>("Database:Oracle:ConnectionString"),
        autoUpdateSchema ?? Settings.Optional("Database:Oracle:AutoUpdateSchema", false)
    );
}