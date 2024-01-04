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
}
