﻿using Baked.Database;
using Baked.Database.Sqlite;
using Baked.Runtime;

namespace Baked;

public static class SqliteDatabaseExtensions
{
    public static SqliteDatabaseFeature Sqlite(this DatabaseConfigurator _,
        Setting<string>? fileName = default,
        Setting<bool>? autoExportSchema = default
    ) => new(
        fileName ?? Settings.Required<string>("Database:Sqlite:FileName"),
        autoExportSchema ?? Settings.Optional("Database:Sqlite:AutoExportSchema", true)
    );
}