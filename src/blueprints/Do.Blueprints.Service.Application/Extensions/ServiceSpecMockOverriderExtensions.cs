using Do.MockOverrider;
using Do.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Do;

public static class ServiceSpecMockOverriderExtensions
{
    public static T The<T>(this Stubber _, params object?[] mockOverrides) where T : notnull =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService<T>(mockOverrides);

    public static object The(this Stubber _, Type type, params object?[] mockOverrides) =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService(type, mockOverrides);

    public static T An<T>(this Stubber _, params object?[] mockOverrides) where T : notnull =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService<T>(mockOverrides);

    public static object An(this Stubber _, Type type, params object?[] mockOverrides) =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService(type, mockOverrides);

    public static T A<T>(this Stubber _, params object?[] mockOverrides) where T : notnull =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService<T>(mockOverrides);

    public static object A(this Stubber _, Type type, params object?[] mockOverrides) =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService(type, mockOverrides);

    static T OverrideMocksAndGetRequiredService<T>(this IServiceProvider serviceProvider, params object?[] mockOverrides) where T : notnull =>
        (T)serviceProvider.OverrideMocksAndGetRequiredService(typeof(T), mockOverrides);

    static object OverrideMocksAndGetRequiredService(this IServiceProvider serviceProvider, Type type, params object?[] mockOverrides)
    {
        var overrider = serviceProvider.GetRequiredService<IMockOverrider>();

        foreach (var mocked in mockOverrides)
        {
            if (mocked is null) { continue; }

            overrider.Override(mocked);
        }

        return serviceProvider.GetRequiredService(type);
    }
}
