using Baked.Theme.Admin;

namespace Baked.Test.Theme;

public class DataTable
{
    public TableDataWithFooter<IEnumerable<Row>> GetWithFooter()
    {

        var items = Enumerable.Range(0, 20).Select(i => new Row($"Label-{i}", $"Text-{i}", 1, 2));

        return new(items)
        {
            Footer = new()
            {
                {  nameof(Row.Column2), items.Sum(i => i.Column2) },
                {  nameof(Row.Column3), items.Sum(i => i.Column3) }
            }
        };
    }
}

public record Row(string Label, string Column1, int Column2, int Column3);