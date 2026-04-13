using Baked.Architecture;
using Baked.Playground.ExceptionHandling;
using Baked.RestApi.Model;

namespace Baked.Playground.Override.Domain;

public class ExceptionSamplesDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddConfigureAction<ExceptionSamples>(nameof(ExceptionSamples.Throw), parameter: p => p["handled"].From = ParameterModelFrom.Query);
        });
    }
}