namespace Do.Test.DataAccess;

public class MappingProperties : TestServiceSpec
{
    [Test]
    public async Task Uniq()
    {
        var entity = GiveMe.An<Entity>().With(uniq: GiveMe.AGuid("eb8dd0a1").ToString());
        entity.Uniq.ShouldBe(GiveMe.AGuid("eb8dd0a1").ToString());

        await entity.Update(uniq: GiveMe.AGuid("ab8dd0a1").ToString());
        entity.Uniq.ShouldBe(GiveMe.AGuid("ab8dd0a1").ToString());

        var actual = GiveMe.The<Entities>().By(uniq: GiveMe.AGuid("ab8dd0a1").ToString()).FirstOrDefault();
        actual.ShouldBe(entity);
    }

    [Test]
    public void Uniq_must_be_unique()
    {
        var entity = GiveMe.An<Entity>().With(uniq: GiveMe.AGuid("eb8dd0a1").ToString());
        entity.Uniq.ShouldBe(GiveMe.AGuid("eb8dd0a1").ToString());

        Func<Entity> task = () => GiveMe.An<Entity>().With(uniq: GiveMe.AGuid("eb8dd0a1").ToString());

        task.ShouldThrow<MustBeUniqException>();
    }

    [Test]
    public async Task Guid()
    {
        var entity = GiveMe.An<Entity>().With(guid: GiveMe.AGuid("eb8dd0a1"));
        entity.Guid.ShouldBe(GiveMe.AGuid("eb8dd0a1"));

        await entity.Update(guid: GiveMe.AGuid("ab8dd0a1"));
        entity.Guid.ShouldBe(GiveMe.AGuid("ab8dd0a1"));

        var actual = GiveMe.The<Entities>().By(guid: GiveMe.AGuid("ab8dd0a1")).FirstOrDefault();
        actual.ShouldBe(entity);
    }

    [Test]
    public async Task String()
    {
        var entity = GiveMe.An<Entity>().With(@string: "string");
        entity.String.ShouldBe("string");

        await entity.Update(@string: "test");
        entity.String.ShouldBe("test");

        var actual = GiveMe.The<Entities>().By(@string: "test").FirstOrDefault();
        actual.ShouldBe(entity);
    }

    [Test]
    public async Task String_data()
    {
        var entity = GiveMe.An<Entity>().With(stringData: "string");
        entity.StringData.ShouldBe("string");

        await entity.Update(stringData: "test");
        entity.StringData.ShouldBe("test");

        var actual = GiveMe.The<Entities>().By(stringData: "test").FirstOrDefault();
        actual.ShouldBe(entity);
    }

    [Test]
    public async Task Object()
    {
        var entity = GiveMe.An<Entity>().With(dynamic: new { dynamic = "dynamic" });
        entity.Dynamic.ShouldBe(new { dynamic = "dynamic" });

        await entity.Update(dynamic: new { update = "update" });
        entity.Dynamic.ShouldBe(new { update = "update" });
    }

    [Test]
    public async Task Int()
    {
        var entity = GiveMe.An<Entity>().With(int32: 5);
        entity.Int32.ShouldBe(5);

        await entity.Update(int32: 1);
        entity.Int32.ShouldBe(1);

        var actual = GiveMe.The<Entities>().By(int32: 1).FirstOrDefault();
        actual.ShouldBe(entity);
    }

    [Test]
    public async Task Enum()
    {
        var entity = GiveMe.An<Entity>().With(@enum: Status.Enabled);
        entity.Enum.ShouldBe(Status.Enabled);

        await entity.Update(@enum: Status.Disabled);
        entity.Enum.ShouldBe(Status.Disabled);

        var actual = GiveMe.The<Entities>().By(status: Status.Disabled).FirstOrDefault();
        actual.ShouldBe(entity);
    }

    [Test]
    public async Task DateTime()
    {
        var entity = GiveMe.An<Entity>().With(dateTime: GiveMe.ADateTime(year: 2023, month: 11, day: 29));
        entity.DateTime.ShouldBe(GiveMe.ADateTime(year: 2023, month: 11, day: 29));

        await entity.Update(dateTime: GiveMe.ADateTime(year: 2023, month: 11, day: 30));
        entity.DateTime.ShouldBe(GiveMe.ADateTime(year: 2023, month: 11, day: 30));

        var actual = GiveMe.The<Entities>().By(dateTime: GiveMe.ADateTime(year: 2023, month: 11, day: 30)).FirstOrDefault();
        actual.ShouldBe(entity);
    }
}
