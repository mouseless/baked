using Do.Business;
using Do.Test.Benchmark;

namespace Do;

public static class BenchmarkBusinessExtensions
{
    public static BenchmarkBusinessFeature Benchmark(this BusinessConfigurator _) => new();
}
