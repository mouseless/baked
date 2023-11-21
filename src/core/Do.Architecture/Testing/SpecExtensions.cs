using Do.Testing;
using NUnit.Framework;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace Do;

public static class SpecExtensions
{
    #region Assertion

    [DoesNotReturn]
    public static void ShouldFail(this Spec _, string message = "") => throw new AssertionException(message);
    [DoesNotReturn]
    public static void ShouldPass(this Spec _, string message = "") => Assert.Pass(message);

    #endregion

    #region Object

    public static T AnInstanceOf<T>(this Stubber _)
    {
        var result = Activator.CreateInstance(typeof(T));

        result.ShouldNotBeNull();

        return (T)result;
    }

    #endregion
}
