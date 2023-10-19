using Do.Architecture;

using static Do.Configuration.ConfigurationLayer;

namespace Do.Domain;

public class DomainLayer : LayerBase<BuildConfiguration>
{
    readonly DomainBuilderConfiguration _domainBuilderConfiguration = new();
    readonly DomainDescriptor _domainDescriptor = new();

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase
            .CreateContextBuilder()
            .Add(_domainDescriptor).Add(_domainBuilderConfiguration)
            .OnDispose(() =>
            {
                Context.Add(DomainModelBuilder.CreateBuilder(_domainBuilderConfiguration, _domainDescriptor).Build());
                Context.Add(_domainDescriptor);
            })
            .Build()
        ;
}
