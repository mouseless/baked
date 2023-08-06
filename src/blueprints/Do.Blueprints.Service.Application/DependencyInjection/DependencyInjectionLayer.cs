using Do.Architecture;
using Microsoft.Extensions.Configuration;
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

    public class AddServices : PhaseBase<ConfigurationManager>
    {
        readonly IServiceCollection _services;

        public AddServices(IServiceCollection services) : base(PhaseOrder.Early) =>
            _services = services;

        protected override void Initialize(ConfigurationManager _)
        {
            Context.Add(_services);
        }
    }
}