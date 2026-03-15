using Baked.Architecture;
using Baked.DataAccess;
using Baked.Testing;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Data.Common;

using NHEnvironment = NHibernate.Cfg.Environment;

namespace Baked;

public static class DataAccessExtensions
{
    public class Configurator(LayerConfigurator _configurator)
    {
        public void ConfigureFluentConfiguration(Action<FluentConfiguration> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigurePersistence(Action<PersistenceConfiguration> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureAutomapping(Action<AutomappingConfiguration> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureAutoPersistenceModel(Action<AutoPersistenceModel> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureNHibernateInterceptor(Action<InterceptorConfiguration> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureDatabaseInitializationCollection(Action<IDatabaseInitializationCollection> configuration) =>
            _configurator.Configure((IDatabaseInitializationCollection initializations, IServiceProvider sp) => configuration(initializations));

        public void ConfigureDatabaseInitializationCollection(Action<IDatabaseInitializationCollection, IServiceProvider> configuration) =>
            _configurator.Configure(configuration);
    }

    extension(LayerConfigurator configurator)
    {
        public Configurator DataAccess => new(configurator);
    }

    extension(ICollection<ILayer> layers)
    {
        public void AddDataAccess() =>
            layers.Add(new DataAccessLayer());
    }

    extension(IDatabaseInitializationCollection initializations)
    {
        public void AddInitializer(Action<ISessionFactory> initializer) =>
            initializations.Add(new(initializer));
    }

    extension(FluentConfiguration configuration)
    {
        public void MaxFetchDepth(int maxFetchDepth) =>
            configuration.ExposeConfiguration(c => c.SetProperty(NHEnvironment.MaxFetchDepth, $"{maxFetchDepth}"));

        public void UpdateSchema(bool useStdOut, bool doUpdate) =>
            configuration.ExposeConfiguration(c => new SchemaUpdate(c).Execute(useStdOut, doUpdate));
    }

    extension(Configuration configuration)
    {
        public void ExportSchema(bool useStdOut, bool doUpdate, bool justDrop, DbConnection connection) =>
            new SchemaExport(configuration).Execute(useStdOut, doUpdate, justDrop, connection, null);
    }

    extension(Stubber giveMe)
    {
        public ISession TheSession(
            bool clear = false
        )
        {
            var result = giveMe.The<ISession>();
            if (clear)
            {
                result.Flush();
                result.Clear();
            }

            return result;
        }
    }

}