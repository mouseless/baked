using System.Runtime.CompilerServices;

namespace Baked.Business;

public record AttributeProperty(string Name, object? Value)
{
    public AttributeProperty(object? value,
        [CallerArgumentExpression("value")] string name = ""
    ) : this(name, value) { }
}