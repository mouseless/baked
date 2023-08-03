using FluentNHibernate.Cfg.Db;

namespace Do.DataAccess;

public class PersistenceConfiguration
{
    public IPersistenceConfigurer Configurer { get; set; } = SQLiteConfiguration.Standard.InMemory();
    public bool AutoExportSchema { get; set; } = false;
    public bool AutoUpdateSchema { get; set; } = false;
}
