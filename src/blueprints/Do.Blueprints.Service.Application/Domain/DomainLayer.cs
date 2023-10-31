using Do.Architecture;
using Do.Domain.Model;
using Microsoft.Extensions.Configuration;

using static Do.Configuration.ConfigurationLayer;

namespace Do.Domain;

public class DomainLayer : LayerBase<BuildConfiguration>
{
    readonly IAssemblyCollection _assemblyCollection = new AssemblyCollection();
    readonly ITypeCollection _typeCollection = new TypeCollection();

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContextBuilder()
            .Add<IAssemblyCollection>(_assemblyCollection)
            .Add<ITypeCollection>(_typeCollection)
            .Build();

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new BuildDomain(_assemblyCollection, _typeCollection);
    }

    public class BuildDomain : PhaseBase<ConfigurationManager>
    {
        readonly IAssemblyCollection _assemblyCollection;
        readonly ITypeCollection _typeCollection;

        public BuildDomain(IAssemblyCollection assemblyCollection, ITypeCollection typeCollection)
            : base(PhaseOrder.Early)
        {
            _assemblyCollection = assemblyCollection;
            _typeCollection = typeCollection;
        }

        protected override void Initialize(ConfigurationManager _)
        {
            Context.Add<DomainModel>(new(_assemblyCollection, _typeCollection));
        }
    }
}
