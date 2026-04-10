using Baked.Domain.Model;

namespace Baked.Domain.Export;

public record ExportSetModel(ModelCollection<TypeExportModel> Types);