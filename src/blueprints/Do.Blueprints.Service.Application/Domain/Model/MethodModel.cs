
namespace Do.Domain.Model;

public record MethodModel(
    TypeModel Target,
    string Name,
    OverloadModel[] Overloads,
    ModelCollection<TypeModel> CustomAttributes,
    bool IsConstructor = false
) : IModelWithMetadata
{
    public List<ParameterModel> GetParameters() =>
        Overloads.SelectMany(o => o.Parameters).ToList();

    public bool HasParameter(Func<ParameterModel, bool> constraint) =>
        GetParameters().Any(p => constraint(p));

    public bool HasAttribute<T>() where T : Attribute =>
        CustomAttributes.Contains(TypeModel.IdFrom(typeof(T)));

    public bool CanReturn(TypeModel type) =>
        Overloads.Any(o =>
            o.ReturnType == type ||
            (o.ReturnType?.IsAssignableTo<Task>() == true && o.ReturnType?.GenericTypeArguments.Any(a => a == type) == true)
        );

    string IModel.Id { get; } = Name;
}
