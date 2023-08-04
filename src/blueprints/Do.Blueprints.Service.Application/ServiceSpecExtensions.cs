using Do.MockOverrider;
using Do.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Reflection;

namespace Do;

public static class ServiceSpecExtensions
{
    #region MockOverrider

    public static T Create<T>(this Stubber _, params object?[] mockOverrides) where T : notnull
    {
        var overrider = ServiceSpec.ServiceProvider.GetRequiredService<IMockOverrider>();

        foreach (var mocked in mockOverrides)
        {
            if (mocked is null) { continue; }

            overrider.Override(mocked);
        }

        return ServiceSpec.ServiceProvider.GetRequiredService<T>();
    }

    #endregion

    #region Reflection

    public static PropertyInfo? PropertyOf<T>(this Stubber _, string name) =>
        typeof(T).GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

    public static void ShouldBe<T>(this Type source) => source.ShouldBe(typeof(T));

    public static void ShouldBeAbstract(this PropertyInfo source)
    {
        source.GetGetMethod(true).ShouldNotBeNull();
        source.GetGetMethod(true)!.ShouldBeAbstract();
    }

    public static void ShouldBeVirtual(this PropertyInfo source)
    {
        source.GetGetMethod(true).ShouldNotBeNull();
        source.GetGetMethod(true)!.ShouldBeVirtual();
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
}
