using Baked.Testing;

namespace Baked.Test;

public static class IntegerExtensions
{
    extension(Stubber _)
    {
        public int AnInt() => 42;
    }
}