namespace DomainModelOverReflection.Models;

public record FieldModel(
    string Name,
    Type Type,
    bool IsPrivate
);
