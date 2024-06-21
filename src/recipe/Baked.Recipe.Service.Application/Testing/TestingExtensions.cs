using Baked.Architecture;
using Baked.Testing;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Language.Flow;

namespace Baked;

public static class TestingExtensions
{
    public static void AddTesting(this List<ILayer> layers) =>
        layers.Add(new TestingLayer());

    public static void ConfigureTestConfiguration(this LayerConfigurator configurator, Action<TestConfiguration> configuration) =>
        configurator.Configure(configuration);

    public static void Add<T>(this IMockCollection mocks, bool singleton = false, Action<Mock<T>>? setup = default) where T : class =>
        mocks.Add(new(
            Type: typeof(T),
            Singleton: singleton,
            Setup: setup == default ? default : obj => setup((Mock<T>)obj)
        ));

    public static void Add(this IMockCollection mocks, Type service, bool singleton = false, Action<Mock>? setup = default) =>
        mocks.Add(new(
            Type: service,
            Singleton: singleton,
            Setup: setup == default ? default : obj => setup(obj)
        ));

    public static void Returns<TMock, TResult>(this ISetup<TMock, TResult> setup, params TResult[] results) where TMock : class
    {
        int currentResultIndex = 0;

        setup.Returns(() =>
        {
            if (currentResultIndex >= results.Length)
            {
                currentResultIndex = 0;
            }

            return results[currentResultIndex++];
        });
    }

    public static void ReturnsAsync<TMock, TResult>(this ISetup<TMock, Task<TResult>> setup, params TResult[] results) where TMock : class
    {
        int currentResultIndex = 0;

        setup.ReturnsAsync(() =>
        {
            if (currentResultIndex >= results.Length)
            {
                currentResultIndex = 0;
            }

            return results[currentResultIndex++];
        });
    }

    public static void ASetting<T>(this Mocker mockMe,
        string? key = default,
        T? value = default
    ) => mockMe.ASetting(key: key, value: $"{value}");

    public static void ASetting(this Mocker mockMe,
        string? key = default,
        string? value = default
    )
    {
        key ??= "Test:Configuration";
        value ??= "value";

        var spec = (ServiceSpec)mockMe.Spec;

        spec.Settings[key] = value;
    }

    internal static IConfiguration TheConfiguration(this Mocker mockMe,
        Func<string, string?>? defaultValueProvider = default,
        Dictionary<string, string>? settings = default
    )
    {
        defaultValueProvider ??= _ => default;
        settings ??= [];

        var configuration = mockMe.Spec.GiveMe.The<IConfiguration>();

        Mock.Get(configuration)
           .Setup(c => c.GetSection(It.IsAny<string>())).Returns((string key) =>
           {
               var mockSection = new Mock<IConfigurationSection>();

               mockSection.Setup(s => s.Value).Returns(() =>
               {
                   if (settings.TryGetValue(key, out var result))
                   {
                       return result;
                   }

                   return defaultValueProvider(key);
               });

               return mockSection.Object;
           });

        return configuration;
    }
}