namespace Baked.Test.Theme;

public record TableWithFooter(IEnumerable<TableRow> Items, int FooterColumn1, int FooterColumn2);