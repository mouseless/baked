using Baked.Domain.Configuration;

namespace Baked.Domain.Inspection;

public class AttributeCaptureType(DomainModelContext _context, string? _orderInfo)
    : ICaptureType
{
    public string Id => _context.Identifier;
    public string? OrderInfo => _orderInfo;

    public string BuildTitle(Type type) =>
        $"[[{type.Name.Replace("Attribute", string.Empty)}]]";

    public object? ConvertTarget<T>(T? target) =>
        target;
}