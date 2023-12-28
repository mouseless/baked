namespace DomainModelOverReflection.Models.Domain;

public record TypeModel(
    string Name,
    Type Type,
    List<MethodModel> Constructors,
    List<FieldModel> Fields,
    List<MethodModel> Methods
);
