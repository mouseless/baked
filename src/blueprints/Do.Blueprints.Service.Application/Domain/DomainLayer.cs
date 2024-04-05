using Do.Architecture;
using Do.Domain.Configuration;
using Do.Domain.Model;
using Microsoft.Extensions.Configuration;

using static Do.Configuration.ConfigurationLayer;

namespace Do.Domain;

public class DomainLayer : LayerBase<BuildConfiguration>
{
    readonly IDomainTypeCollection _domainTypes = new DomainTypeCollection();
    readonly DomainModelBuilderOptions _builderOptions = new();

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContextBuilder()
            .Add(_domainTypes)
            .Add(_builderOptions)
            .Build();

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new BuildDomain(_domainTypes, _builderOptions);
    }

    public class BuildDomain(IDomainTypeCollection _domainTypes, DomainModelBuilderOptions _builderOptions)
        : PhaseBase<ConfigurationManager>(PhaseOrder.Early)
    {
        protected override void Initialize(ConfigurationManager _)
        {
            var builder = new DomainModelBuilder(_builderOptions);
            var model = builder.Build(_domainTypes);

            Context.Add(model);
        }
    }
}