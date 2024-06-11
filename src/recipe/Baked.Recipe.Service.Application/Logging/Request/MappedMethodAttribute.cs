namespace Baked.Logging.Request;

[AttributeUsage(AttributeTargets.Method)]
public class MappedMethodAttribute(string typeFullName, string methodName)
    : Attribute
{
    public string TypeFullName { get; } = typeFullName;
    public string MethodName { get; } = methodName;
}