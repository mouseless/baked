using Baked.Domain.Model;

using static Baked.Domain.Metadata.TypeMetadataModel;

namespace Baked.Domain.Metadata;

public class MetadataModelBuilder(MetadataModelBuilderOptions _options)
{
    public MetadataModel Build(DomainModel domain)
    {
        var types = new List<TypeMetadataModel>();
        foreach (var type in domain.Types)
        {
            if (!type.TryGetMetadata(out var metadata)) { continue; }

            var typeMetadataModel = Build(metadata);
            if (typeMetadataModel is null) { continue; }

            types.Add(typeMetadataModel);
        }

        return new(new(types));
    }

    TypeMetadataModel? Build(TypeModelMetadata type)
    {
        var attributes = ExtractAttributes(type, _options.TypeAttributes);
        if (_options.ExcludeTypesMissingAttributes && !attributes.Any())
        {
            return default;
        }

        var typeMetadataModel = new TypeMetadataModel(((IModel)type).Id, type.Name);
        typeMetadataModel.Attributes.AddRange(attributes.Select(a => new AttributeMetadataModel(a.GetType().Name)));

        return type.TryGetMembers(out var members) ? BuildMembers(typeMetadataModel, members) : typeMetadataModel;
    }

    TypeMetadataModel BuildMembers(TypeMetadataModel metadata, TypeModelMembers type)
    {
        foreach (var method in type.Methods)
        {
            var attributes = ExtractAttributes(method, _options.TypeAttributes);
            metadata.Methods.AddRange(attributes.Select(a => new MethodMetadataModel(a.GetType().Name, [])));
        }

        foreach (var property in type.Properties)
        {
            var attributes = ExtractAttributes(property, _options.TypeAttributes);
            metadata.Properties.AddRange(attributes.Select(a => new PropertyMetadataModel(a.GetType().Name, [])));
        }

        return metadata;
    }

    List<Attribute> ExtractAttributes(ICustomAttributesModel model, List<Type> candidates)
    {
        var result = new List<Attribute>();
        foreach (var attributeType in candidates)
        {
            if (attributeType.AllowsMultiple() && model.TryGetAll(attributeType, out var attributes))
            {
                result.AddRange(attributes);
            }
            else if (model.TryGet(attributeType, out var attribute))
            {
                result.Add(attribute);
            }
        }

        return result;
    }
}

public class MetadataModelBuilderOptions
{
    public List<Type> TypeAttributes { get; set; } = [];
    public bool ExcludeTypesMissingAttributes { get; set; }
}