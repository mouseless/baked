using Do.MockOverrider;
using Do.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Do;

public static class ServiceSpecExtensions
{
    #region MockOverrider

    public static T The<T>(this Stubber _, params object?[] mockOverrides) where T : notnull =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService<T>(mockOverrides);

    public static T An<T>(this Stubber _, params object?[] mockOverrides) where T : notnull =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService<T>(mockOverrides);

    public static T A<T>(this Stubber _, params object?[] mockOverrides) where T : notnull =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService<T>(mockOverrides);

    static T OverrideMocksAndGetRequiredService<T>(this IServiceProvider serviceProvider, params object?[] mockOverrides) where T : notnull
    {
        var overrider = serviceProvider.GetRequiredService<IMockOverrider>();

        foreach (var mocked in mockOverrides)
        {
            if (mocked is null) { continue; }

            overrider.Override(mocked);
        }

        return serviceProvider.GetRequiredService<T>();
    }

    #endregion

    #region Reflection

    public static PropertyInfo? PropertyOf<T>(this Stubber _, string name) =>
        typeof(T).GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

    public static void ShouldBe<T>(this Type source) => source.ShouldBe(typeof(T));

    public static void ShouldBeAbstract(this PropertyInfo source)
    {
        var getMethod = source.GetGetMethod(true);

        getMethod.ShouldNotBeNull();
        getMethod.ShouldBeAbstract();
    }

    public static void ShouldBeVirtual(this PropertyInfo source)
    {
        var getMethod = source.GetGetMethod(true);

        getMethod.ShouldNotBeNull();
        getMethod.ShouldBeVirtual();
    }

    public static MethodInfo? MethodOf<T>(this Stubber _, string name) =>
        typeof(T).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

    public static void ShouldBeAbstract(this MethodInfo source)
    {
        source.IsAbstract.ShouldBeTrue();
    }

    public static void ShouldBeVirtual(this MethodInfo source)
    {
        source.IsVirtual.ShouldBeTrue();
    }

    public static void ShouldHaveOneParameter<T>(this MethodInfo source)
    {
        source.GetParameters().Length.ShouldBe(1);
        source.GetParameters().First().ParameterType.ShouldBe<T>();
    }

    #endregion

    #region Url Extensions

    public static Uri AUrl(this Stubber giveMe,
        string? url = default
    )
    {
        url ??= $"https://www.{Regex.Replace(giveMe.AGuid().ToString(), "[0-9]", "x")}.com";

        return new(url);
    }

    public static void ShouldBe(this Uri? uri, string urlString) => uri?.ToString().ShouldBe(urlString);

    #endregion

    #region Guid Extensions

    public static Guid AGuid(this Stubber _,
        string? guid = default
    ) => guid is null ? Guid.NewGuid() : Guid.Parse(guid);

    #endregion
}
