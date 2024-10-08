using Baked.Architecture;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

using static Baked.Runtime.RuntimeLayer;
using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.DataAccess;

public class DataAccessLayer : LayerBase<BuildConfiguration, AddServices, PostBuild>
{
    readonly PersistenceConfiguration _persistenceConfiguration = new();
    readonly InterceptorConfiguration _interceptorConfiguration = new();
    readonly AutomappingConfiguration _automappingConfiguration = new();
    readonly AutoPersistenceModel _autoPersistenceModel;
    readonly FluentConfiguration _fluentConfiguration;

    volatile bool _exported = false;
    readonly object _exportedLock = new();

    public DataAccessLayer()
    {
        _autoPersistenceModel = new(new DelegatedAutomappingConfiguration(_automappingConfiguration));
        _fluentConfiguration = Fluently.Configure();
    }

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContext(_fluentConfiguration);

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.GetServiceCollection();

        services.AddSingleton<INHibernateLoggerFactory, StandardNHibernateLoggerFactory>();
        services.AddSingleton(sp =>
        {
            _fluentConfiguration
                .ExposeConfiguration(c => c.SetInterceptor(new DelegatedInterceptor(sp, _interceptorConfiguration)))
                .Mappings(m => m.AutoMappings.Add(_autoPersistenceModel));

            return _fluentConfiguration.BuildConfiguration();
        });

        services.AddSingleton(sp => sp.GetRequiredService<NHConfiguration>().BuildSessionFactory());
        services.AddSingleton<Func<ISession>>(sp => () => sp.GetRequiredServiceUsingRequestServices<ISession>());

        services.AddScoped(sp =>
        {
            var result = sp.GetRequiredService<ISessionFactory>().OpenSession();

            if (_persistenceConfiguration.AutoExportSchema)
            {
                if (!_exported)
                {
                    lock (_exportedLock)
                    {
                        if (!_exported)
                        {
                            var export = new SchemaExport(sp.GetRequiredService<NHConfiguration>());

                            export.Execute(false, true, false, result.Connection, null);
                            _exported = true;
                        }
                    }
                }
            }

            return result;
        });

        return phase.CreateContextBuilder()
            .Add(_persistenceConfiguration)
            .Add(_interceptorConfiguration)
            .Add(_automappingConfiguration)
            .Add(_autoPersistenceModel)
            .Build();
    }

    protected override PhaseContext GetContext(PostBuild phase)
    {
        var sp = Context.GetServiceProvider();
        NHibernateLogger.SetLoggersFactory(sp.GetRequiredService<INHibernateLoggerFactory>());

        return phase.CreateEmptyContext();
    }
}