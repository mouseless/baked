namespace Baked.Playground.CodingStyle.CommandPattern;

public class Command
{
    string _query = default!;

    public Command With(string query)
    {
        _query = query;

        return this;
    }

    public string Transient(string body) =>
        $"{_query}:{body}";
}