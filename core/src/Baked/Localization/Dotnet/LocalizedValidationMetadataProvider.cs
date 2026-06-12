using Humanizer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace Baked.Localization.Dotnet;

public class LocalizedValidationMetadataProvider(IStringLocalizer _l)
        : IValidationMetadataProvider, IDisplayMetadataProvider
{
    public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
    {
        if (context.Key.Name == null) { return; }

        context.DisplayMetadata.DisplayName = () => _l[context.Key.Name.Pascalize()];
    }

    public void CreateValidationMetadata(ValidationMetadataProviderContext context)
    {
        foreach (var attribute in context.Attributes.OfType<ValidationAttribute>())
        {
            if (attribute.ErrorMessage != null) { continue; }

            attribute.ErrorMessage = attribute switch
            {
                RequiredAttribute => _l["The field '{0}' is required."],
                _ => _l["The field '{0}' has an invalid value."]
            };
        }
    }
}