namespace Baked.Test.Theme;

public class TestPage
{
    string _query = default!;

    public TestPage With(string query)
    {
        _query = query;

        return this;
    }

    public string GetData(string panel) =>
        $"{_query} - {panel}";
}