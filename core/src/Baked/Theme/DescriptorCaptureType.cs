using Baked.Domain.Inspection;
using Baked.Ui;

namespace Baked.Theme;

public class DescriptorCaptureType(ComponentContext _context, string? _orderInfo)
    : ICaptureType
{
    public string Id => $"{_context.Path}";
    public string? OrderInfo => _orderInfo;

    public string BuildTitle(Type type) =>
        $"<{type.GetName(includeDeclaringTypes: true)}>";

    public object? ConvertTarget<T>(T? target) =>
        target is IComponentDescriptor descriptor
            ? descriptor.Schema
            : target;
}