using Baked.Reporting;

namespace Baked.Playground.Reporting;

public class ReportSamples(IReportContext _context)
{
    public async Task<List<EntityReportData>> GetEntity(string @string) =>
        [.. (await _context.Execute("entity",
            new()
            {
                { nameof(@string), $"{@string}%" }
            }
        ))
        .Select(row =>
            new EntityReportData(
                Convert.ToInt32(row[0]),
                (string?)row[1] ?? string.Empty
            )
        )];

    public async Task GetNonExisting()
    {
        try
        {
            await _context.Execute("non-existing", []);
        }
        catch (QueryNotFoundException ex)
        {
            if (!ex.Message.Contains("non-existing")) { throw; }

            return;
        }
        catch
        {
            throw;
        }
    }
}