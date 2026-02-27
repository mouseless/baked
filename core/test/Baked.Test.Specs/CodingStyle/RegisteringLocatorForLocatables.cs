using Baked.Business;
using Baked.Orm;
using Baked.Playground.CodingStyle.RichTransient;
using Baked.Playground.Orm;

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
        var locator = GiveMe.The<ILocator<RichTransientWithData>>();

        locator.GetType().Name.ShouldBe($"{nameof(RichTransientWithData)}Locator");
    }

    [Test]
    public void Rich_transients_with_non_public_also_use_custom_generated_locator()
    {
        var locator = GiveMe.The<ILocator<RichTransientParent>>();

        locator.GetType().Name.ShouldBe($"{nameof(RichTransientParent)}Locator");
    }
}