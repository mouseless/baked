using Baked.Reporting;

namespace Baked.Test.Reporting;

public class ReportSamples(IReportContext _context)
{
    public async Task<List<EntityReportData>> GetEntity(string name) =>
        (await _context.Execute("entity", new() { { nameof(name), $"{name}%" } }))
          .Select(row => new EntityReportData((string?)row[1] ?? string.Empty, Convert.ToInt32(row[0])))
          .ToList();
}