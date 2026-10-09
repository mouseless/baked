namespace Baked.Theme.Default;

[AttributeUsage(AttributeTargets.Method)]
public class UiAction : Attribute
{
    public bool HideInLists { get; set; }
    public string? RoutePathBack { get; set; }
}