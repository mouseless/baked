using BenchmarkDotNet.Attributes;
using Do.Architecture;

namespace Do.Test;

[SimpleJob(launchCount: 1, warmupCount: 2, iterationCount: 20, invocationCount: 100)]
public class Build
{
    [Benchmark]
    public ApplicationContext DomainModel_with_reflection()
    {
        return BenchmarkSpec.Init(app =>
        {
            app.Layers.AddConfiguration();
            app.Layers.AddDataAccess();
            app.Layers.AddDependencyInjection();
            app.Layers.AddDomain();
            app.Layers.AddMonitoring();
            app.Layers.AddRestApi();
            app.Layers.AddTesting();

            app.Features.AddBusiness(c => c.Benchmark());
            app.Features.AddCore(c => c.Mock());
            app.Features.AddDatabase(c => c.InMemory());
            app.Features.AddExceptionHandling(c => c.Default());
            app.Features.AddOrm(c => c.Default());
            app.Features.AddConfigurationOverrider();
        });
    }
}
