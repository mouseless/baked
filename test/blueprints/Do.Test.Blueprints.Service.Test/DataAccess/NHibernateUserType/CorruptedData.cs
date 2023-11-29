namespace Do.Test.DataAccess.NHibernateUserType;

public class CorruptedData : TestServiceSpec
{
    [Test]
    public void Special_characters_do_not_cause_corrupted_data_for_object_user_type_properties()
    {
        var entity = GiveMe.AnEntity(dynamic: new { test = "ğ€@test" });
        var entities = GiveMe.The<Entities>();

        Func<List<Entity>> task = () => entities.By(entity.String);

        task.ShouldNotThrow();
    }
}
