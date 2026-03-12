using Baked.Architecture;
using Baked.Testing;
using Moq;
using Moq.Language.Flow;

namespace Baked;

public static class TestingExtensions
{
    extension(List<ILayer> layers)
    {
        public void AddTesting() =>
            layers.Add(new TestingLayer());
    }

    extension(LayerConfigurator configurator)
    {
        public void ConfigureTestConfiguration(Action<TestConfiguration> configuration) =>
            configurator.Configure(configuration);
    }

    extension(IMockCollection mocks)
    {
        public void Add<T>(
            bool singleton = false,
            Action<Mock<T>>? setup = default
        ) where T : class =>
            mocks.Add(new(
                Type: typeof(T),
                Singleton: singleton,
                Setup: setup == default ? default : obj => setup((Mock<T>)obj)
            ));

        public void Add(Type service,
            bool singleton = false,
            Action<Mock>? setup = default
        ) =>
            mocks.Add(new(
                Type: service,
                Singleton: singleton,
                Setup: setup == default ? default : obj => setup(obj)
            ));
    }

    extension<TMock, TResult>(ISetup<TMock, TResult> setup) where TMock : class
    {
        public void Returns(params IList<TResult> results)
        {
            int currentResultIndex = 0;

            setup.Returns(() =>
            {
                if (currentResultIndex >= results.Count)
                {
                    currentResultIndex = 0;
                }

                return results[currentResultIndex++];
            });
        }
    }

    extension<TMock, TResult>(ISetup<TMock, Task<TResult>> setup) where TMock : class
    {
        public void ReturnsAsync(params IList<TResult> results)
        {
            int currentResultIndex = 0;

            setup.ReturnsAsync(() =>
            {
                if (currentResultIndex >= results.Count)
                {
                    currentResultIndex = 0;
                }

                return results[currentResultIndex++];
            });
        }
    }

}