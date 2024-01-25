namespace Do.Test.Orm;

public class QueryingEntities : TestServiceSpec
{
    // when a single entity is queried by a unique property, the only result is returned
    // single by
    // -> entities.single by

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

    // multiple entities are queried by all types of properties
    // -> entities.by(string, bool, datetime...)

    // entities are queried by other entities
    // -> parent.getchildren

    // when multiple entities are queried, result have take, skip, order by options
    // -> entities.by_string
    // -> entities.by_string(take skip)
    // -> entities.by_string(asc desc)
    // -> entities.by_string(asc take skip)

    // when all entities are queried, every record is returned
    // -> parent.all

    // when all entities are queried, result have take, skip, order by options
    // -> parent.all(take skip)
    // -> parent.all(asc desc)
    // -> parent.all(asc take skip)

    [Test]
    [Ignore("not implemented")]
    public void Returns_the_range_of_entities_by_the_given_condition() => this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void Returns_a_single_entity_by_the_given_condition() => this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void Returns_all_entites() => this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void Returns_all_entities_with_given_order() => this.ShouldFail();
}
