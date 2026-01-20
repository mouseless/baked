using Baked.Business;

namespace Baked.Playground.CodingStyle.RichTransient;

public class RichTransientAsync
{
    public async Task<RichTransientAsync> With(Id id)
    {
        await Task.Delay(0);

        Id = id;

        return this;
    }

    public Id Id { get; private set; } = default!;
}