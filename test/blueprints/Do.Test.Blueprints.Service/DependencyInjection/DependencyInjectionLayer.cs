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

    public class AddServices : PhaseBase<IServiceCollection>
    {
        public AddServices() : base(PhaseOrder.Earliest) { }

        protected override void Initialize(IServiceCollection _) { }
    }
}
