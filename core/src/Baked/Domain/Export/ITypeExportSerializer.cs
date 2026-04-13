namespace Baked.Domain.Export;

public interface ITypeExportSerializer
{
    string FileExtension { get; }

    string Serialize(TypeAttributeExportModel model);
}