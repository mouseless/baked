namespace Baked.Theme.Default;

[AttributeUsage(AttributeTargets.Method)]
public class ActionAttribute : Attribute
{
    public string? RoutePath { get; set; }
    public string? RoutePathBack { get; set; }
}