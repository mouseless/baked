using Do.Architecture;
using Do.Testing;
using Moq;
using Moq.Language.Flow;

namespace Do;

public static class TestingExtensions
{
    public static void AddTesting(this List<ILayer> source) => source.Add(new TestingLayer());
    public static IServiceProvider GetServiceProvider(this ApplicationContext source) => source.Get<IServiceProvider>();
    public static void ConfigureTestConfiguration(this LayerConfigurator source, Action<TestConfiguration> configuration) => source.Configure(configuration);

    public static void Add<T>(this IMockCollection source, bool singleton = false, Action<Mock<T>>? setup = default) where T : class =>
        source.Add(new(
            Type: typeof(T),
            Singleton: singleton,
            Setup: setup == default ? default : obj => setup((Mock<T>)obj)
        ));

    public static void Returns<TMock, TResult>(this ISetup<TMock, TResult> source, params TResult[] results) where TMock : class
    {
        int currentResultIndex = 0;

        source.Returns(() =>
        {
            if (currentResultIndex >= results.Length)
            {
                currentResultIndex = 0;
            }

            return results[currentResultIndex++];
        });
    }

    public static void ReturnsAsync<TMock, TResult>(this ISetup<TMock, Task<TResult>> source, params TResult[] results) where TMock : class
    {
        int currentResultIndex = 0;

        source.ReturnsAsync(() =>
        {
            if (currentResultIndex >= results.Length)
            {
                currentResultIndex = 0;
            }

            return results[currentResultIndex++];
        });
    }
}
