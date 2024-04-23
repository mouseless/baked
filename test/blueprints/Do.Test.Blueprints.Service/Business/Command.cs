namespace Do.Test.Business;

public class Command
{
    string _query = default!;

    public Command With(string query)
    {
        _query = query;

        return this;
    }

    public string Execute(string body) =>
        $"{_query}:{body}";

    public string ExecuteAlternative(string body) =>
        $"{_query}:{body}";
}