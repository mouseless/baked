using Baked.Reporting;
using Baked.Reporting.Mock;

namespace Baked;

public static class MockReportingExtensions
{
    extension(ReportingConfigurator _)
    {
        public MockReportingFeature Mock() =>
            new();
    }
}