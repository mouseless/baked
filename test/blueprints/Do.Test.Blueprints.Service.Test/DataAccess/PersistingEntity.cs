namespace Do.Test.DataAccess;

public class PersistingEntity : TestServiceSpec
{
    [Test]
    public void Created_entity_persists()
    {
        var newEntity = GiveMe.A<Func<Entity>>();

        var actual = newEntity().With(
            guid: Guid.NewGuid(),
            @string: string.Empty,
            stringData: string.Empty,
            int32: 0,
            uri: GiveMe.AUrl(),
            @dynamic: new { },
            status: Status.Disabled
        );

        actual.ShouldBeInserted();
    }

    [Test]
    public void Entity_does_not_persist_when_deleted()
    {
        var entity = GiveMe.AnEntity();

        entity.Delete();

        entity.ShouldBeDeleted();
    }

    [Test]
    public void Entity_update_commits_when_done_asynchronously_if_an_error_occurs()
    {
        var entity = GiveMe.AnEntity(@string: "test");
        var entities = GiveMe.The<Entities>();
        var task = entity.Update(
            guid: entity.Guid,
            @string: "updated",
            stringData: entity.StringData,
            int32: entity.Int32,
            uri: entity.Uri,
            dynamic: entity.Dynamic,
            status: entity.Status,
            useTransaction: true,
            throwError: true
        );

        task.ShouldThrow<Exception>();

        var result = entities.By().FirstOrDefault(e => e.Guid == entity.Guid);

        result.ShouldNotBeNull();
        result.String.ShouldBe("updated");
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