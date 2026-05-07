namespace Baked.Business;

[AttributeUsage(AttributeTargets.Method)]
public class QueryMethodAttribute : Attribute
{
    public bool AllParametersAreOptional { get; set; }
    public string? PrimaryParameterName { get; set; }
}