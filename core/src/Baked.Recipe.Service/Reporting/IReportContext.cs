namespace Baked.Reporting;

public interface IReportContext
{
    Task<object?[][]> Execute(string queryName, Dictionary<string, object?> parameters);
}