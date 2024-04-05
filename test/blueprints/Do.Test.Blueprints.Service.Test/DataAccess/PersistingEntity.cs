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
            unique: $"{Guid.NewGuid()}",
            uri: GiveMe.AUrl(),
            @dynamic: new { },
            @enum: Status.Disabled
        );

        actual.ShouldBeInserted();
    }

    [Test]
    public void Entity_is_deleted_successfully()
    {
        var entity = GiveMe.AnEntity();

        entity.Delete();

        entity.ShouldBeDeleted();
    }

    [Test]
    public void Object_user_type_supports_special_characters_to_be_used_within_strings()
    {
        var entity = GiveMe.AnEntity(dynamic: new { test = "ğ€@test" });
        var entities = GiveMe.The<Entities>();

        Func<List<Entity>> task = () => entities.By(@string: entity.String);

        task.ShouldNotThrow();
    }
}
