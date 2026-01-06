using Baked.Playground.Orm;
using NHibernate.Proxy;

namespace Baked.Test.Orm;

public class AutoProxySupport : TestSpec
{
    [Test]
    public void Private_methods_gets_proxified_even_if_call_to_proxy_occured_through_an_explicit_interface()
    {
        GiveMe.AParent(withChild: true);
        GiveMe.TheSession(clear: true);
        var children = GiveMe.The<Children>();

        var child = children.FirstBy(fetchParents: false);
        IParentInterface? parent = child?.Parent;

        parent.ShouldNotBeNull();
        parent.ShouldBeAssignableTo<INHibernateProxy>();
        parent.IsContextNull().ShouldBeFalse();
    }
}