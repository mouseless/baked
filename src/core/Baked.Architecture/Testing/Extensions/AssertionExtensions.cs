﻿using Baked.Testing;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class AssertionExtensions
{
    [DoesNotReturn]
    public static void ShouldFail(this Spec _, string message = "") =>
        throw new AssertionException(message);

    [DoesNotReturn]
    public static void ShouldPass(this Spec _, string message = "") =>
        Assert.Pass(message);
}