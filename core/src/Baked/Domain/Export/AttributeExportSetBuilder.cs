using Baked.Business;
using Baked.Domain.Model;

namespace Baked.Domain.Export;

public class AttributeExportSetBuilder(AttributeExport _options)
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
        var attributes = BuildAttributes(type, _options.TypeAttributes);
        if (!attributes.Any()) { return default; }

        var typeMetadataModel = new TypeExportModel(((IModel)type).Id, type.Name);
        typeMetadataModel.Attributes.AddRange(attributes);
        typeMetadataModel.GroupName = _options.GetTypeGroupName(type);

        return type.TryGetMembers(out var members) ? BuildMembers(typeMetadataModel, members) : typeMetadataModel;
    }

    TypeExportModel BuildMembers(TypeExportModel metadata, TypeModelMembers type)
    {
        foreach (var method in type.Methods)
        {
            var attributes = BuildAttributes(method, _options.MethodAttributes);
            if (!attributes.Any()) { continue; }

            var methodMetadata = new MethodExportModel(method.Name, attributes)
            {
                Parameters = BuildParameters(method)
            };

            metadata.Methods.Add(methodMetadata);
        }

        foreach (var property in type.Properties)
        {
            var attributes = BuildAttributes(property, _options.PropertyAttributes);
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
            var attributes = BuildAttributes(parameter, _options.ParameterAttributes);
            if (!attributes.Any()) { continue; }

            var parameterMetadata = new ParameterExportModel(parameter.Name, attributes);
            parameters.Add(parameterMetadata);
        }

        return parameters;
    }

    List<AttributeExportModel> BuildAttributes(ICustomAttributesModel model, List<Type> candidates)
    {
        var result = new List<AttributeExportModel>();
        foreach (var attributeType in candidates)
        {
            if (attributeType.AllowsMultiple() && model.TryGetAll(attributeType, out var attributes))
            {
                result.AddRange([.. attributes.Select(a => BuildAttribute(attributeType, a))]);
            }
            else if (model.TryGet(attributeType, out var attribute))
            {
                result.Add(BuildAttribute(attributeType, attribute));
            }
        }

        return result;
    }

    AttributeExportModel BuildAttribute<T>(Type type, T instance) where T : Attribute
    {
        var attributeMetadata = new AttributeExportModel(type.Name)
        {
            Values = instance is IMetadataSerializer serializer ? serializer.Properties.ToDictionary(p => p.Name, p => p.Value) : new()
        };

        return attributeMetadata;
    }
}