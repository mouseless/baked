using Do.Domain.Model;

namespace Do.Domain.Configuration;

public interface IModelCollectionIndex
{
    ModelIndexKey IndexKey { get; }

    bool Selector(IModel model);
}
