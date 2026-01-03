namespace Baked.Playground.Business;

public class CustomAttribute : Attribute
{
    public string Value { get; set; } = string.Empty;
}