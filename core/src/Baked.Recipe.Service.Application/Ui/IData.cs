namespace Baked.Ui;

public interface IData
{
    string Type { get; }
    bool? RequireLocalization { get; }
}