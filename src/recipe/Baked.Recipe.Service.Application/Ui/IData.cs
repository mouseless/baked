namespace Baked.Ui;

public interface IData
{
    public string Type { get; }
    public bool? RequireLocalization => default;
}