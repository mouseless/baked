namespace Baked.Playground.Lifetime;

public class TransientAsync
{
    internal Task<TransientAsync> With() =>
        Task.FromResult(this);
}