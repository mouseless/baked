namespace Do.Test.Orm;

public class QueryingEntities : TestServiceSpec
{
    [Test]
    public void When_a_single_entity_is_queried_by_a_unique_property__the_only_result_is_returned()
    {
        var expected = GiveMe.AnEntity(unique: GiveMe.AGuid("eb8dd0a1"));
        GiveMe.AnEntity(unique: GiveMe.AGuid("ac9dd0a2"));
        var testing = GiveMe.The<Entities>();

        testing.SingleByUnique(GiveMe.AGuid("eb8dd0a1")).ShouldBe(expected);
    }

    [Test]
    public void When_a_single_entity_is_queried_by_a_property__first_result_is_returned()
    {
        var expected = GiveMe.AnEntity(@string: "ab");
        var expected2 = GiveMe.AnEntity(@string: "aa");
        var expected3 = GiveMe.AnEntity(@string: "ac");
        var testing = GiveMe.The<Entities>();

        testing.FirstByString(startsWith: "a").ShouldBe(expected);
        testing.FirstByString(startsWith: "a", asc: true).ShouldBe(expected2);
        testing.FirstByString(startsWith: "a", desc: true).ShouldBe(expected3);
    }

    [Test]
    public void Entities_are_queried_by_other_entities()
    {
        var parent = GiveMe.AParent(withChild: true);

        parent.GetChildren().Count.ShouldBe(1);
    }

    [Test]
    public void When_multiple_entities_are_queried__result_have_take_skip_order_by_options()
    {
        GiveMe.AParent(name: "ab");
        GiveMe.AParent(name: "aa");
        GiveMe.AParent(name: "ac");

        var testing = GiveMe.The<Parents>();

        testing.ByName(contains: "a").Count.ShouldBe(3);
        testing.ByName(contains: "a", take: 1).Count.ShouldBe(1);
        testing.ByName(contains: "a", take: 1, skip: 1).First().Name.ShouldBe("aa");
        testing.ByName(contains: "a", asc: true).First().Name.ShouldBe("aa");
        testing.ByName(contains: "a", asc: true, take: 1).Count.ShouldBe(1);
        testing.ByName(contains: "a", asc: true, take: 1, skip: 1).First().Name.ShouldBe("ab");
        testing.ByName(contains: "a", desc: true).First().Name.ShouldBe("ac");
    }

    [Test]
    public void When_all_entities_are_queried__every_record_is_returned()
    {
        GiveMe.AParent();
        GiveMe.AParent();
        var testing = GiveMe.The<Parents>();

        testing.All().Count.ShouldBe(2);
    }

    [Test]
    public void When_all_entities_are_queried__results_have_take_skip_orderby_options()
    {
        GiveMe.AParent(name: "b");
        GiveMe.AParent(name: "a");
        GiveMe.AParent(name: "c");

        var testing = GiveMe.The<Parents>();

        testing.All(skip: 1, take: 1).Count.ShouldBe(1);
        testing.All(skip: 1, take: 1).First().Name.ShouldBe("a");

        testing.All(asc: true).First().Name.ShouldBe("a");
        testing.All(desc: true).First().Name.ShouldBe("c");
    }
}
