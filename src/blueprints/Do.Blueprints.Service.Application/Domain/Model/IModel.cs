namespace Do.Domain.Model;

public interface IModel
{
    string Id { get; }
    AttributeCollection CustomAttributes { get; }

    bool Has<T>() where T : Attribute;
}
