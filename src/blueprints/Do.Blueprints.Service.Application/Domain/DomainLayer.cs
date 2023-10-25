using Do.Architecture;
using Microsoft.Extensions.Configuration;

using static Do.Domain.DomainLayer;

namespace Do.Domain;

public class DomainLayer : LayerBase<ConfigureDomain>
{
    readonly DomainDescriptor _domainDescriptor = new();

    protected override PhaseContext GetContext(ConfigureDomain phase) =>
        phase.CreateContext(_domainDescriptor);

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new ConfigureDomain(_domainDescriptor);
        yield return new BuildDomain();
    }

    public class ConfigureDomain : PhaseBase<ConfigurationManager>
    {
        readonly DomainDescriptor _domainDescriptor;

        public ConfigureDomain(DomainDescriptor domainDescriptor) : base(PhaseOrder.Early)
        {
            _domainDescriptor = domainDescriptor;
        }

        protected override void Initialize(ConfigurationManager _)
        {
            Context.Add(_domainDescriptor);
        }
    }

    public class BuildDomain : PhaseBase<DomainDescriptor>
    {
        public BuildDomain() : base(PhaseOrder.Early) { }

        protected override void Initialize(DomainDescriptor domainDescriptor)
        {
            Context.Add(DomainModelBuilder.CreateBuilder(domainDescriptor).Build());
        }
    }
}
