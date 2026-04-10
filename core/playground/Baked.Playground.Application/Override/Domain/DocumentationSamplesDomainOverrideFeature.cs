using Baked.Architecture;
using Baked.Playground.Business;
using Baked.RestApi.Model;

namespace Baked.Playground.Override.Domain;

public class DocumentationSamplesDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddConfigureAction<DocumentationSamples>(nameof(DocumentationSamples.Route), parameter: p =>
            {
                p["route"].From = ParameterModelFrom.Route;
                p["route"].RoutePosition = 2;
            });
        });
    }
}