namespace Do.Domain.Model;

public interface IModelWithMetadata : IModel
{
    ModelCollection<TypeModel> CustomAttributes { get; }

    bool HasAttribute<T>() where T : Attribute;
}
