using System.Reflection;

namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    Type ReturnType
)
{
    public MethodModel(MethodInfo methodInfo) : this(methodInfo.Name, methodInfo.ReturnType) { }
}

