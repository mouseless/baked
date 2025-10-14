using Baked.Architecture;
using Baked.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Reporting.Fake;

public class FakeReportingFeature(Setting<string> _basePath) :
    IFeature<ReportingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton(new ReportOptions(_basePath));
            services.AddSingleton<IReportContext, ReportContext>();
        });
    }
}