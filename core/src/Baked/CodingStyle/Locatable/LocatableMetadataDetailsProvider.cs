using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Baked.CodingStyle.Locatable;

public class LocatableMetadataDetailsProvider(Dictionary<Type, string> _idPropertyNames)
    : IValidationMetadataProvider
{
    public void CreateValidationMetadata(ValidationMetadataProviderContext context)
    {
        if (context.Key.ContainerType is null) { return; }
        if (!_idPropertyNames.TryGetValue(context.Key.ContainerType, out var idPropertyName)) { return; }
        if (context.Key.PropertyInfo?.Name == idPropertyName) { return; }

        context.ValidationMetadata.IsRequired = false;
        context.ValidationMetadata.ValidatorMetadata.Clear();
    }
}