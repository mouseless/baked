using Baked.Test.Orm;

namespace Baked.Test.Database;

public class TransactionalWork : TestServiceSpec
{
    [Test]
    public void Commit_async_takes_nullable_parameters()
    {
        var transactionSamples = GiveMe.The<TransactionSamples>();

        var task = transactionSamples.CommitNullable(null, GiveMe.AString());

        task.ShouldNotThrow();
    }

    [Test]
    public void Commit_async_update_occurs_when_entity_is_not_null()
    {
        var entity = GiveMe.AnEntity(@string: "before");
        var transactionSamples = GiveMe.The<TransactionSamples>();

        var task = transactionSamples.CommitNullable(entity, GiveMe.AString());

        task.ShouldNotThrow();
        entity.String.ShouldNotBe("before");
    }

    [Test]
    public async Task Transaction_is_skipped_during_tests()
    {
        var entity = GiveMe.AnEntity(@string: "old");

        await entity.Update(
            @string: "new",
            useTransaction: true
        );

        entity.String.ShouldBe("new");
    }

    [Test(Description = "Actual behaviour is not testable, this test is included only for documentation and to improve coverage")]
    public void Entity_created_by_a_transaction_committed_asynchronously_persists_when_an_error_occurs()
    {
        var transactionSamples = GiveMe.The<TransactionSamples>();
        var entities = GiveMe.The<Entities>();

        var task = transactionSamples.CommitAction();

        task.ShouldThrow<Exception>();
        entities.By().ShouldNotBeEmpty();
    }

    [Test(Description = "Actual behaviour is not testable, this test is included only for documentation and to improve coverage")]
    public void Only_the_updates_outside_of_transaction_are_rolled_back_when_an_error_occurs()
    {
        var transactionSamples = GiveMe.The<TransactionSamples>();
        var entities = GiveMe.The<Entities>();

        var task = transactionSamples.CommitFunc();

        task.ShouldThrow<Exception>();
        entities.By().ShouldNotBeEmpty();
    }
}