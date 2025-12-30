using B = Baked.Ui.Components;

namespace Baked.Ui;

public interface IComponentDescriptor : ISupportsReaction
{
    string Type { get; }
    IComponentSchema Schema { get; }
    IData? Data { get; set; }
    public IAction? Action { get; set; }

    public static IComponentDescriptor operator +(IComponentDescriptor? left, IComponentDescriptor right)
    {
        if (left is null) { return right; }
        if (left is not ComponentDescriptor<Composite> composite)
        {
            composite = B.Composite(options: c => c.Parts.Add(left));
        }

        composite.Schema.Parts.Add(right);

        return composite;
    }
}