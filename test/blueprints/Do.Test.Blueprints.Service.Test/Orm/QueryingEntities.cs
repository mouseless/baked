namespace Do.Test.Orm;

public class QueryingEntities : TestServiceSpec
{
    [Test]
    public void Returns_the_range_of_entities_by_the_given_condition()
    {
        var parent = GiveMe.AParentEntitiy();
        var child1 = GiveMe.AChildEntity(parent, GiveMe.ADateTime(2024, 1, 23, 14, 21));
        var child2 = GiveMe.AChildEntity(parent, GiveMe.ADateTime(2024, 1, 23, 14, 22));

        parent.GetChildren().ShouldBe([child1, child2]);
        parent.GetChildren(reverse: true).ShouldBe([child2, child1]);
    }

    [Test]
    public void Returns_a_single_entity_by_the_given_condition()
    {
        var parent = GiveMe.AParentEntitiy();
        var child1 = GiveMe.AChildEntity(parent, GiveMe.ADateTime(2024, 1, 23, 14, 21));
        var child2 = GiveMe.AChildEntity(parent, GiveMe.ADateTime(2024, 1, 23, 14, 22));
        var childEntities = GiveMe.The<Children>();

        childEntities.FirstByParent(parent.Id).ShouldBe(child1);
        childEntities.FirstByParent(parent.Id, reverse: true)?.ShouldBe(child2);
    }

    [Test]
    public void Returns_all_entites()
    {
        var child1 = GiveMe.AChildEntity();
        var child2 = GiveMe.AChildEntity();
        var childEntities = GiveMe.The<Children>();

        GiveMe.The<Children>().All().ShouldBe([child1, child2]);
    }

    [Test]
    public void Returns_all_entites_with_given_order()
    {
        var parent1 = GiveMe.AParentEntitiy(GiveMe.ADateTime(2024, 1, 23, 14, 21));
        var parent2 = GiveMe.AParentEntitiy(GiveMe.ADateTime(2024, 1, 23, 14, 22));
        var parentEntities = GiveMe.The<Parents>();

        parentEntities.All().ShouldBe([parent1, parent2]);
        parentEntities.All(reverse: true).ShouldBe([parent2, parent1]);
    }
}
