using Baked.Reporting.NativeSql;
using Baked.Runtime;

namespace Baked;

public static class NativeSqlReportingExtensions
{
    extension(ReportingFeature.Configurator _)
    {
        public NativeSqlReportingFeature NativeSql(
            Setting<string>? basePath = default
        ) => new(basePath ?? string.Empty);
    }
}