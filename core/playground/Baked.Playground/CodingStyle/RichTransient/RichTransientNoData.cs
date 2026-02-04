namespace Baked.Playground.CodingStyle.RichTransient;

public class RichTransientNoData
{
    public RichTransientNoData With(Baked.Business.Id id)
    {
        Id = id;

        return this;
    }

    internal Baked.Business.Id Id { get; private set; } = default!;

    public string Method(string text) =>
        text;
}