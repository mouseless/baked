using Baked.Architecture;
using Baked.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static Baked.Runtime.RuntimeLayer;

namespace Baked.Testing;

public class TestingLayer : LayerBase<AddServices>
{
    readonly TestConfiguration _configuration = new();
    readonly TestRun _run;

    public TestingLayer()
    {
        _run = new(_configuration);
    }

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.Get<IServiceCollection>();

        services.AddHttpContextAccessor();
        services.AddSingleton<IServiceProviderAccessor>(_run);

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
        yield return new Build(_run);
    }

    public class CreateConfigurationManager()
        : PhaseBase(PhaseOrder.Earliest)
    {
        protected override void Initialize()
        {
            Context.Add(new ConfigurationManager());
        }
    }

    class Build(TestRun _run)
        : PhaseBase<IServiceCollection>(PhaseOrder.Latest)
    {
        protected override void Initialize(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider(validateScopes: true);

            Context.Add<IServiceProvider>(serviceProvider);
            Context.Add<ITestRun>(_run);
        }
    }
}