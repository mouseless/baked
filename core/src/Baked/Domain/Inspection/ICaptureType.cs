namespace Baked.Domain.Inspection;

internal interface ICaptureType
{
    string Id { get; }
    string? OrderInfo { get; }

    string BuildTitle(Type type);
    object? ConvertTarget<T>(T? target);
}