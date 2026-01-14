namespace Baked.Business;

[AttributeUsage(AttributeTargets.Property)]
public class IdAttribute : Attribute
{
    public string Type { get; set; } = default!;
    public string Key { get; set; } = default!;
}