using Baked.Domain.Model;

namespace Baked.Domain.Export;

public class ExportSetBuilder(ExportConfiguration _export, IAttributePropertyBuilder _builder)
{
    public ExportSetModel Build(DomainModel domain)
    {
        var types = new List<TypeExportModel>();
        foreach (var type in domain.Types)
        {
            if (!type.TryGetMetadata(out var metadata)) { continue; }

            var typeMetadataModel = BuildMetadata(metadata);
            if (typeMetadataModel is null) { continue; }

            types.Add(typeMetadataModel);
        }

        return new(new(types));
    }

    TypeExportModel? BuildMetadata(TypeModelMetadata type)
    {
        var attributes = BuildAttributes(type, _export.Type);
        if (!attributes.Any()) { return default; }

        var typeMetadataModel = new TypeExportModel(((IModel)type).Id, type.Name);
        typeMetadataModel.Attributes.AddRange(attributes);
        typeMetadataModel.GroupName = _export.GetTypeGroupName(type);

        return type.TryGetMembers(out var members) ? BuildMembers(typeMetadataModel, members) : typeMetadataModel;
    }

    TypeExportModel BuildMembers(TypeExportModel metadata, TypeModelMembers type)
    {
        foreach (var method in type.Methods)
        {
            var attributes = BuildAttributes(method, _export.Method);
            if (!attributes.Any()) { continue; }

            var methodMetadata = new MethodExportModel(method.Name, attributes)
            {
                Parameters = BuildParameters(method)
            };

            metadata.Methods.Add(methodMetadata);
        }

        foreach (var property in type.Properties)
        {
            var attributes = BuildAttributes(property, _export.Property);
            if (!attributes.Any()) { continue; }

            var propertyMetadata = new PropertyExportModel(property.Name, attributes);
            metadata.Properties.Add(propertyMetadata);
        }

        return metadata;
    }

    List<ParameterExportModel> BuildParameters(MethodModel method)
    {
        var parameters = new List<ParameterExportModel>();
        foreach (var parameter in method.DefaultOverload.Parameters)
        {
            var attributes = BuildAttributes(parameter, _export.Parameter);
            if (!attributes.Any()) { continue; }

            var parameterMetadata = new ParameterExportModel(parameter.Name, attributes);
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
        foreach (var property in attributeExport.Properties)
        {
            properties.Add(property((Attribute)instance));
        }

        var attributeMetadata = new AttributeExportModel(instance.GetType().Name)
        {
            Values = properties.Where(p => !attributeExport.RemoveProperty.Any(r => r(p))).ToDictionary(p => p.Name, p => p.Value)
        };

        return attributeMetadata;
    }
}