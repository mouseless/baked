using Do.Architecture;
using Do.Domain.Model;
using Microsoft.Extensions.Configuration;

using static Do.Configuration.ConfigurationLayer;

namespace Do.Domain;

public class DomainLayer : LayerBase<BuildConfiguration>
{
    readonly DomainDescriptor _domainDescriptor = new();

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContext(_domainDescriptor);

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new BuildDomain(_domainDescriptor);
    }

    public class BuildDomain : PhaseBase<ConfigurationManager>
    {
        readonly DomainDescriptor _domainDescriptor;

        public BuildDomain(DomainDescriptor domainDescriptor) : base(PhaseOrder.Early) =>
            _domainDescriptor = domainDescriptor;

        protected override void Initialize(ConfigurationManager _)
        {
            Context.Add<DomainModel>(new(_domainDescriptor));
        }
    }
}
