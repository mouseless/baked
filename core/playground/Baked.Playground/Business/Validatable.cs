using Baked.Playground.CodingStyle.ResourceViaIdInitializer;
using System.ComponentModel.DataAnnotations;

namespace Baked.Playground.Business;

public record Validatable : IValidatableObject
{
    public ResourceWithData Left { get; private set; }
    public ResourceWithData Right { get; private set; }

    public Validatable(ResourceWithData left, ResourceWithData right)
    {
        Left = left;
        Right = right;

        Validate();
    }

    void Validate()
    {
        if (string.IsNullOrWhiteSpace(Left.Id)) { return; } // NOTE object not yet serialized, skip validation
        if (Left.Id == Right.Id) { return; }

        throw new Exceptions.ValidatableNotValidException();
    }

    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        Validate();

        return [];
    }
}