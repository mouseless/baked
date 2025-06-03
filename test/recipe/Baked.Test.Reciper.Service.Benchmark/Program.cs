using Baked.Test;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Testing>(config: DefaultConfig.Instance.WithArtifactsPath(@"./.benchmark"));