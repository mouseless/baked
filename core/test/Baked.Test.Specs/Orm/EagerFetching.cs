using Baked.Business;
using Baked.Playground.Orm;
using NHibernate.Proxy;

namespace Baked.Test.Orm;

public class EagerFetching : TestSpec
{
    [Test]
    public void Parents_are_fetched_eagerly_in_queries()
    {
        GiveMe.AParent(withChild: true);
        GiveMe.TheSession(clear: true);
        var children = GiveMe.The<Children>();

        var child = children.By().First();

        child.Parent.ShouldNotBeAssignableTo<INHibernateProxy>();
    }

    [Test]
    public void Parents_are_fetched_eagerly_in_locators()
    {
        var parent = GiveMe.AParent(withChild: true);
        var childId = parent.GetChildren().First().Id;
        GiveMe.TheSession(clear: true);
        var childLocator = GiveMe.The<ILocator<Child>>();

        var child = childLocator.Locate(childId);

        child.Parent.ShouldNotBeAssignableTo<INHibernateProxy>();
    }
}