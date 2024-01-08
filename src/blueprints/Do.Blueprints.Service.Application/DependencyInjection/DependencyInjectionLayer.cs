using Do.Architecture;
using Do.Domain.Model;
using Microsoft.Extensions.DependencyInjection;

using static Do.DependencyInjection.DependencyInjectionLayer;

namespace Do.DependencyInjection;

public class DependencyInjectionLayer : LayerBase<AddServices>
{
    readonly IServiceCollection _services = new ServiceCollection();

    protected override PhaseContext GetContext(AddServices phase) =>
        phase.CreateContext(_services);

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new AddServices(_services);
    }

    public class AddServices(IServiceCollection _services) : PhaseBase<DomainModel>(PhaseOrder.Early)
    {
        protected override void Initialize(DomainModel _)
        {
            Context.Add(_services);
        }
    }
}
