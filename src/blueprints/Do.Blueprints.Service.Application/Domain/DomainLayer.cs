using Do.Architecture;
using Do.Domain.Configuration;
using Do.Domain.Model;
using Microsoft.Extensions.Configuration;
using static Do.Configuration.ConfigurationLayer;

namespace Do.Domain;

public class DomainLayer : LayerBase<BuildConfiguration>
{
    readonly IDomainAssemblyCollection _domainAssemblies = new DomainAssemblyCollection();
    readonly IDomainTypeCollection _domainTypes = new DomainTypeCollection();
    readonly DomainBuilderOptions _domainBuilderOptions = new();
    readonly DomainConventions _domainConventions = new();
    readonly ModelIndexerCollection _domainIndexers = [];

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContextBuilder()
            .Add<IDomainAssemblyCollection>(_domainAssemblies)
            .Add<IDomainTypeCollection>(_domainTypes)
            .Add<DomainBuilderOptions>(_domainBuilderOptions)
            .Add<DomainConventions>(_domainConventions)
            .Add<ModelIndexerCollection>(_domainIndexers)
            .Build();

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new BuildDomain(_domainAssemblies, _domainTypes, _domainBuilderOptions, _domainConventions, _domainIndexers);
    }

    public class BuildDomain(
        IDomainAssemblyCollection _domainAssemblies,
        IDomainTypeCollection _domainTypes,
        DomainBuilderOptions _domainBuilderOptions,
        DomainConventions _domainConventions,
        ModelIndexerCollection _domainIndexers
    ) : PhaseBase<ConfigurationManager>(PhaseOrder.Early)
    {
        protected override void Initialize(ConfigurationManager _)
        {
            var configurer = new DomainServiceCollection()
                .AddOptions(_domainBuilderOptions)
                .AddConventions(_domainConventions)
                .AddIndexers(_domainIndexers);

            var builder = configurer.Build();

            var model = builder.BuildFrom(_domainAssemblies, _domainTypes);

            Context.Add<DomainModel>(model);
        }
    }
}
