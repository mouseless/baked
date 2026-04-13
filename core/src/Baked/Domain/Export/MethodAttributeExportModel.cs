namespace Baked.Domain.Export;

public record MethodAttributeExportModel(string Name, List<AttributeExportModel> Attributes)
{
    public List<ParameterAttributeExportModel> Parameters { get; init; } = [];
}