using Baked.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Reporting.Fake;

public class FakeReportingFeature : IFeature<ReportingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<IReportContext, ReportContext>();
        });
    }
}