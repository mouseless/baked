using Do.ExceptionHandling;

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

        var result = entities.By().FirstOrDefault(e => e.Guid == entity.Guid);

        result.ShouldNotBeNull();
        result.String.ShouldBe("updated");
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

    [Test]
    public void Special_characters_do_not_cause_corrupted_data_for_object_properties()
    {
        var entity = GiveMe.AnEntity(dynamic: new { test = "ğ€@test" });
        var entities = GiveMe.The<Entities>();

        entity.ShouldBeInserted();

        Func<List<Entity>> task = () => entities.By(entity.String);

        task.ShouldNotThrow();
    }

    [Test]
    public void TestException_throws_handled_exception_when_the_handled_bool_flag_is_provided()
    {
        var singleton = GiveMe.The<Singleton>();

        Action task = () => singleton.TestException(handled: true);

        task.ShouldThrow<HandledException>();
    }

    [Test]
    public void TestException_throws_unhandled_exception_when_the_handled_bool_flag_is_provided()
    {
        var singleton = GiveMe.The<Singleton>();

        Action task = () => singleton.TestException(handled: false);

        task.ShouldThrow<InvalidOperationException>();
    }

    [Test]
    public async Task Transaction_committed_asynchronously_does_not_rollback_when_error_occurs()
    {
        var singleton = GiveMe.The<Singleton>();
        var entities = GiveMe.The<Entities>();

        var task = singleton.TestTransactionAction();

        await task.ShouldThrowAsync<Exception>();
        entities.By().ShouldNotBeEmpty();
    }

    [Test]
    public async Task Synchronous_update_is_not_committed_when_an_error_occurs_during_transaction()
    {
        var singleton = GiveMe.The<Singleton>();
        var entities = GiveMe.The<Entities>();

        var task = singleton.TestTransactionFunc();

        await task.ShouldThrowAsync<Exception>();

        var entity = entities.By().FirstOrDefault();

        entity.ShouldNotBeNull();
        entity.String.ShouldNotBeSameAs("rollback");
    }
}
