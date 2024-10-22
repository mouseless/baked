using Baked.Architecture;
using Baked.Runtime;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace Baked.Reporting.NativeSql;

public class NativeSqlReportingFeature(Setting<string> _basePath)
    : IFeature<ReportingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton(new ReportSettings(_basePath));
            services.AddSingleton<IReportContext, ReportContext>();
            services.AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenStatelessSession());
            services.AddSingleton<Func<IStatelessSession>>(sp => () => sp.UsingCurrentScope().GetRequiredService<IStatelessSession>());
        });
    }
}