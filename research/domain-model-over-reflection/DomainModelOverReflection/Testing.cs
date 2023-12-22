using BenchmarkDotNet.Attributes;
using DomainModelOverReflection.Models.Target;
using System.Reflection;

namespace DomainModelOverReflection;

[SimpleJob(launchCount: 1, warmupCount: 2, iterationCount: 20, invocationCount: 100)]
public class Testing
{
    [Benchmark(Baseline = true)]
    public ApiModel Controller_models_using_reflection() =>
        ApiModel.Build(Assembly.GetAssembly(typeof(Testing)));
}
