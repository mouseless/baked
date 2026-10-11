namespace Baked.Binding;

[AttributeUsage(AttributeTargets.Method)]
public class MappedMethod(string typeFullName, string methodName)
    : Attribute(), IExportOptions
{
    public string TypeFullName { get; } = typeFullName;
    public string MethodName { get; } = methodName;
    string IExportOptions.Name => "Mapped";
}