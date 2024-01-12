namespace Do.Domain.Model;

public record ConstructorModel(
    string Name,
    TypeModel Type,
    ModelCollection<ParameterModel> Parameters
) : IModel;