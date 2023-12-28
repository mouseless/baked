using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using DomainModelOverReflection;

BenchmarkRunner.Run<Testing>(config: DefaultConfig.Instance.WithArtifactsPath(@"./.benchmark"));
