using Do.Architecture;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static Do.DependencyInjection.DependencyInjectionLayer;

namespace Do.Testing;

public class TestingLayer : LayerBase<AddServices>
{
    readonly TestConfiguration _configuration = new();

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.Get<IServiceCollection>();

        return phase.CreateContext(_configuration,
            onDispose: () =>
            {
                foreach (var mock in _configuration.Mocks)
                {
                    if (mock.Singleton)
                    {
                        services.AddSingleton(mock.Type, sp => _configuration.MockFactory.Create(sp, mock));
                    }
                    else
                    {
                        services.AddTransient(mock.Type, sp => _configuration.MockFactory.Create(sp, mock));
                    }
                }
            }
        );
    }

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new CreateConfigurationManager();
    }

    public class CreateConfigurationManager : PhaseBase
    {
        public CreateConfigurationManager() : base(PhaseOrder.Earliest) { }

        protected override void Initialize()
        {
            Context.Add(new ConfigurationManager());
        }
    }
}
