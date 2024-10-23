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
        configurator.ConfigureConfigurationBuilder(configuration =>
        {
            configuration.AddJsonAsDefault($$"""
            {
              "Logging": {
                "LogLevel": {
                  "NHibernate": "None",
                  "NHibernate.Sql": "{{(configurator.IsDevelopment() ? "Debug" : "None")}}"
                }
              }
            }
            """);
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton(new ReportOptions(_basePath));
            services.AddSingleton<IReportContext, ReportContext>();
            services.AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenStatelessSession());
            services.AddSingleton<Func<IStatelessSession>>(sp => () => sp.UsingCurrentScope().GetRequiredService<IStatelessSession>());
        });
    }
}