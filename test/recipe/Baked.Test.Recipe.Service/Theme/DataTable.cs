namespace Baked.Test.Theme;

public class DataTable
{
    public TableWithFooter GetTableDataWithFooter(int count)
    {
        var items = Enumerable.Range(0, count).Select(i => new TableRow($"Label-{i}", $"Text-{i}", 1, ((double)i) / 7));

        return new(
            Items: items,
            FooterColumn1: items.Sum(i => i.Column2),
            FooterColumn2: items.Sum(i => i.Column3)
        );
    }
}