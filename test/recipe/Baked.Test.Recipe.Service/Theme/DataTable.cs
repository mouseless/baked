namespace Baked.Test.Theme;

public class DataTable
{
    public TableWithFooter GetTableDataWithFooter(int count)
    {
        var items = Enumerable.Range(0, count).Select(i => new TableRow($"This is a Label-{i}", $"This should be a very long Text-{i}", Guid.NewGuid(), Guid.NewGuid(), i * 10000, ((double)i) / 7));

        return new(
            Items: items,
            FooterColumn1: items.Sum(i => i.Column4),
            FooterColumn2: items.Sum(i => i.Column5)
        );
    }
}