namespace Baked.Playground.CodingStyle.RichTransient;

public class RichTransientNoData
{
    public RichTransientNoData With(string id)
    {
        Id = id;

        return this;
    }

    internal string Id { get; private set; } = default!;

    public string Method(string text) =>
        text;
}