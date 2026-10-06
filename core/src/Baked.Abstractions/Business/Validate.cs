namespace Baked.Business;

public class Validate(IServiceProvider _sp)
{
    public Validate That(Action validate) =>
        That(_ => validate());

    public Validate That(Action<IServiceProvider> validate)
    {
        validate(_sp);

        return this;
    }

    public async Task<Validate> ThatAsync(Func<Task> validate) =>
        await ThatAsync(async _ => await validate());

    public async Task<Validate> ThatAsync(Func<IServiceProvider, Task> validate)
    {
        await validate(_sp);

        return this;
    }
}