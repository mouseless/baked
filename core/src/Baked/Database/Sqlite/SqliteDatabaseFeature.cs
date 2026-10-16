using Baked.Architecture;
using Baked.DataAccess.Sqlite;
using Baked.Database;
using Baked.Domain.Configuration;
using Baked.Runtime;
using Microsoft.Extensions.DependencyInjection;
using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked;

public static class SqliteDatabaseFeature
{
    public static IFeature<DatabaseFeature.Configurator> Sqlite(this DatabaseFeature.Configurator x,
        Setting<string>? fileName = default,
        Setting<bool>? autoExportSchema = default
    ) => x.Define((
        fileName ?? Settings.Required<string>("Database:Sqlite:FileName"),
        autoExportSchema ?? Settings.Optional("Database:Sqlite:AutoExportSchema", true)
    ), (configurator, args) =>
    {
        var (fileName, autoExportSchema) = args;

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.Add(new AddTransactionFilterToActionConvention(), order: Order.At.Global.Max);
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITransaction, FlatTransaction>();
            services.AddScoped<TransactionFilter>();
        });

        configurator.DataAccess.ConfigurePersistence(persistence =>
        {
            var fullFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

            persistence.Configurer = SQLiteConfiguration.Microsoft.UsingFile(fullFilePath);
        });

        configurator.DataAccess.ConfigureDatabaseInitializationCollection((initializations, sp) =>
        {
            initializations.AddInitializer(sf =>
            {
                if (autoExportSchema)
                {
                    using (var session = sf.OpenSession())
                    {
                        sp.GetRequiredService<NHConfiguration>().ExportSchema(false, true, false, session.Connection);
                    }
                }
            });
        });
    });
}