using Baked.Architecture;
using Baked.DataAccess.Sqlite;
using Baked.Runtime;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;

using Environment = System.Environment;

namespace Baked.Database.Sqlite;

public class SqliteDatabaseFeature(Setting<string> _fileName, Setting<bool> _autoExportSchema)
    : IFeature<DatabaseConfigurator>
{
    string FullFilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _fileName);

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITransaction, FlatTransaction>();
        });

        configurator.ConfigurePersistence(persistence =>
        {
            persistence.Configurer = SQLiteConfiguration.Microsoft.UsingFile(FullFilePath);
        });

        configurator.ConfigureDatabaseInitializationCollection((initializations, sp) =>
        {
            initializations.AddInitializer(sf =>
            {
                if (_autoExportSchema)
                {
                    using (var session = sf.OpenSession())
                    {
                        sp.GetRequiredService<Configuration>().ExportSchema(false, true, false, session.Connection);
                    }
                }
            });
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}