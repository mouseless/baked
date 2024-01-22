using System.Collections.ObjectModel;

namespace Do.Domain.Model;

public class KeyedModelCollection<TItem> : KeyedCollection<string, TItem>
        where TItem : IModel
{
    protected override string GetKeyForItem(TItem item) => item.Id;

    public new bool Contains(TItem item) => Contains(item.Id);
}
