using Do.CodeGeneration;
using Do.Testing;

namespace Do.Test;

public static class CompilerExtensions
{
    public static Compiler ACompiler(this Stubber giveMe,
        string? code = default
    )
    {
        code ??= "public class Test { }";

        var descriptor = new GeneratedAssemblyDescriptor("Test");
        descriptor.Codes.Add(code);

        return new(descriptor);
    }
}
