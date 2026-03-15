using Baked.Architecture;
using Baked.DataAccess.Sqlite;
using Baked.Runtime;
using Microsoft.Extensions.DependencyInjection;

using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.Database.Sqlite;

public class SqliteDatabaseFeature(Setting<string> _fileName, Setting<bool> _autoExportSchema)
    : IFeature<DatabaseConfigurator>
{
    string FullFilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _fileName);

    public void Configure(LayerConfigurator configurator)
    {
        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITransaction, FlatTransaction>();
        });

        configurator.DataAccess.ConfigurePersistence(persistence =>
        {
            persistence.Configurer = SQLiteConfiguration.Microsoft.UsingFile(FullFilePath);
        });

        configurator.DataAccess.ConfigureDatabaseInitializationCollection((initializations, sp) =>
        {
            initializations.AddInitializer(sf =>
            {
                if (_autoExportSchema)
                {
                    using (var session = sf.OpenSession())
                    {
                        sp.GetRequiredService<NHConfiguration>().ExportSchema(false, true, false, session.Connection);
                    }
                }
            });
        });

        configurator.HttpServer.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}