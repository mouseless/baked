namespace Baked.Test.Theme;

public class DataTableSamples
{
    public object GetTableDataWithFooter(int count)
    {
        var items = Enumerable.Range(0, count).Select(i => new Row($"Label-{i}", $"Text-{i}", 1, 2));

        return new
        {
            Items = items,
            Column2 = items.Sum(i => i.Column2),
            Column3 = items.Sum(i => i.Column3),
        };
    }
}

public record Row(string Label, string Column1, int Column2, int Column3);