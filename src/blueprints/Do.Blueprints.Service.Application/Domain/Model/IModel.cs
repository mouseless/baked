namespace Do.Domain.Model;

public interface IModel
{
    string Id { get; }
    AttributeCollection CustomAttributes { get; }

    bool HasAttribute<T>() where T : Attribute;
}
