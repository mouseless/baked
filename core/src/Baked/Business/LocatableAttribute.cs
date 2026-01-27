using Baked.Domain.Model;

namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableAttribute(Type ServiceType, string LocateSingleMethodName) : Attribute
{
    public Type ServiceType { get; set; } = ServiceType;
    public string LocateSingleMethodName { get; set; } = LocateSingleMethodName;
    public bool IsFactory { get; set; } = false;
    public bool IsAsync { get; set; } = false;
    public string? LocateMultipleMethodName { get; set; }
    public TypeModel? CastTo { get; set; }
}