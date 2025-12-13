namespace Baked.Ui;

public interface ITrigger
{
    string Type { get; }

    public static ITrigger operator +(ITrigger? left, ITrigger right)
    {
        if (left is null) { return right; }
        if (left is not CompositeTrigger composite)
        {
            composite = new CompositeTrigger { Parts = [left] };
        }

        composite.Parts.Add(right);

        return composite;
    }
}