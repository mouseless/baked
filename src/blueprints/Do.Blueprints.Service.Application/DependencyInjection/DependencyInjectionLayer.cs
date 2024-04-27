using Do.Architecture;
using Do.CodeGeneration;
using Do.Domain.Model;
using Microsoft.Extensions.DependencyInjection;

using static Do.DependencyInjection.DependencyInjectionLayer;

namespace Do.DependencyInjection;

public class DependencyInjectionLayer : LayerBase<AddServices, PostBuild>
{
    readonly IServiceCollection _services = new ServiceCollection();

    protected override PhaseContext GetContext(AddServices phase) =>
        phase.CreateContext(_services);

    protected override PhaseContext GetContext(PostBuild phase) =>
        phase.CreateContext(Context.GetServiceProvider());

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new AddServices(_services);
        yield return new PostBuild();
    }

    public class AddServices(IServiceCollection _services)
        : PhaseBase<DomainModel, GeneratedAssemblyProvider>(PhaseOrder.Early)
    {
        protected override void Initialize(DomainModel _, GeneratedAssemblyProvider __)
        {
            Context.Add(_services);
        }
    }

    public class PostBuild
        : PhaseBase<IServiceProvider>
    {
        protected override void Initialize(IServiceProvider _) { }
    }
}