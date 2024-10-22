using Baked.Reporting;

namespace Baked.Test.Reporting;

public class ReportSamples(IReportContext _context)
{
    public async Task<EntityReportData> GetEntity(string name)
    {
        var result = await _context.Read("entity", new() { { nameof(name), $"{name}%" } });

        return new(name, Convert.ToInt32(result[0][0]));
    }
}