namespace Baked.Test.CodingStyle.RichTransient;

public class RichTransientWithData
{
    public RichTransientWithData With(string id)
    {
        Id = id;

        return this;
    }

    public string Id { get; set; } = default!;

    public string Method(string text) =>
        text;
}