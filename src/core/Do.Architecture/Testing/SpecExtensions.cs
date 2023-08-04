using Do.Testing;
using NUnit.Framework;
using Shouldly;

namespace Do;

public static class SpecExtensions
{
    #region Assertion

    public static void ShouldFail(this Spec source, string message = "") => Assert.Fail(message);
    public static void ShouldPass(this Spec source, string message = "") => Assert.Pass(message);

    #endregion

    #region Object

    public static T An<T>(this Stubber source) => source.A<T>();
    public static T A<T>(this Stubber source)
    {
        var result = Activator.CreateInstance(typeof(T));

        result.ShouldNotBeNull();

        return (T)result!;
    }

    #endregion
}
