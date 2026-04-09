namespace Baked.Domain.Metadata;

public interface ITypeMetadataSerializer
{
    string FileType { get; }

    string Serialize(TypeMetadataModel model);
}