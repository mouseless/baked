using Baked.Domain.Model;

namespace Baked.Domain.Metadata;

public class MetadataModelBuilder(MetadataModelBuilderOptions _options)
{
    public TypeMetadataModel Build(TypeModelMetadata type)
    {
        var typeMetadataModel = new TypeMetadataModel(type.Name);
        foreach (var attribute in ExtractAttributes(type, _options.TypeAttributes))
        {
            typeMetadataModel.Attributes.Add(new(attribute.GetType().Name));
        }

        return typeMetadataModel;
    }

    List<Attribute> ExtractAttributes(TypeModelMetadata type, List<Type> candidates)
    {
        var result = new List<Attribute>();
        foreach (var attributeType in candidates)
        {
            if (attributeType.AllowsMultiple())
            {
                result.AddRange(type.GetAll(attributeType));
            }
            else
            {
                result.Add(type.Get(attributeType));
            }
        }

        return result;
    }
}

public class MetadataModelBuilderOptions
{
    public List<Type> TypeAttributes { get; set; } = [];
}