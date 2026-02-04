namespace Baked.Playground.CodingStyle.RichTransient;

public class RichTransientAsync
{
    public async Task<RichTransientAsync> With(Baked.Business.Id id)
    {
        await Task.Delay(0);

        Id = id;

        return this;
    }

    public Baked.Business.Id Id { get; private set; } = default!;
}