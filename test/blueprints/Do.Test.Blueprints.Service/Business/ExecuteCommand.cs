namespace Do.Test.Business;

public class ExecuteCommand : IExecuteCommand
{
    string _query = default!;

    public ExecuteCommand With(string query)
    {
        _query = query;

        return this;
    }

    public string Execute(string body) =>
        $"{_query}:{body}";
}