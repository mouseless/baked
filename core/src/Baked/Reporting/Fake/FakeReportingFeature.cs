using Baked.Architecture;
using Baked.Reporting;
using Baked.Reporting.Fake;
using Baked.Runtime;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Baked;

public static class FakeReportingFeature
{
    public static IFeature<ReportingFeature.Configurator> Fake(this ReportingFeature.Configurator x,
        Setting<string>? basePath = default
    ) => x.Define(basePath ?? string.Empty, (configurator, basePath) =>
    {
        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton(new ReportOptions(basePath));
            services.AddSingleton<IReportContext, ReportContext>();
        });
    });

    extension(Stubber giveMe)
    {
        public IReportContext AFakeReportContext(
            string basePath = "Fake"
        ) => new ReportContext(giveMe.The<IFileProvider>(), new(basePath));
    }
}