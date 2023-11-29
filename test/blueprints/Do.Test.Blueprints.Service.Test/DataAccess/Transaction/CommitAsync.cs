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
        var singleton = GiveMe.The<Singleton>();

        var task = singleton.TestTransactionNullable(entity);

        task.ShouldNotThrow();
        entity.String.ShouldNotBe("string");
    }

    [Test]
    public void Entity_created_by_a_transaction_committed_asynchronously_persists_when_an_error_occurs()
    {
        var singleton = GiveMe.The<Singleton>();
        var entities = GiveMe.The<Entities>();

        var task = singleton.TestTransactionAction();

        task.ShouldThrow<Exception>();
        entities.By().ShouldNotBeEmpty();
    }
}
