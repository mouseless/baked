namespace DomainModelOverReflection.Models.Domain;

public record FieldModel(
    string Name,
    Type Type,
    bool IsPrivate
);
