using Baked.Core;
using Baked.Core.Mock;
using Baked.Testing;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Baked;

public static class MockCoreExtensions
{
    extension(CoreConfigurator _)
    {
        public MockCoreFeature Mock() =>
            new();
    }

    extension(Mocker mockMe)
    {
        public TimeProvider TheTime(
            DateTime? now = default,
            bool passSomeTime = false,
            bool reset = false
        )
        {
            var fakeTimeProvider = (ResettableFakeTimeProvider)mockMe.Spec.GiveMe.The<TimeProvider>();

            if (reset)
            {
                fakeTimeProvider.Reset();
            }

            if (now is not null)
            {
                fakeTimeProvider.SetUtcNow(new(now.Value, fakeTimeProvider.LocalTimeZone.BaseUtcOffset));
            }

            if (passSomeTime)
            {
                fakeTimeProvider.Advance(TimeSpan.FromSeconds(1));
            }

            return fakeTimeProvider;
        }

        public void ASetting<T>(
            string? key = default,
            T? value = default
        ) => mockMe.ASetting(key: key, value: $"{value}");

        public void ASetting(
            string? key = default,
            string? value = default
        )
        {
            key ??= "Test:Configuration";
            value ??= "value";

            var settings = mockMe.Spec.GiveMe.The<FakeSettings>();

            settings[key] = value;
        }

        public IConfiguration TheConfiguration(
            Func<string, string?>? defaultValueProvider = default
        )
        {
            defaultValueProvider ??= _ => default;

            var configuration = mockMe.Spec.GiveMe.The<IConfiguration>();
            var settings = mockMe.Spec.GiveMe.The<FakeSettings>();

            Moq.Mock.Get(configuration)
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
}