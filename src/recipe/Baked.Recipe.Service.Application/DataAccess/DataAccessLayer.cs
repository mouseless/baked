using Baked.Architecture;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

using static Baked.Runtime.RuntimeLayer;
using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.DataAccess;

public class DataAccessLayer : LayerBase<AddServices, PostBuild>
{
    readonly PersistenceConfiguration _persistenceConfiguration = new();
    readonly InterceptorConfiguration _interceptorConfiguration = new();
    readonly AutomappingConfiguration _automappingConfiguration = new();
    readonly AutoPersistenceModel _autoPersistenceModel;
    readonly FluentConfiguration _fluentConfiguration;
    readonly IDatabaseInitializationCollection _databaseInitializationCollection = new DatabaseInitializationCollection();

    public DataAccessLayer()
    {
        _autoPersistenceModel = new(new DelegatedAutomappingConfiguration(_automappingConfiguration));
        _fluentConfiguration = Fluently.Configure();
    }

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.GetServiceCollection();

        services.AddSingleton<INHibernateLoggerFactory, StandardNHibernateLoggerFactory>();

        return phase.CreateContextBuilder()
            .Add(_fluentConfiguration)
            .Add(_persistenceConfiguration)
            .Add(_interceptorConfiguration)
            .Add(_automappingConfiguration)
            .Add(_autoPersistenceModel)
            .OnDispose(() =>
            {
                _fluentConfiguration.Database(_persistenceConfiguration.Configurer);
                _fluentConfiguration.Mappings(m => m.AutoMappings.Add(_autoPersistenceModel));

                services.AddSingleton(sp => _fluentConfiguration.BuildConfiguration());
                services.AddSingleton(sp => sp.GetRequiredService<NHConfiguration>().BuildSessionFactory());
            })
            .Build();
    }

    protected override PhaseContext GetContext(PostBuild phase)
    {
        var sp = Context.GetServiceProvider();

        NHibernateLogger.SetLoggersFactory(sp.GetRequiredService<INHibernateLoggerFactory>());
        sp.GetRequiredService<NHConfiguration>().SetInterceptor(new DelegatedInterceptor(sp, _interceptorConfiguration));

        return phase.CreateContext(_databaseInitializationCollection, sp, onDispose: () =>
        {
            var sessionFactory = sp.GetRequiredService<ISessionFactory>();
            foreach (var descriptor in _databaseInitializationCollection)
            {
                descriptor.Initializer(sessionFactory);
            }
        });
    }
}