namespace Baked.Playground.Theme;

public record TableWithFooter(IEnumerable<TableRow> Items, int FooterColumn1, double FooterColumn2, decimal FooterColumn3);