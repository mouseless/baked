using Baked.Architecture;
using Baked.MockOverrider;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class MockOverriderExtensions
{
    extension(List<IFeature> features)
    {
        public void AddMockOverrider(Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>> configure) =>
            features.Add(configure(new()));
    }

    extension(Stubber giveMe)
    {
        public T The<T>(object? mockOverride, params IEnumerable<object?> otherMockOverrides) where T : notnull =>
            giveMe.TheServiceProvider().OverrideMocksAndGetRequiredService<T>([mockOverride, .. otherMockOverrides]);

        public object The(Type type, object? mockOverride, params IEnumerable<object?> otherMockOverrides) =>
            giveMe.TheServiceProvider().OverrideMocksAndGetRequiredService(type, [mockOverride, .. otherMockOverrides]);

        public T An<T>(object? mockOverride, params IEnumerable<object?> otherMockOverrides) where T : notnull =>
            giveMe.TheServiceProvider().OverrideMocksAndGetRequiredService<T>([mockOverride, .. otherMockOverrides]);

        public object An(Type type, object? mockOverride, params IEnumerable<object?> otherMockOverrides) =>
            giveMe.TheServiceProvider().OverrideMocksAndGetRequiredService(type, [mockOverride, .. otherMockOverrides]);

        public T A<T>(object? mockOverride, params IEnumerable<object?> otherMockOverrides) where T : notnull =>
            giveMe.TheServiceProvider().OverrideMocksAndGetRequiredService<T>([mockOverride, .. otherMockOverrides]);

        public object A(Type type, params IEnumerable<object?> mockOverrides) =>
            giveMe.TheServiceProvider().OverrideMocksAndGetRequiredService(type, mockOverrides);
    }

    extension(IServiceProvider serviceProvider)
    {
#pragma warning disable IDE0051
        T OverrideMocksAndGetRequiredService<T>(params IEnumerable<object?> mockOverrides) where T : notnull =>
            (T)serviceProvider.OverrideMocksAndGetRequiredService(typeof(T), mockOverrides);
        object OverrideMocksAndGetRequiredService(Type type, params IEnumerable<object?> mockOverrides)
        {
            var overrider = serviceProvider.GetRequiredService<IMockOverrider>();

            foreach (var mocked in mockOverrides)
            {
                if (mocked is null) { continue; }

                overrider.Override(mocked);
            }

            return serviceProvider.GetRequiredService(type);
        }
#pragma warning restore IDE0051
    }
}