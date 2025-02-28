namespace Baked.Ui;

public class ComputedData(string Composable) : IData
{
    public string Type => "Computed";
    public string Composable { get; set; } = Composable;
}