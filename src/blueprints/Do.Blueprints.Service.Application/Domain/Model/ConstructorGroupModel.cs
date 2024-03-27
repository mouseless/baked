namespace Do.Domain.Model;

public record ConstructorGroupModel(
    TypeModel ReflectedType
) : IModel
{
    public string Name { get; } = ".ctor";
    public List<ConstructorModel> Constructors { get; private set; } = default!;

    internal void Init(List<ConstructorModel> constructors)
    {
        Constructors = constructors;
    }

    string IModel.Id => Name;
}