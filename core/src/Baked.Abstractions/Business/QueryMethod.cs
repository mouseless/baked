namespace Baked.Business;

[AttributeUsage(AttributeTargets.Method)]
public class QueryMethod : Attribute
{
    public bool AllParametersAreOptional { get; set; }
    public string? PrimaryParameterName { get; set; }
}