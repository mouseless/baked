using Baked.CodeGeneration;
using Baked.Testing;

namespace Baked.Test;

public static class CompilerExtensions
{
    extension(Stubber _)
    {
        public Compiler ACompiler(
            string? code = default,
            IEnumerable<Type>? referencesFrom = default
        )
        {
            code ??= "public class Test { }";
            referencesFrom ??= [typeof(string)];

            var descriptor = new GeneratedAssemblyDescriptor("Test");
            descriptor.Codes.Add(code);

            foreach (var type in referencesFrom)
            {
                descriptor.AddReferenceFrom(type);
            }

            return new(descriptor);
        }
    }
}