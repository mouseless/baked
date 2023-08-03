using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

using static Do.DependencyInjection.DependencyInjectionLayer;

namespace Do.DependencyInjection;

public class DependencyInjectionLayer : LayerBase<AddServices>
{
    protected override PhaseContext GetContext(AddServices phase) =>
        phase.CreateContext(Context.GetServiceCollection());

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new AddServices();
    }

    public class AddServices : PhaseBase
    {
        public AddServices() : base(PhaseOrder.Early) { }

        protected override void Initialize()
        {
            IServiceCollection services = new ServiceCollection();

            Context.Add(services);
        }
    }
}
