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

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContextBuilder()
            .Add<IDomainAssemblyCollection>(_domainAssemblies)
            .Add<IDomainTypeCollection>(_domainTypes)
            .Add<DomainBuilderOptions>(_domainBuilderOptions)
            .Add<DomainConventions>(_domainConventions)
            .Build();

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new BuildDomain(_domainAssemblies, _domainTypes, _domainBuilderOptions, _domainConventions);
    }

    public class BuildDomain(
        IDomainAssemblyCollection _domainAssemblies,
        IDomainTypeCollection _domainTypes,
        DomainBuilderOptions _domainBuilderOptions,
        DomainConventions _domainConventions
    )
        : PhaseBase<ConfigurationManager>(PhaseOrder.Early)
    {
        protected override void Initialize(ConfigurationManager _)
        {
            var builder = new DomainModelBuilder(_domainBuilderOptions, _domainConventions);
            builder.Initialize();
            var model = builder.BuildFrom(_domainAssemblies, _domainTypes);

            Context.Add<DomainModel>(model);
        }
    }
}
