namespace Baked.Playground.CodingStyle.ResourceViaIdInitializer;

public class ResourceAsync
{
    public Baked.Business.Id Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public async Task<ResourceAsync> With(Baked.Business.Id id)
    {
        await Task.Delay(0);

        Id = id;
        Name = $"{id} name";

        return this;
    }
}