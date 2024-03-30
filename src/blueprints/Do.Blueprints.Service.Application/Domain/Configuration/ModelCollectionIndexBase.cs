using Do.Domain.Model;

namespace Do.Domain.Configuration;

public abstract class ModelCollectionIndexBase : IModelCollectionIndex
{
    protected abstract ModelIndexKey IndexKey { get; }
    protected abstract bool Selector(IModel model);

    ModelIndexKey IModelCollectionIndex.IndexKey => IndexKey;
    bool IModelCollectionIndex.Selector(IModel model) => Selector(model);
}
