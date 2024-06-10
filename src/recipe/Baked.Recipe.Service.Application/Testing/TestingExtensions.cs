using Baked.Architecture;
using Baked.Testing;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Language.Flow;

namespace Baked;

public static class TestingExtensions
{
    public static void AddTesting(this List<ILayer> source) =>
        source.Add(new TestingLayer());

    public static void ConfigureTestConfiguration(this LayerConfigurator source, Action<TestConfiguration> configuration) =>
        source.Configure(configuration);

    public static void Add<T>(this IMockCollection source, bool singleton = false, Action<Mock<T>>? setup = default) where T : class =>
        source.Add(new(
            Type: typeof(T),
            Singleton: singleton,
            Setup: setup == default ? default : obj => setup((Mock<T>)obj)
        ));

    public static void Add(this IMockCollection source, Type service, bool singleton = false, Action<Mock>? setup = default) =>
        source.Add(new(
            Type: service,
            Singleton: singleton,
            Setup: setup == default ? default : obj => setup(obj)
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