using Do.Architecture;
using Do.Domain.Model;
using Microsoft.Extensions.Configuration;

using static Do.Configuration.ConfigurationLayer;

namespace Do.Domain;

public class DomainLayer : LayerBase<BuildConfiguration>
{
    readonly IAssemblyCollection _assemblyCollection = new AssemblyCollection();
    readonly ITypeCollection _typeCollection = new TypeCollection();
    readonly DomainOptions _domainOptions = new();

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContextBuilder()
            .Add<IAssemblyCollection>(_assemblyCollection)
            .Add<ITypeCollection>(_typeCollection)
            .Add<DomainOptions>(_domainOptions)
            .Build();

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new BuildDomain(_assemblyCollection, _typeCollection, _domainOptions);
    }

    public class BuildDomain(IAssemblyCollection _assemblyCollection, ITypeCollection _typeCollection, DomainOptions _domainOptions)
        : PhaseBase<ConfigurationManager>(PhaseOrder.Early)
    {
        protected override void Initialize(ConfigurationManager _)
        {
            var model = new DomainModel(_domainOptions);

            foreach (var descriptor in _assemblyCollection)
            {
                model.Assemblies.TryAdd(new(descriptor.Assembly));

                foreach (var type in descriptor.Assembly.GetExportedTypes())
                {
                    _typeCollection.Add(type);
                }
            }

            foreach (var descriptor in _typeCollection)
            {
                model.Types.TryAdd(new TypeModel(descriptor.Type));
            }

            model.Init();

            Context.Add<DomainModel>(model);
        }
    }
}