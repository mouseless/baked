
namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    OverloadModel[] Overloads,
    bool IsConstructor = false
) : IModelWithMetadata
{
    public ModelCollection<TypeModel> CustomAttributes { get; } = [];

    public bool Has<T>() where T : Attribute =>
        CustomAttributes.Contains(TypeModel.IdFrom(typeof(T)));

    public bool CanReturn(TypeModel type) =>
        Overloads.Any(o =>
            o.ReturnType == type ||
            (o.ReturnType?.IsAssignableTo<Task>() == true && o.ReturnType?.GenericTypeArguments.Any(a => a == type) == true)
        );

    string IModel.Id { get; } = Name;
}
