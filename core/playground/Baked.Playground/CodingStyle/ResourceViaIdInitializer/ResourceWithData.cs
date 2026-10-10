namespace Baked.Playground.CodingStyle.ResourceViaIdInitializer;

public class ResourceWithData(TimeProvider _timeProvider)
{
    public Baked.Business.Id Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Time => _timeProvider.GetNow().ToString();
    internal string InternalProperty => $"{Guid.NewGuid()}";

    public ResourceWithData With(Baked.Business.Id id)
    {
        Id = id;
        Name = $"{id} name";

        return this;
    }

    public string Method(string text) =>
        text;
}