using Baked.Reporting;
using Baked.Reporting.Mock;

namespace Baked;

public static class MockReportingExtensions
{
    public static MockReportingFeature Mock(this ReportingConfigurator _) =>
        new();
}