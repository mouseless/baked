namespace Baked.Theme.Admin;

public class TableDataWithFooter<T>(T Items) where T : IEnumerable
{
    public T Items { get; init; } = Items;
    public Dictionary<string, object> Footer { get; init; } = [];
}