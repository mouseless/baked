using Baked.Domain.Model;

namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableAttribute : Attribute
{
    public bool IsAsync { get; set; } = default!;
    public Type ServiceType { get; set; } = default!;
    public bool FromFactory { get; set; } = false;
    public string LocateSingleMethodName { get; set; } = default!;
    public string? LocateMultipleMethodName { get; set; }
    public TypeModel? CastTo { get; set; }
}