using Do.Architecture;
using Do.Testing;
using Moq;

namespace Do;

public static class TestingExtensions
{
    public static void AddTesting(this List<ILayer> source) => source.Add(new TestingLayer());
    public static void ConfigureTestConfiguration(this LayerConfigurator source, Action<TestConfiguration> configuration) => source.Configure(configuration);

    public static void Add<T>(this IMockCollection source, bool singleton = false, Action<Mock<T>>? setup = default) where T : class =>
        source.Add(new(
            Type: typeof(T),
            Singleton: singleton,
            Setup: setup == default ? default : obj => setup((Mock<T>)obj)
        ));
}
