namespace Do.Database.InMemory;

public class SkippedTransaction : ITransaction
{
    public Task<T> CommitAsync<T>(Func<T> action)
    {
        var result = action();

        return Task.FromResult(result);
    }
}
