namespace Baked.Test.CodingStyle.RichTransient;

public class RichTransientAsync
{
    public async Task<RichTransientAsync> With(string id)
    {
        await Task.Delay(0);

        Id = id;

        return this;
    }

    public string Id { get; private set; } = default!;
}