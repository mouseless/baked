using Microsoft.Extensions.FileProviders;
using NHibernate;

namespace Baked.Reporting.NativeSql;

public class ReportContext(IFileProvider _fileProvider, Func<IStatelessSession> _getStatelessSession, ReportOptions _options)
    : IReportContext
{
    public async Task<object?[][]> Execute(string queryName, Dictionary<string, object?> parameters)
    {
        var queryPath = $"/{Path.Join(_options.BasePath, $"{queryName}.sql")}";
        if (!_fileProvider.Exists(queryPath))
        {
            throw new QueryNotFoundException(queryName);
        }

        var queryString = await _fileProvider.ReadAsStringAsync(queryPath);
        var query = _getStatelessSession().CreateSQLQuery(queryString);
        foreach (var (name, value) in parameters)
        {
            if (value is null)
            {
                query.SetParameter(name, null, NHibernateUtil.String);
                continue;
            }

            query.SetParameter(name, value);
        }

        var result = await query.ListAsync();

        return [.. result.Cast<object[]>()];
    }
}