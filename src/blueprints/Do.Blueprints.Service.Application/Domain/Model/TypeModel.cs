namespace Do.Domain.Model;

public record TypeModel(Type Type, string Name,
        List<MethodModel>? Constructors = default,
        List<MethodModel>? Methods = default
)
{
    public bool HasConstructor(Func<MethodModel, bool> predicate) =>
        Constructors is not null && Constructors.Any(predicate);

    public bool HasMethod(Func<MethodModel, bool> predicate) =>
        Methods is not null && Methods.Any(predicate);
}
