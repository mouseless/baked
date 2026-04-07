using Baked.Domain.Model;
using System.Reflection;

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
        var attributes = BuildAttributes(type, _options.TypeAttributes);
        if (_options.ExcludeTypesMissingAttributes && !attributes.Any()) { return default; }

        var typeMetadataModel = new TypeMetadataModel(((IModel)type).Id, type.Name);
        typeMetadataModel.Attributes.AddRange(attributes);

        return type.TryGetMembers(out var members) ? BuildMembers(typeMetadataModel, members) : typeMetadataModel;
    }

    TypeMetadataModel BuildMembers(TypeMetadataModel metadata, TypeModelMembers type)
    {
        foreach (var method in type.Methods)
        {
            var attributes = BuildAttributes(method, _options.MethodAttributes);
            if (!attributes.Any()) { continue; }

            var methodMetadata = new MethodMetadataModel(method.Name, attributes, BuildParameters(method));
            metadata.Methods.Add(methodMetadata);
        }

        foreach (var property in type.Properties)
        {
            var attributes = BuildAttributes(property, _options.PropertyAttributes);
            if (!attributes.Any()) { continue; }

            var propertyMetadata = new PropertyMetadataModel(property.Name, attributes);
            metadata.Properties.Add(propertyMetadata);
        }

        return metadata;
    }

    List<ParameterMetadataModel> BuildParameters(MethodModel method)
    {
        var parameters = new List<ParameterMetadataModel>();
        foreach (var parameter in method.DefaultOverload.Parameters)
        {
            var attributes = BuildAttributes(parameter, _options.ParameterAttributes);
            if (!attributes.Any()) { continue; }

            var parameterMetadata = new ParameterMetadataModel(parameter.Name, attributes);
            parameters.Add(parameterMetadata);
        }

        return parameters;
    }

    List<AttributeMetadataModel> BuildAttributes(ICustomAttributesModel model, List<Type> candidates)
    {
        var result = new List<AttributeMetadataModel>();
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

    // TODO temporari implementation
    // values will be get directly from
    // attributes
    AttributeMetadataModel BuildAttribute<T>(Type type, T instance) where T : Attribute
    {
        var attributeMetadata = new AttributeMetadataModel(type.Name)
        {
            Values = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead)
                .ToDictionary(
                    p => p.Name,
                    p => p.GetValue(instance)
                )
        };

        return attributeMetadata;
    }
}