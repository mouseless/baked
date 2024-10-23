namespace Baked.Reporting;

public class QueryNotFoundException(string queryName)
    : Exception($"No query file with '{queryName}' was found");