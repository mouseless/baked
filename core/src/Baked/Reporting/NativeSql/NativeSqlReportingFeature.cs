using Baked.Architecture;
using Baked.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Reporting.NativeSql;

public class NativeSqlReportingFeature(Setting<string> _basePath)
    : IFeature<ReportingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Runtime.ConfigureConfigurationBuilder(configuration =>
        {
            configuration.AddJsonAsDefault($$"""
            {
              "Logging": {
                "LogLevel": {
                  "NHibernate": "None",
                  "NHibernate.Sql": "{{(configurator.IsDevelopment ? "Debug" : "None")}}"
                }
              }
            }
            """);
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton(new ReportOptions(_basePath));
            services.AddSingleton<IReportContext, ReportContext>();
        });
    }
}