namespace Do.Domain.Model;

public record PropertyModel(
    TypeModel Owner,
    string Name,
    TypeModel PropertyType,
    bool IsPublic,
    bool IsVirtual,
    AttributeCollection CustomAttributes
) : IModel
{
    public bool Has<T>() where T : Attribute =>
        CustomAttributes.ContainsKey(typeof(T));

    string IModel.Id { get; } = Name;
}
