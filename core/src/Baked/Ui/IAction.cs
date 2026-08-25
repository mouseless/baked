namespace Baked.Ui;

public interface IAction
{
    string Type { get; }

    public static IAction operator +(IAction? left, IAction right)
    {
        if (left is null) { return right; }
        if (left is not CompositeAction composite)
        {
            composite = new() { Parts = [left] };
        }

        composite.Parts.Add(right);

        return composite;
    }
}