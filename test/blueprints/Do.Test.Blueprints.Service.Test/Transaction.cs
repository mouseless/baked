namespace Do.Test;

public class Transaction : TestServiceSpec
{
    [Test]
    public void Created_entity_persists()
    {
        var entity = GiveMe.AnEntity();

        entity.ShouldBeInserted();
    }

    [Test]
    public async Task Entity_update_does_not_rollback_when_done_asynchronously_if_an_error_occurs()
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

        await task.ShouldThrowAsync<Exception>();

        var result = entities.By("updated").FirstOrDefault(e => e.Guid == entity.Guid);

        result.ShouldNotBeNull();
        result.String.ShouldBe(entity.String);
    }

    [Test]
    public void Entity_does_not_persist_when_deleted()
    {
        var entity = GiveMe.AnEntity();

        entity.ShouldBeInserted();

        entity.Delete();

        entity.ShouldBeDeleted();
    }

    [Test]
    public void A_single_instance_of_singleton_is_shared_across_application()
    {
        var singleton1 = GiveMe.The<Singleton>();
        var singleton2 = GiveMe.The<Singleton>();

        singleton1.ShouldBe(singleton2);
    }

    [Test]
    public void New_instance_of_transient_is_created_at_each_request()
    {
        var entity1 = GiveMe.An<Entity>();
        var entity2 = GiveMe.An<Entity>();

        entity1.ShouldNotBe(entity2);
    }
}
