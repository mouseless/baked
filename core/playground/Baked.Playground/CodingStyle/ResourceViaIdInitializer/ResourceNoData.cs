namespace Baked.Playground.CodingStyle.ResourceViaIdInitializer;

public class ResourceNoData
{
    public ResourceNoData With(Baked.Business.Id id)
    {
        Id = id;

        return this;
    }

    internal Baked.Business.Id Id { get; private set; } = default!;

    public string Method(string text) =>
        text;
}