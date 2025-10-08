using FluentNHibernate.Cfg.Db;
using Microsoft.Data.Sqlite;
using NHibernate.Extensions.Sqlite;

namespace Baked.DataAccess.Sqlite;

public class SQLiteConfiguration : PersistenceConfiguration<SQLiteConfiguration>
{
    public static SQLiteConfiguration Microsoft => new();

    public SQLiteConfiguration()
    {
        Driver<SqliteDriver>();
        Dialect<SqliteDialect>();
        Raw("query.substitutions", "true=1;false=0");
    }

    public SQLiteConfiguration InMemory(
        SqliteCacheMode cache = SqliteCacheMode.Default
    )
    {
        Raw("connection.release_mode", "on_close");

        return ConnectionString(c => c.Is(new SqliteConnectionStringBuilder { DataSource = ":memory:", Mode = SqliteOpenMode.Memory, Cache = cache }.ToString()));
    }

    public SQLiteConfiguration UsingFile(string fileName) =>
        ConnectionString(c => c.Is(new SqliteConnectionStringBuilder { DataSource = fileName }.ToString()));
}