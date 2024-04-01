using Do.Architecture;
using Do.Domain.Configuration;
using Do.Domain.Model;
using Microsoft.Extensions.Configuration;
using static Do.Configuration.ConfigurationLayer;

namespace Do.Domain;

public class DomainLayer : LayerBase<BuildConfiguration>
{
    readonly IDomainTypeCollection _domainTypes = new DomainTypeCollection();
    readonly DomainModelBuilderOptions _domainBuilderOptions = new();

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContextBuilder()
            .Add<IDomainTypeCollection>(_domainTypes)
            .Add<DomainModelBuilderOptions>(_domainBuilderOptions)
            .Build();

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new BuildDomain(_domainTypes, _domainBuilderOptions);
    }

    public class BuildDomain(
        IDomainTypeCollection _domainTypes,
        DomainModelBuilderOptions _domainBuilderOptions
    ) : PhaseBase<ConfigurationManager>(PhaseOrder.Early)
    {
        protected override void Initialize(ConfigurationManager _)
        {
            var builder = new DomainModelBuilder(_domainBuilderOptions);
            var model = builder.Build(_domainTypes);

            Context.Add<DomainModel>(model);
        }
    }
}
