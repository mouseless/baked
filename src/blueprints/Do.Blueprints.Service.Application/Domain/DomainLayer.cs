using Do.Architecture;
using Do.Domain.Model;
using Microsoft.Extensions.Configuration;

using static Do.Configuration.ConfigurationLayer;

namespace Do.Domain;

public class DomainLayer : LayerBase<BuildConfiguration>
{
    readonly IDomainAssemblyCollection _domainAssemblyCollection = new DomainAssemblyCollection();
    readonly IDomainTypeCollection _domainTypeCollection = new DomainTypeCollection();
    readonly DomainBuilderOptions _domainBuilderOptions = new();

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContextBuilder()
            .Add<IDomainAssemblyCollection>(_domainAssemblyCollection)
            .Add<IDomainTypeCollection>(_domainTypeCollection)
            .Add<DomainBuilderOptions>(_domainBuilderOptions)
            .Build();

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new BuildDomain(_domainAssemblyCollection, _domainTypeCollection, _domainBuilderOptions);
    }

    public class BuildDomain(IDomainAssemblyCollection _domainAssemblyCollection, IDomainTypeCollection _domainTypeCollection, DomainBuilderOptions _domainBuilderOptions)
        : PhaseBase<ConfigurationManager>(PhaseOrder.Early)
    {
        protected override void Initialize(ConfigurationManager _)
        {
            var builder = new DomainModelBuilder(_domainBuilderOptions);

            var model = builder.BuildFrom(_domainAssemblyCollection, _domainTypeCollection);

            Context.Add<DomainModel>(model);
        }
    }
}