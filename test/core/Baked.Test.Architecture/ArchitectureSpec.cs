using Baked.Testing;

namespace Baked.Test;

public abstract class ArchitectureSpec : Spec
{
    static ArchitectureSpec() =>
        Init(_ => { });
}