namespace Baked.Ui;

public record ReloadAction : IAction
{
    public string Type => "Reload";
    public bool? IgnoreOnEmpty { get; set; }
}