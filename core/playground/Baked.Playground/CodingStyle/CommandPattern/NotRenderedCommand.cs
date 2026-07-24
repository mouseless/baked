namespace Baked.Playground.CodingStyle.CommandPattern;

public class NotRenderedCommand
{
    string _query = default!;

    public NotRenderedCommand With(Func<string> queryFunc)
    {
        _query = queryFunc();

        return this;
    }

    public string Execute(string body) =>
        $"{_query}:{body}";
}