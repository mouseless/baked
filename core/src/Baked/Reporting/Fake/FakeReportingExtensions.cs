using Baked.Reporting;
using Baked.Reporting.Fake;
using Baked.Runtime;
using Baked.Testing;
using Microsoft.Extensions.FileProviders;

namespace Baked;

public static class FakeReportingExtensions
{
    extension(ReportingConfigurator _)
    {
        public FakeReportingFeature Fake(
            Setting<string>? basePath = default
        ) => new(basePath ?? string.Empty);
    }

    extension(Stubber giveMe)
    {
        public IReportContext AFakeReportContext(
            string basePath = "Fake"
        ) => new ReportContext(giveMe.The<IFileProvider>(), new(basePath));
    }
}