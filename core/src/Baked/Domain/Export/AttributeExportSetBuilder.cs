using Baked.Business;
using Baked.Domain.Model;

using static Baked.Domain.Export.AttributeExport;

namespace Baked.Domain.Export;

public class AttributeExportSetBuilder(AttributeExport _options)
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
        var attributes = BuildAttributes(type, _options.TypeFilters);
        if (!attributes.Any()) { return default; }

        var typeMetadataModel = new TypeAttributeExportModel(((IModel)type).Id, type.Name);
        typeMetadataModel.Attributes.AddRange(attributes);
        typeMetadataModel.GroupName = _options.GetTypeGroupName(type);

        return type.TryGetMembers(out var members) ? BuildMembers(typeMetadataModel, members) : typeMetadataModel;
    }

    TypeAttributeExportModel BuildMembers(TypeAttributeExportModel metadata, TypeModelMembers type)
    {
        foreach (var method in type.Methods)
        {
            var attributes = BuildAttributes(method, _options.MethodFilters);
            if (!attributes.Any()) { continue; }

            var methodMetadata = new MethodAttributeExportModel(method.Name, attributes)
            {
                Parameters = BuildParameters(method)
            };

            metadata.Methods.Add(methodMetadata);
        }

        foreach (var property in type.Properties)
        {
            var attributes = BuildAttributes(property, _options.PropertyFilters);
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
            var attributes = BuildAttributes(parameter, _options.ParameterFilters);
            if (!attributes.Any()) { continue; }

            var parameterMetadata = new ParameterAttributeExportModel(parameter.Name, attributes);
            parameters.Add(parameterMetadata);
        }

        return parameters;
    }

    List<AttributeExportModel> BuildAttributes(ICustomAttributesModel model, List<AttributeFilter> filters)
    {
        var result = new List<AttributeExportModel>();
        foreach (var filter in filters)
        {
            if (filter.Type.AllowsMultiple() && model.TryGetAll(filter.Type, out var attributes))
            {
                result.AddRange([.. attributes.Select(a => BuildAttribute(filter.Type, a, filter))]);
            }
            else if (model.TryGet(filter.Type, out var attribute))
            {
                result.Add(BuildAttribute(filter.Type, attribute, filter));
            }
        }

        return result;
    }

    AttributeExportModel BuildAttribute<T>(Type type, T instance, AttributeFilter filter) where T : Attribute
    {
        var attributeMetadata = new AttributeExportModel(type.Name)
        {
            Values = instance is IMetadataSerializer serializer ?
                serializer.Properties.Where(p => filter.PropertyFilters.All(f => f(p))).ToDictionary(p => p.Name, p => p.Value) :
                new()
        };

        return attributeMetadata;
    }
}