namespace Do.Test.DataAccess.Transaction;

public class CommitAsync : TestServiceSpec
{
    [Test]
    public void Commit_async_takes_nullable_parameters()
    {
        var singleton = GiveMe.The<Singleton>();
        var task = singleton.TestTransactionNullable(null);

        task.ShouldNotThrow();
    }

    [Test]
    public void Commit_async_update_occurs_when_entity_is_not_null()
    {
        var entity = GiveMe.AnEntity(@string: "string");
        var entitites = GiveMe.The<Entities>();
        var singleton = GiveMe.The<Singleton>();
        var task = singleton.TestTransactionNullable(entity);

        task.ShouldNotThrow();

        var actual = entitites.By().FirstOrDefault();

        actual.ShouldNotBeNull();
        actual.String.ShouldNotBe("string");
    }
}
