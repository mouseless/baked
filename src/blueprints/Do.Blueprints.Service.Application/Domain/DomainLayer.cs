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

    public class BuildDomain(IAssemblyCollection _assemblyCollection, ITypeCollection _typeCollection)
        : PhaseBase<ConfigurationManager>(PhaseOrder.Early)
    {
        protected override void Initialize(ConfigurationManager _)
        {
            var model = new DomainModel();

            foreach (var descriptor in _assemblyCollection)
            {
                model.Assemblies.Add(new(descriptor.Assembly));

                foreach (var type in descriptor.Assembly.GetExportedTypes())
                {
                    _typeCollection.Add(type);
                }
            }

            foreach (var descriptor in _typeCollection.Distinct())
            {
                model.Types.Add(new(descriptor.Type));
            }

            model.Init();

            Context.Add<DomainModel>(model);
        }
    }
}


