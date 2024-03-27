
namespace Do.Domain.Model;

public record MethodGroupModel(
    TypeModel ReflectedType,
    string Name
) : IModel
{
    public List<MethodModel> Methods { get; private set; } = default!;

    internal void Init(List<MethodModel> methods)
    {
        Methods = methods;
    }

    string IModel.Id => Name;
}
