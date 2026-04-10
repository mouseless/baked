namespace Baked.Domain.Export;

public record PropertyExportModel(string Name, List<AttributeExportModel> Attributes);