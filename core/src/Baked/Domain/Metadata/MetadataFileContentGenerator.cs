namespace Baked.Domain.Metadata;

public class MetadataFileContentGenerator(MetadataFileContentGenerator.Options _options)
{
    public Dictionary<string, string> Generate(MetadataSetModel model)
    {
        var contents = new Dictionary<string, string>();

        foreach (var type in model.Types)
        {
            var result = _options.Serializer.Serialize(type);
            var fileName = _options.GetFileName(type);
            if (contents.ContainsKey(fileName))
            {
                contents[fileName] += result;
            }
            else
            {
                contents[fileName] = result;
            }
        }

        return contents;
    }

    public class Options
    {
        public ITypeMetadataSerializer Serializer { get; set; } = new KdlMetadataSerializer();
        public Func<TypeMetadataModel, string> GetFileName { get; set; } = type => type.GroupName;
    }
}
