namespace Baked.Test.CodingStyle.RichTransient;

public class RichTransientNoData
{
    public RichTransientNoData With(string id)
    {
        Id = id;

        return this;
    }

    internal string Id { get; set; } = default!;

    public object Method(object data) =>
        data;
}