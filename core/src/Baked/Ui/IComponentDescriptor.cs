namespace Baked.Ui;

public interface IComponentDescriptor : ISupportsReaction
{
    string Type { get; set; }
    IComponentSchema Schema { get; }
    IData? Data { get; set; }
    public IAction? Action { get; set; }
    public bool? ActionSkipsEmptyModel { get; set; }

    public static IComponentDescriptor operator +(IComponentDescriptor? left, IComponentDescriptor right)
    {
        if (left is null) { return right; }
        if (left is not ComponentDescriptor<Composite> composite)
        {
            composite = new Composite { Parts = { left } }.ToDescriptor();
        }

        composite.Schema.Parts.Add(right);

        return composite;
    }
}