using Baked.Architecture;
using Baked.Reporting;
using Baked.Reporting.NativeSql;
using Baked.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class NativeSqlReportingFeature
{
    public static IFeature<ReportingFeature.Configurator> NativeSql(this ReportingFeature.Configurator x,
        Setting<string>? basePath = default
    ) => x.Define(configurator =>
    {
        basePath ??= string.Empty;

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
            services.AddSingleton(new ReportOptions(basePath));
            services.AddSingleton<IReportContext, ReportContext>();
        });
    });
}