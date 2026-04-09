using System.Runtime.CompilerServices;

namespace Baked.Business;

public record MetadataProperty(string Name, object? Value)
{
    public MetadataProperty(object? value,
        [CallerArgumentExpression("value")] string name = ""
    ) : this(name, value) { }
}