using Baked.Testing;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class AssertionExtensions
{
    extension(Spec _)
    {
        [DoesNotReturn]
        public void ShouldFail(
            string message = ""
        ) => throw new AssertionException(message);

        [DoesNotReturn]
        public Task ShouldFailAsync(
            string message = ""
        ) => throw new AssertionException(message);

        [DoesNotReturn]
        public void ShouldPass(
            string message = ""
        ) => Assert.Pass(message);

        [DoesNotReturn]
        public Task ShouldPassAsync(
            string message = ""
        )
        {
            Assert.Pass(message);

            return Task.CompletedTask;
        }
    }
}