using Baked.Architecture;
using Baked.Reporting;

namespace Baked;

public static class MockReportingFeature
{
    public static IFeature<ReportingFeature.Configurator> Mock(this ReportingFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Testing.ConfigureTestConfiguration(test =>
        {
            test.Mocks.Add<IReportContext>(singleton: true);
        });
    });
}