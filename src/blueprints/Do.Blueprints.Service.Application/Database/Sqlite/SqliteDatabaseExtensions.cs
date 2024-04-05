using Do.Configuration;
using Do.Database;
using Do.Database.Sqlite;

namespace Do;

public static class SqliteDatabaseExtensions
{
    public static SqliteDatabaseFeature Sqlite(this DatabaseConfigurator _,
        Setting<string>? fileName = default
    ) => new(
        fileName ?? Settings.Required<string>("Database:Sqlite:FileName")
    );
}