using Do.Architecture;

using static Do.Test.Blueprints.Service.DependencyInjection.DependencyInjectionLayer;

namespace Do.Test.Blueprints.Service.DependencyInjection;

public class DependencyInjectionLayer : LayerBase<AddServices>
{
    protected override PhaseContext GetContext(AddServices phase) =>
        phase.CreateContext(Context.Get<IServiceCollection>());

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
