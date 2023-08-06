using Do.Testing;
using NUnit.Framework;
using Shouldly;

namespace Do;

public static class SpecExtensions
{
    #region Assertion

    public static void ShouldFail(this Spec _, string message = "") => Assert.Fail(message);
    public static void ShouldPass(this Spec _, string message = "") => Assert.Pass(message);

    #endregion

    #region Object

    public static T AnInstanceOf<T>(this Stubber _)
    {
        var result = Activator.CreateInstance(typeof(T));

        result.ShouldNotBeNull();

        return (T)result!;
    }

    #endregion
}
