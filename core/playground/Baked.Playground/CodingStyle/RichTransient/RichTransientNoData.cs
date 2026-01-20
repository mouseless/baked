using Baked.Business;

namespace Baked.Playground.CodingStyle.RichTransient;

public class RichTransientNoData
{
    public RichTransientNoData With(Id id)
    {
        Id = id;

        return this;
    }

    internal Id Id { get; private set; } = default!;

    public string Method(string text) =>
        text;
}