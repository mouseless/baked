using Do.Architecture;
using Do.Configuration;
using Do.Testing;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Do;

public static class ConfigurationExtensions
{
    public static void AddConfiguration(this List<ILayer> source) => source.Add(new ConfigurationLayer());
    public static void ConfigureConfigurationBuilder(this LayerConfigurator source, Action<IConfigurationBuilder> configuration) => source.Configure(configuration);

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
