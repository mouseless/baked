namespace Do.Test;

public class Transaction : TestServiceSpec
{
    [Test]
    public void Created_entity_persists()
    {
        var entity = GiveMe.AnEntity();

        VerifyPersists(entity);
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
}
