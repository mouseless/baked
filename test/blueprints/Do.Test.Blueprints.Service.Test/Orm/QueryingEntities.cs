using Do.Orm;

namespace Do.Test.Orm;

public class QueryingEntities : TestServiceSpec
{
    [Test]
    public void Returns_a_single_entity_by_the_given_condition()
    {
        var guid = GiveMe.AGuid("b4b6bd2b-b8f3-414e-bd84-1e044cbe77dc");
        var entity = GiveMe.AnEntity(guid: guid);
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.SingleBy(e => e.Guid == guid);

        actual.ShouldBeEquivalentTo(entity);
    }

    [Test]
    public void Returns_the_first_single_entity_by_the_given_condition()
    {
        var guid = GiveMe.AGuid("b4b6bd2b-b8f3-414e-bd84-1e044cbe77dc");
        var entity = GiveMe.AnEntity(guid: guid);
        GiveMe.AnEntity();
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.FirstBy(e => e.Guid == guid);

        actual.ShouldBeEquivalentTo(entity);
    }

    [Test]
    public void Returns_the_first_single_entity_by_the_order()
    {
        var guid = GiveMe.AGuid("b4b6bd2b-b8f3-414e-bd84-1e044cbe77dc");
        GiveMe.AnEntity(int32: 2);
        var entity = GiveMe.AnEntity(guid: guid, int32: 1);
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.FirstBy(e => true, orderBy: e => e.Int32);

        actual.ShouldBeEquivalentTo(entity);
    }

    [Test]
    public void Returns_the_first_single_entity_by_the_descending_order()
    {
        var guid = GiveMe.AGuid("b4b6bd2b-b8f3-414e-bd84-1e044cbe77dc");
        GiveMe.AnEntity(int32: 1);
        var entity = GiveMe.AnEntity(guid: guid, int32: 2);
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.FirstBy(e => true, orderByDescending: e => e.Int32);

        actual.ShouldBeEquivalentTo(entity);
    }

    [Test]
    public void Returns_the_range_of_entities_by_the_given_condition_and_order()
    {
        var firstEntity = GiveMe.AnEntity(int32: 2);
        var secondEntity = GiveMe.AnEntity(int32: 1);
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.By(e => e.Int32 > 0, orderBy: e => e.Int32);

        actual.ShouldBe([secondEntity, firstEntity]);
    }

    [Test]
    public void Returns_the_range_of_entities_by_the_given_condition_and_descending_order()
    {
        var firstEntity = GiveMe.AnEntity(int32: 2);
        var secondEntity = GiveMe.AnEntity(int32: 1);
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.By(e => true, orderByDescending: e => e.Int32);

        actual.ShouldBe([firstEntity, secondEntity]);
    }

    [Test]
    public void Returns_the_range_of_entities_skips_by_given_amount()
    {
        var firstEntity = GiveMe.AnEntity(int32: 2);
        var secondEntity = GiveMe.AnEntity(int32: 1);
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.By(e => true, orderBy: e => e.Int32, skip: 1);

        actual.ShouldBe([firstEntity]);
    }

    [Test]
    public void Returns_the_range_of_entities_takes_by_given_amount()
    {
        var firstEntity = GiveMe.AnEntity(int32: 2);
        var secondEntity = GiveMe.AnEntity(int32: 1);
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.By(e => true, orderBy: e => e.Int32, take: 1);

        actual.ShouldBe([secondEntity]);
    }

    [Test]
    public void Returns_all_entites()
    {
        var firstEntity = GiveMe.AnEntity(int32: 2);
        var secondEntity = GiveMe.AnEntity(int32: 1);
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.All();

        actual.ShouldBe([firstEntity, secondEntity]);
    }

    [Test]
    public void Returns_all_entites_with_given_order()
    {
        var firstEntity = GiveMe.AnEntity(int32: 2);
        var secondEntity = GiveMe.AnEntity(int32: 1);
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.All(orderBy: e => e.Int32);

        actual.ShouldBe([secondEntity, firstEntity]);
    }

    [Test]
    public void Returns_all_entites_with_given_descending_order()
    {
        var firstEntity = GiveMe.AnEntity(int32: 2);
        var secondEntity = GiveMe.AnEntity(int32: 1);
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.All(orderByDescending: e => e.Int32);

        actual.ShouldBe([firstEntity, secondEntity]);
    }

    [Test]
    public void Returns_all_entites_skips_given_amount()
    {
        var firstEntity = GiveMe.AnEntity(int32: 2);
        var secondEntity = GiveMe.AnEntity(int32: 1);
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.All(orderBy: e => e.Int32, skip: 1);

        actual.ShouldBe([firstEntity]);
    }

    [Test]
    public void Returns_all_entites_takes_given_amount()
    {
        var firstEntity = GiveMe.AnEntity(int32: 2);
        var secondEntity = GiveMe.AnEntity(int32: 1);
        var entitites = GiveMe.The<IQueryContext<Entity>>();

        var actual = entitites.All(orderBy: e => e.Int32, take: 1);

        actual.ShouldBe([secondEntity]);
    }
}
