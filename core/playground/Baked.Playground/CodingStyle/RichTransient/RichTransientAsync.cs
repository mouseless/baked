namespace Baked.Playground.CodingStyle.RichTransient;

public class RichTransientAsync
{
    public Baked.Business.Id Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public async Task<RichTransientAsync> With(Baked.Business.Id id)
    {
        await Task.Delay(0);

        Id = id;
        Name = $"{id} name";

        return this;
    }
}