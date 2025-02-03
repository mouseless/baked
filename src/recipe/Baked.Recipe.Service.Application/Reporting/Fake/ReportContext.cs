using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace Baked.Reporting.Fake;

public class ReportContext(IFileProvider _fileProvider, ReportOptions _options)
    : IReportContext
{
    public async Task<object?[][]> Execute(string queryName, Dictionary<string, object> parameters)
    {
        var dataPath = $"/{Path.Join(_options.BasePath, $"{queryName}.json")}";
        if (!_fileProvider.Exists(dataPath)) { throw new QueryNotFoundException(queryName); }

        var dataString = await _fileProvider.ReadAsStringAsync(dataPath) ?? string.Empty;

        var fakes = JsonConvert.DeserializeObject<List<FakeData>>(dataString) ?? new();
        var match = fakes.FirstOrDefault(fake => fake.Matches(parameters));
        if (match is null) { return []; }

        return [.. match.Result.Select(row => row.Values.ToArray())];
    }
}