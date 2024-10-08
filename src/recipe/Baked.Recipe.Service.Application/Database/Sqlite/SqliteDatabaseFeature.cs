using Baked.Architecture;
using Baked.DataAccess.Sqlite;
using Baked.Runtime.Configuration;
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

        configurator.ConfigureFluentBuilder(builder =>
        {
            builder.Database(SQLiteConfiguration.Microsoft.UsingFile(FullFilePath));
        });

        configurator.ConfigurePersistence(persistence =>
        {
            persistence.AutoExportSchema = _autoExportSchema;
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}