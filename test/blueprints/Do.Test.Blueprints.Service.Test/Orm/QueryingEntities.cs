namespace Do.Test.Orm;

public class QueryingEntities : TestServiceSpec
{
    // multiple entities are queried by all types of properties
    // -> entities.by(string, bool, datetime...)

    // when all entities are queried, result have take, skip, order by options
    // -> parent.all(take skip)
    // -> parent.all(asc desc)
    // -> parent.all(asc take skip)

    // when a single entity is queried by a unique property, the only result is returned
    // single by
    // -> entities.single by
    [Test]
    public void When_a_single_entity_is_queried_by_a_unique_property__the_only_result_is_returned()
    {
        var expected = GiveMe.AnEntity(@string: "a");
        GiveMe.AnEntity(@string: "b");
        var testing = GiveMe.The<Entities>();

        var actual = testing.SingleByString("a");

        actual.ShouldNotBeNull();
        actual.ShouldBe(expected);
    }

    // when a single entity is queried by a property, first result is returned
    // first by
    // ---
    //  giveme ab
    //  giveme aa
    //  giveme ac
    //  entities.FirstByString("a").ShouldBe(ab)
    //  entities.FirstByString("a", asc: true).ShouldBe(aa)
    //  entities.FirstByString("a", desc: true).ShouldBe(ac)
    // ---
    [Test]
    public void When_a_single_entity_is_queried_by_a_property__first_result_is_returned()
    {
        var expected = GiveMe.AnEntity(@string: "ab");
        var expected2 = GiveMe.AnEntity(@string: "aa");
        var expected3 = GiveMe.AnEntity(@string: "ac");
        var testing = GiveMe.The<Entities>();

        var actual = testing.FirstByStringStartsWith("a");
        actual.ShouldNotBeNull();
        actual.ShouldBe(expected);

        actual = testing.FirstByStringStartsWith("a", asc: true);
        actual.ShouldNotBeNull();
        actual.ShouldBe(expected2);

        actual = testing.FirstByStringStartsWith("a", desc: true);
        actual.ShouldNotBeNull();
        actual.ShouldBe(expected3);
    }

    // entities are queried by other entities
    // -> parent.getchildren
    [Test]
    public void Entities_are_queried_by_other_entities()
    {
        var parent = GiveMe.AParent();
        parent.AddChild();
        var parent2 = GiveMe.AParent();
        parent2.AddChild();

        var testing = GiveMe.The<Children>();

        var actual = testing.ByParent(parent);
        actual.ShouldNotBeNull();
        actual.Count.ShouldBe(1);
    }

    // when multiple entities are queried, result have take, skip, order by options
    // -> entities.by_string
    // -> entities.by_string(take skip)
    // -> entities.by_string(asc desc)
    // -> entities.by_string(asc take skip)
    [Test]
    public void When_multiple_entities_are_queried__result_have_take_skip_order_by_options()
    {
        GiveMe.AnEntity(@string: "a", int32: 0);
        GiveMe.AnEntity(@string: "a", int32: 1);
        GiveMe.AnEntity(@string: "a", int32: 2);
        var testing = GiveMe.The<Entities>();

        var actual = testing.By(@string: "a");
        actual.ShouldNotBeNull();
        actual.Count.ShouldBe(3);

        actual = testing.By(@string: "a", skip: 2, take: 1);
        actual[0].Int32.ShouldBe(2);
    }

    // when all entities are queried, every record is returned
    // -> parent.all
    [Test]
    public void When_all_entities_are_queried__every_record_is_returned()
    {
        GiveMe.AParent();
        GiveMe.AParent();

        var testing = GiveMe.The<Parents>();

        var actual = testing.All();
        actual.ShouldNotBeNull();
        actual.Count.ShouldBe(2);
    }
}
