namespace Do.Domain.Model;

public interface IModelWithMetadata : IModel
{
    ModelCollection<TypeModel> CustomAttributes { get; }

    bool Has<T>() where T : Attribute;
}
