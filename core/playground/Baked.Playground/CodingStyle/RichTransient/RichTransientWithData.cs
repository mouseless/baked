using Baked.Business;

namespace Baked.Playground.CodingStyle.RichTransient;

public class RichTransientWithData(TimeProvider _timeProvider)
{
    public RichTransientWithData With(Id id)
    {
        Id = id;

        return this;
    }

    public Id Id { get; private set; } = default!;
    public string Time => _timeProvider.GetNow().ToString();
    internal string InternalProperty => $"{Guid.NewGuid()}";

    public string Method(string text) =>
        text;
}