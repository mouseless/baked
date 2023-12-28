using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Domain;
using Domain.Business;
using DomainModelOverReflection.Api;
using System.Reflection;

namespace DomainModelOverReflection;

[SimpleJob(runStrategy: RunStrategy.ColdStart, launchCount: 1, iterationCount: 1, invocationCount: 1)]
public class Testing
{
    [Benchmark(Baseline = true)]
    public ApiModel Controller_models_using_reflection() =>
        ApiModel.Build(Assembly.GetAssembly(typeof(IQueryContext<>)));

    [Benchmark]
    public ApiModel Controller_models_using_domain_model() =>
        ApiModel.Build(new DomainModel());
}
