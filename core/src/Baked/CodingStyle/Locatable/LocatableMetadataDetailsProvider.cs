using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Baked.CodingStyle.Locatable;

public class LocatableMetadataDetailsProvider(Dictionary<Type, string> _idPropertyNames)
    : IValidationMetadataProvider
{
    public void CreateValidationMetadata(ValidationMetadataProviderContext context)
    {
        if (context.Key.ContainerType is null) { return; }
        if (!_idPropertyNames.TryGetValue(context.Key.ContainerType, out var idPropertyName)) { return; }
        if (context.Key.PropertyInfo?.Name == idPropertyName) { return; }

        context.ValidationMetadata.PropertyValidationFilter = new SkipPropertyFilter(idPropertyName);
    }

    class SkipPropertyFilter(string _idPropertyName)
        : IPropertyValidationFilter
    {
        public bool ShouldValidateEntry(ValidationEntry entry, ValidationEntry parentEntry) =>
            entry.Metadata.PropertyName == _idPropertyName;
    }
}