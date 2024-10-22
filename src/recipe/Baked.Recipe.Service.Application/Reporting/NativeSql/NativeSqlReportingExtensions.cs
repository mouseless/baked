using Baked.Reporting;
using Baked.Reporting.NativeSql;
using Baked.Runtime;

namespace Baked;

public static class NativeSqlReportingExtensions
{
    public static NativeSqlReportingFeature NativeSql(this ReportingConfigurator _,
        Setting<string>? basePath = default
    ) => new(basePath ?? "/");
}