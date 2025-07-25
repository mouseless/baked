using Baked.Localization;

namespace Baked.Test.Theme;

public class DataTable(ILocalizer _localizer)
{
    public TableWithFooter GetTableDataWithFooter(int count)
    {
        var items = Enumerable.Range(0, count).Select(i => new TableRow($"{_localizer["This_is_a_label"]}-{i}", i % 5, $"This should be a very long Text-{i}", Guid.NewGuid(), Guid.NewGuid(), i * 10, ((double)i) / 7));

        return new(
            Items: items,
            FooterColumn1: items.Sum(i => i.Column4),
            FooterColumn2: items.Sum(i => i.Column5)
        );
    }
}