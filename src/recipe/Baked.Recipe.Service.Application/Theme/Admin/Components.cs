using Baked.Ui;

namespace Baked.Theme.Admin;

public class Components
{
    public static readonly IComponentDescriptor None = new ComponentDescriptor(nameof(None));
    public static readonly IComponentDescriptor String = new ComponentDescriptor(nameof(String));

    public static IComponentDescriptor Menu(object data) =>
        new ComponentDescriptor(nameof(Menu))
        {
            Data = new InlineData { Value = data }
        };
}