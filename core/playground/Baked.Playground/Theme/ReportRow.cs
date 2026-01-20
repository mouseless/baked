using Baked.Business;

namespace Baked.Playground.Theme;

public record ReportRow(
    Id Id,
    string Label,
    string Column1,
    string Column2,
    string? Column3
);