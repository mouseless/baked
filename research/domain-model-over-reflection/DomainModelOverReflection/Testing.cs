using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using DomainModelOverReflection.Api;
using DomainModelOverReflection.Domain;
using DomainModelOverReflection.Domain.Business;
using System.Reflection;

namespace DomainModelOverReflection;

[SimpleJob(runtimeMoniker: RuntimeMoniker.Net70, runStrategy: RunStrategy.ColdStart, launchCount: 1, iterationCount: 1, invocationCount: 1)]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net80, runStrategy: RunStrategy.ColdStart, launchCount: 1, iterationCount: 1, invocationCount: 1)]
public class Testing
{
    [Benchmark]
    public ApiModel Controller_models_using_domain_model_with_reflection() =>
        ApiModel.Build(new DomainModelWithReflection());

    [Benchmark(Baseline = true)]
    public ApiModel Controller_models_using_reflection() =>
        ApiModel.Build(Assembly.GetAssembly(typeof(IQueryContext<>)));

    [Benchmark]
    public ApiModel Controller_models_using_generated_domain_model() =>
        ApiModel.Build(new DomainModelWithGeneration());

    static DomainModelWithRuntimeReflection _testing = default!;

    [Benchmark]
    public ApiModel Controller_models_using_runtime_built_domain_model()
    {
        if (_testing is null)
        {
            _testing = DomainModelWithRuntimeReflection.Build(Assembly.GetAssembly(typeof(IQueryContext<>)) ?? throw new());
        }

        return ApiModel.Build(_testing);
    }
}
