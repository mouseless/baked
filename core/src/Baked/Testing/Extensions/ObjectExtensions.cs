using Baked.Testing;
using Shouldly;

namespace Baked;

public static class ObjectExtensions
{
    extension(Stubber _)
    {
        public T AnInstanceOf<T>()
        {
            var result = Activator.CreateInstance(typeof(T));

            result.ShouldNotBeNull();

            return (T)result;
        }
    }
}