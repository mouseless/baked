using Do.Architecture;

namespace Do.Test.Blueprints.Service.DependencyInjection;

public class DependencyInjectionLayer : LayerBase<DependencyInjectionLayer.AddServices>
{
    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new AddServices();
    }

    protected override PhaseContext GetContext(AddServices phase) =>
        phase.CreateContext(Context.Get<IServiceCollection>());

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
