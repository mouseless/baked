namespace DomainModelOverReflection.Models.Domain;

public record TypeModel(string Name, Type Type, List<MethodModel> Methods);
