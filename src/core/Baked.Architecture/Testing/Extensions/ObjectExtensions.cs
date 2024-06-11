using Baked.Testing;
using Shouldly;

namespace Baked;

public static class ObjectExtensions
{
    public static T AnInstanceOf<T>(this Stubber _)
    {
        var result = Activator.CreateInstance(typeof(T));

        result.ShouldNotBeNull();

        return (T)result;
    }
}