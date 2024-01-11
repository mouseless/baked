namespace Do.Domain.Model;

public record ConstructorModel(
    TypeModel Type,
    List<ParameterModel> Parameters
);