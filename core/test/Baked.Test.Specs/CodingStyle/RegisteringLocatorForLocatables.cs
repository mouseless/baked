using Baked.Business;
using Baked.Orm;
using Baked.Playground.CodingStyle.ResourceViaIdInitializer;

using Entity = Baked.Playground.Orm.Entity;

namespace Baked.Test.CodingStyle;

public class RegisteringLocatorForLocatables : TestSpec
{
    [Test]
    public void Entities_use_entity_locator()
    {
        var locator = GiveMe.The<ILocator<Entity>>();

        locator.ShouldBeOfType<EntityLocator<Entity>>();
    }

    [Test]
    public void Rich_transients_use_custom_generated_locator()
    {
        var locator = GiveMe.The<ILocator<ResourceWithData>>();

        locator.GetType().Name.ShouldBe($"{nameof(ResourceWithData)}Locator");
    }

    [Test]
    public void Rich_transients_with_non_public_also_use_custom_generated_locator()
    {
        var locator = GiveMe.The<ILocator<ResourceParent>>();

        locator.GetType().Name.ShouldBe($"{nameof(ResourceParent)}Locator");
    }
}