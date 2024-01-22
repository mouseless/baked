using System.Collections.ObjectModel;

namespace Do.Domain.Model;

public class KeyedModelCollection<TItem> : KeyedCollection<string, TItem>
        where TItem : IModel
{
    protected override string GetKeyForItem(TItem item) => item.Id;

    public TItem? GetOrDefault(string? key)
    {
        if (string.IsNullOrEmpty(key)) { return default; }

        if (TryGetValue(key, out var item))
        {
            return item;
        }

        return default;
    }
}
