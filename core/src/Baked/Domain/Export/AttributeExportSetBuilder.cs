using Baked.Domain.Model;

namespace Baked.Domain.Export;

public class AttributeExportSetBuilder(AttributeExport _export, IAttributeDataBuilder _builder)
{
    public AttributeExportSetModel Build(DomainModel domain)
    {
        var types = new List<TypeAttributeExportModel>();
        foreach (var type in domain.Types)
        {
            if (!type.TryGetMetadata(out var metadata)) { continue; }

            var typeMetadataModel = BuildMetadata(metadata);
            if (typeMetadataModel is null) { continue; }

            types.Add(typeMetadataModel);
        }

        return new(new(types));
    }

    TypeAttributeExportModel? BuildMetadata(TypeModelMetadata type)
    {
        var attributes = BuildAttributes(type, _export.TypeExports);
        if (!attributes.Any()) { return default; }

        var typeMetadataModel = new TypeAttributeExportModel(((IModel)type).Id, type.Name);
        typeMetadataModel.Attributes.AddRange(attributes);
        typeMetadataModel.GroupName = _export.GetTypeGroupName(type);

        return type.TryGetMembers(out var members) ? BuildMembers(typeMetadataModel, members) : typeMetadataModel;
    }

    TypeAttributeExportModel BuildMembers(TypeAttributeExportModel metadata, TypeModelMembers type)
    {
        foreach (var method in type.Methods)
        {
            var attributes = BuildAttributes(method, _export.MethodExports);
            if (!attributes.Any()) { continue; }

            var methodMetadata = new MethodAttributeExportModel(method.Name, attributes)
            {
                Parameters = BuildParameters(method)
            };

            metadata.Methods.Add(methodMetadata);
        }

        foreach (var property in type.Properties)
        {
            var attributes = BuildAttributes(property, _export.PropertyExports);
            if (!attributes.Any()) { continue; }

            var propertyMetadata = new PropertyAttributeExportModel(property.Name, attributes);
            metadata.Properties.Add(propertyMetadata);
        }

        return metadata;
    }

    List<ParameterAttributeExportModel> BuildParameters(MethodModel method)
    {
        var parameters = new List<ParameterAttributeExportModel>();
        foreach (var parameter in method.DefaultOverload.Parameters)
        {
            var attributes = BuildAttributes(parameter, _export.ParameterExports);
            if (!attributes.Any()) { continue; }

            var parameterMetadata = new ParameterAttributeExportModel(parameter.Name, attributes);
            parameters.Add(parameterMetadata);
        }

        return parameters;
    }

    List<AttributeExportModel> BuildAttributes(ICustomAttributesModel model, List<IAttributeExport> attributeExports)
    {
        var result = new List<AttributeExportModel>();

        foreach (var attributeExport in attributeExports)
        {
            if (attributeExport.Type.AllowsMultiple() && model.TryGetAll(attributeExport.Type, out var attributes))
            {
                result.AddRange(attributes.Where(a => attributeExport.AppliesTo(a, model)).Select(a => BuildAttribute(a, attributeExport)));
            }
            else if (model.TryGet(attributeExport.Type, out var attribute) && attributeExport.AppliesTo(attribute, model))
            {
                result.Add(BuildAttribute(attribute, attributeExport));
            }
        }

        return result;
    }

    AttributeExportModel BuildAttribute(object instance, IAttributeExport attributeExport)
    {
        var properties = _builder.Build(instance);
        foreach (var extension in attributeExport.PropertyExtensions)
        {
            properties.Add(extension((Attribute)instance));
        }

        var attributeMetadata = new AttributeExportModel(instance.GetType().Name)
        {
            Values = properties.Where(p => !attributeExport.RemoveProperty.Any(r => r(p))).ToDictionary(p => p.Name, p => p.Value)
        };

        return attributeMetadata;
    }
}