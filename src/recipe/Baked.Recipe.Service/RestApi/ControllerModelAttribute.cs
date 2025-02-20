namespace Baked.RestApi;

[AttributeUsage(AttributeTargets.Class)]
public class ControllerModelAttribute : Attribute
{
    public string? ClassName { get; set; }
    public string? GroupName { get; set; }
}