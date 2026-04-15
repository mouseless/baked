namespace Baked.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequireUserAttribute(
    string[]? claims = default
) : Attribute
{
    public bool Override { get; set; }

    public string[] Claims => claims ?? [];
}