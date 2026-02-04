namespace Baked.Playground.CodingStyle.RichTransient;

public class RichTransientWithData(TimeProvider _timeProvider)
{
    public RichTransientWithData With(Baked.Business.Id id)
    {
        Id = id;

        return this;
    }

    public Baked.Business.Id Id { get; private set; } = default!;
    public string Time => _timeProvider.GetNow().ToString();
    internal string InternalProperty => $"{Guid.NewGuid()}";

    public string Method(string text) =>
        text;
}