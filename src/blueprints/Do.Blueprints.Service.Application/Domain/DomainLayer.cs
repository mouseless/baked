using Do.Architecture;

using static Do.Configuration.ConfigurationLayer;

namespace Do.Domain;

public class DomainLayer : LayerBase<BuildConfiguration>
{
    readonly DomainDescriptor _domainDescriptor = new();

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase
            .CreateContextBuilder()
            .Add(_domainDescriptor)
            .OnDispose(() =>
            {
                Context.Add(_domainDescriptor);
                Context.Add(DomainModelBuilder.CreateBuilder(_domainDescriptor).Build());
            })
            .Build()
        ;
}
