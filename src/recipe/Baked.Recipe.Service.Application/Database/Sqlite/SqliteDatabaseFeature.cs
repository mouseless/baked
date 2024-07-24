using Baked.Architecture;
using Baked.Configuration;
using Baked.DataAccess.Sqlite;
using Microsoft.Extensions.DependencyInjection;

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
            persistence.AutoExportSchema = _autoExportSchema;
            persistence.Configurer =
                SQLiteConfiguration.Microsoft
                    .UsingFile(FullFilePath)
                    .MaxFetchDepth(1);
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}