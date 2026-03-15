using Baked.Database;
using Baked.Database.Sqlite;
using Baked.Runtime;

namespace Baked;

public static class SqliteDatabaseExtensions
{
    extension(DatabaseConfigurator _)
    {
        public SqliteDatabaseFeature Sqlite(
            Setting<string>? fileName = default,
            Setting<bool>? autoExportSchema = default
        ) => new(
            fileName ?? Settings.Required<string>("Database:Sqlite:FileName"),
            autoExportSchema ?? Settings.Optional("Database:Sqlite:AutoExportSchema", true)
        );
    }
}