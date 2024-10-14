using Baked.Architecture;
using Baked.DataAccess.Sqlite;
using Baked.Runtime;
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
            persistence.Configurer = SQLiteConfiguration.Microsoft.UsingFile(FullFilePath);
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}