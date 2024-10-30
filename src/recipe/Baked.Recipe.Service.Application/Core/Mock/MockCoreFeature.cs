using Baked.Architecture;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Core.Mock;

public class MockCoreFeature : IFeature<CoreConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<TimeProvider, ResettableFakeTimeProvider>();
            services.AddSingleton<ITextTransformer, HumanizerTextTransformer>();
            services.AddSingleton<FakeSettings>();
        });

        configurator.ConfigureTestConfiguration(test =>
        {
            test.Mocks.Add<IConfiguration>(singleton: true);
            test.SetUps.Add(spec =>
            {
                spec.MockMe.TheConfiguration();

                // This is the initial release date. Do not change this to avoid
                // potential "Cannot go back in time." errors.
                spec.MockMe.TheTime(now: new DateTime(2023, 06, 15, 16, 59, 00), reset: true);
            });
            test.TearDowns.Add(spec => spec.GiveMe.The<FakeSettings>().Clear());
        });
    }
}