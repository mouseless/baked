using Baked.CodeGeneration;
using Baked.Testing;

namespace Baked.Test;

public static class CompilerExtensions
{
    public static Compiler ACompiler(this Stubber _,
        string? code = default
    )
    {
        code ??= "public class Test { }";

        var descriptor = new GeneratedAssemblyDescriptor("Test");
        descriptor.Codes.Add(code);

        return new(descriptor);
    }
}