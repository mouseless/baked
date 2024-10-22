using Microsoft.Extensions.FileProviders;

namespace Baked.Reporting.NativeSql;

public class ReportContext(IFileProvider _fileProvider, Func<NHibernate.IStatelessSession> _getStatelessSession, ReportSettings _options)
    : IReportContext
{
    public async Task<object?[][]> Read(string queryName, Dictionary<string, object> parameters)
    {
        var queryPath = $"/{Path.Join(_options.BasePath, $"{queryName}.sql")}";
        if (!_fileProvider.GetFileInfo(queryPath).Exists)
        {
            throw new("no query");
        }

        var queryString = await _fileProvider.ReadAsStringAsync(queryPath);
        var query = _getStatelessSession().CreateSQLQuery(queryString);
        foreach (var (name, value) in parameters)
        {
            query.SetParameter(name, value);
        }

        var result = await query.ListAsync();

        return result.Cast<object[]>().ToArray();
    }
}