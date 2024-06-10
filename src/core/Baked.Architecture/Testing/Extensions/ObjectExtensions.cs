using Do.Testing;
using Shouldly;

namespace Do;

public static class ObjectExtensions
{
    public static T AnInstanceOf<T>(this Stubber _)
    {
        var result = Activator.CreateInstance(typeof(T));

        result.ShouldNotBeNull();

        return (T)result;
    }
}