namespace Baked.Domain.Export;

public record MethodExportModel(string Name, List<AttributeExportModel> Attributes)
{
    public List<ParameterExportModel> Parameters { get; init; } = [];
}