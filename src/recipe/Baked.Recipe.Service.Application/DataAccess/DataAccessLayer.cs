using Baked.Architecture;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

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
                services.AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenSession());
                services.AddSingleton<Func<ISession>>(sp => () => sp.GetRequiredServiceUsingRequestServices<ISession>());
            })
            .Build();
    }

    protected override PhaseContext GetContext(PostBuild phase)
    {
        var sp = Context.GetServiceProvider();

        NHibernateLogger.SetLoggersFactory(sp.GetRequiredService<INHibernateLoggerFactory>());
        sp.GetRequiredService<NHConfiguration>().SetInterceptor(new DelegatedInterceptor(sp, _interceptorConfiguration));

        if (_persistenceConfiguration.AutoExportSchema)
        {
            if (Context.Has<IServiceScope>())
            {
                ExportSchema(sp);
            }
            else
            {
                using (var scope = sp.CreateScope())
                {
                    ExportSchema(scope.ServiceProvider);
                }
            }
        }

        return phase.CreateEmptyContext();
    }

    void ExportSchema(IServiceProvider sp)
    {
        var session = sp.GetRequiredService<ISession>();

        var export = new SchemaExport(sp.GetRequiredService<NHConfiguration>());
        export.Execute(false, true, false, session.Connection, null);
    }
}