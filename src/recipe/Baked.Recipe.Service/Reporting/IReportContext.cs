namespace Baked.Reporting;

public interface IReportContext
{
    Task<object?[][]> Read(string queryName, Dictionary<string, object> parameters);
}