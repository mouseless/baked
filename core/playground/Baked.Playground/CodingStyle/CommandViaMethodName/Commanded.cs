namespace Baked.Playground.CodingStyle.CommandViaMethodName;

public class Commanded
{
    string _query = default!;

    public Commanded With(string query)
    {
        _query = query;

        return this;
    }

    public string Method(string body) =>
        $"{_query}:{body}";
}