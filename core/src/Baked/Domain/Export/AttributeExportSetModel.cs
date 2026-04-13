using Baked.Domain.Model;

namespace Baked.Domain.Export;

public record AttributeExportSetModel(ModelCollection<TypeAttributeExportModel> Types);