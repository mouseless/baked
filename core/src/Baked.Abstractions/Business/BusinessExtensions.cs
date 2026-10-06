using Baked.Business;

namespace Baked;

public static class BusinessExtensions
{
    extension(Task<Validate> task)
    {
        public async Task<Validate> That(Action validate) =>
            await task.That((IServiceProvider sp) => validate());

        public async Task<Validate> That(Action<IServiceProvider> validate) =>
            (await task).That(validate);

        public async Task<Validate> ThatAsync(Func<Task> validate) =>
            await task.ThatAsync(async (IServiceProvider sp) => await validate());

        public async Task<Validate> ThatAsync(Func<IServiceProvider, Task> validate) =>
            await (await task).ThatAsync(validate);
    }
}