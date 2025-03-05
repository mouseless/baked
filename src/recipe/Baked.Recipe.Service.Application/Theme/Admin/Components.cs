using Baked.Ui;

namespace Baked.Theme.Admin;

public static class Components
{
    public static readonly IComponentDescriptor None = new ComponentDescriptor(nameof(None));

    public static IComponentDescriptor String(
        IData? data = default
    ) => new ComponentDescriptor(nameof(String))
    {
        Data = data
    };
}