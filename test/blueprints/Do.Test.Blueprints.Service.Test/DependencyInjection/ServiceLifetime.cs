namespace Do.Test.DependencyInjection;

public class ServiceLifetime : TestServiceSpec
{
    [Test]
    public void A_single_instance_of_singleton_is_shared_across_application()
    {
        var singleton1 = GiveMe.The<Singleton>();
        var singleton2 = GiveMe.The<Singleton>();

        singleton1.ShouldBeSameAs(singleton2);
    }

    [Test]
    public void New_instance_of_transient_is_created_at_each_request()
    {
        var entity1 = GiveMe.An<Entity>();
        var entity2 = GiveMe.An<Entity>();

        entity1.ShouldNotBeSameAs(entity2);
    }
}
