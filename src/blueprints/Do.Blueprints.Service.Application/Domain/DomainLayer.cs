using Do.Architecture;
using Do.Domain.Model;
using Microsoft.Extensions.Configuration;

using static Do.Configuration.ConfigurationLayer;

namespace Do.Domain;

public class DomainLayer : LayerBase<BuildConfiguration>
{
    readonly IAssemblyCollection _assemblies = new AssemblyCollection();
    readonly ITypeCollection _types = new TypeCollection();
    readonly DomainBuilderOptions _domainBuilderOptions = new();

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContextBuilder()
            .Add<IAssemblyCollection>(_assemblies)
            .Add<ITypeCollection>(_types)
            .Add<DomainBuilderOptions>(_domainBuilderOptions)
            .Build();

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new BuildDomain(_assemblies, _types, _domainBuilderOptions);
    }

    public class BuildDomain(IAssemblyCollection _assemblies, ITypeCollection _types, DomainBuilderOptions _domainBuilderOptions)
        : PhaseBase<ConfigurationManager>(PhaseOrder.Early)
    {
        protected override void Initialize(ConfigurationManager _)
        {
            var builder = new DomainModelBuilder(_domainBuilderOptions);
            var model = builder.BuildFrom(_assemblies, _types);

            Context.Add<DomainModel>(model);
        }
    }
}
