namespace Baked.Ui;

public interface IData
{
    string Type { get; }
    bool? RequireLocalization { get; }

    public static IData operator +(IData? left, IData right)
    {
        if (left is null) { return right; }
        if (left is not CompositeData composite)
        {
            composite = new CompositeData { Parts = [left] };
        }

        composite.Parts.Add(right);

        return composite;
    }
}