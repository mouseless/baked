﻿using Baked.Architecture;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static Baked.DependencyInjection.DependencyInjectionLayer;

namespace Baked.Testing;

public class TestingLayer : LayerBase<AddServices>
{
    readonly TestConfiguration _configuration = new();

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.Get<IServiceCollection>();

        services.AddHttpContextAccessor();

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
        yield return new Run(_configuration);
    }

    public class CreateConfigurationManager()
        : PhaseBase(PhaseOrder.Earliest)
    {
        protected override void Initialize()
        {
            Context.Add(new ConfigurationManager());
        }
    }

    class Run(TestConfiguration _configuration)
        : PhaseBase<IServiceCollection>(PhaseOrder.Latest)
    {
        protected override void Initialize(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.CreateScope();

            Context.Add<IServiceProvider>(serviceProvider);
            Context.Add<ITestRun>(new TestRun(_configuration));
        }
    }
}