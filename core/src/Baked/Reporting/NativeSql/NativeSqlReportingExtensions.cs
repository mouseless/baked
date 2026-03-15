using Baked.Reporting;
using Baked.Reporting.NativeSql;
using Baked.Runtime;

namespace Baked;

public static class NativeSqlReportingExtensions
{
    extension(ReportingConfigurator _)
    {
        public NativeSqlReportingFeature NativeSql(
            Setting<string>? basePath = default
        ) => new(basePath ?? string.Empty);
    }
}