namespace Baked.Domain.Export;

public record ParameterExportModel(string Name, List<AttributeExportModel> Attributes);