using Baked.Playground.CodingStyle.ResourceViaIdInitializer;
using System.ComponentModel.DataAnnotations;

namespace Baked.Playground.Business;

public record ValidatableWithResults : IValidatableObject
{
    public ResourceWithData Left { get; private set; }
    public ResourceWithData Right { get; private set; }

    public ValidatableWithResults(ResourceWithData left, ResourceWithData right)
    {
        Left = left;
        Right = right;
    }

    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Left.Id)) { yield break; } // NOTE object not yet serialized, skip validation
        if (Left.Id == Right.Id) { yield break; }

        yield return new("Left and right should be the same", [nameof(Left), nameof(Right)]);
    }
}