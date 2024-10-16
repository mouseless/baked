using FluentNHibernate.Cfg.Db;

namespace Baked.DataAccess;

public class PersistenceConfiguration
{
    public IPersistenceConfigurer Configurer { get; set; } = SQLiteConfiguration.Standard.InMemory();
}