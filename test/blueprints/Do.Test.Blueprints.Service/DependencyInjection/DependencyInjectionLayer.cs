using Do.Architecture;

namespace Do.Test.Blueprints.Service.DependencyInjection;

public class DependencyInjectionLayer : LayerBase
{
    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new AddServices();
    }

    protected override ConfigurationTarget GetConfigurationTarget(IPhase phase, ApplicationContext context) =>
        phase switch
        {
            AddServices => ConfigurationTarget.Create(context.Get<IServiceCollection>()),
            _ => ConfigurationTarget.Empty
        };

    public class AddServices : PhaseBase<IServiceCollection>
    {
        public AddServices() : base(PhaseOrder.Earliest) { }

        protected override void Initialize(IServiceCollection _) { }
    }
}
