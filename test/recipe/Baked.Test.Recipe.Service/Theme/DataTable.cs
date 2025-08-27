using Microsoft.Extensions.Localization;

namespace Baked.Test.Theme;

public class DataTable(IStringLocalizer _l)
{
    public TableWithFooter GetTableDataWithFooter(RowCount count)
    {
        var items = Enumerable.Range(0, (int)count)
            .Select(i => new TableRow(
                $"{_l["This is a label"]}-{i}",
                i % 5,
                $"{_l["This should be a very long text"]}-{i}",
                Guid.NewGuid(),
                Guid.NewGuid(),
                i * 10,
                ((double)i) / 7
            ));

        return new(
            Items: items,
            FooterColumn1: items.Sum(i => i.Column4),
            FooterColumn2: items.Sum(i => i.Column5)
        );
    }
}