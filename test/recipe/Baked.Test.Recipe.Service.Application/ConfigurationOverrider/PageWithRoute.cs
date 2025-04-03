using Baked.Ui;

namespace Baked.Test.ConfigurationOverrider;

public class PageWithRoute : IComponentSchema
{
    public IData Title { get; set; } = new InlineData("PageWithRoute");
}
