using Baked.Architecture;

namespace Baked.Reporting.Mock;

public class MockReportingFeature : IFeature<ReportingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTestConfiguration(test =>
        {
            test.Mocks.Add<IReportContext>(singleton: true);
        });
    }
}