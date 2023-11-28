namespace Do.Test.DataAccess.Transaction;

public class CommitAsync : TestServiceSpec
{
    [Test]
    public async Task Commit_async_takes_nullable_parameters()
    {
        var singleton = GiveMe.The<Singleton>();
        var task = singleton.TestTransactionNullable(null);

        await task.ShouldNotThrowAsync();
    }
}
