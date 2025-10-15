using Baked.Reporting;
using Baked.Reporting.Fake;
using Baked.Runtime;
using Baked.Testing;
using Microsoft.Extensions.FileProviders;

namespace Baked;

public static class FakeReportingExtensions
{
    public static FakeReportingFeature Fake(this ReportingConfigurator _,
        Setting<string>? basePath = default
    ) => new(basePath ?? string.Empty);

    public static IReportContext AFakeReportContext(this Stubber giveMe,
        string basePath = "Fake"
    ) => new ReportContext(giveMe.The<IFileProvider>(), new(basePath));
}