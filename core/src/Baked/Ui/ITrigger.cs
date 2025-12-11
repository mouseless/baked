namespace Baked.Ui;

public interface ITrigger
{
    public static ITrigger operator +(ITrigger? left, ITrigger right)
    {
        if (left is null) { return right; }
        if (left is not Triggers triggers)
        {
            triggers = new Triggers { left };
        }

        triggers.Add(right);

        return triggers;
    }
}