using Baked.Architecture;
using Baked.MockOverrider;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class MockOverriderExtensions
{
    public static void AddMockOverrider(this List<IFeature> features, Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>> configure) =>
        features.Add(configure(new()));

    public static T The<T>(this Stubber giveMe, object? mockOverride, params object?[] otherMockOverrides) where T : notnull =>
        giveMe.TheServiceProvider().OverrideMocksAndGetRequiredService<T>([mockOverride, .. otherMockOverrides]);

    public static object The(this Stubber giveMe, Type type, object? mockOverride, params object?[] otherMockOverrides) =>
        giveMe.TheServiceProvider().OverrideMocksAndGetRequiredService(type, [mockOverride, .. otherMockOverrides]);

    public static T An<T>(this Stubber giveMe, object? mockOverride, params object?[] otherMockOverrides) where T : notnull =>
        giveMe.TheServiceProvider().OverrideMocksAndGetRequiredService<T>([mockOverride, .. otherMockOverrides]);

    public static object An(this Stubber giveMe, Type type, object? mockOverride, params object?[] otherMockOverrides) =>
        giveMe.TheServiceProvider().OverrideMocksAndGetRequiredService(type, [mockOverride, .. otherMockOverrides]);

    public static T A<T>(this Stubber giveMe, object? mockOverride, params object?[] otherMockOverrides) where T : notnull =>
        giveMe.TheServiceProvider().OverrideMocksAndGetRequiredService<T>([mockOverride, .. otherMockOverrides]);

    public static object A(this Stubber giveMe, Type type, params object?[] mockOverrides) =>
        giveMe.TheServiceProvider().OverrideMocksAndGetRequiredService(type, mockOverrides);

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