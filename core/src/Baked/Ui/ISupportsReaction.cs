namespace Baked.Ui;

public interface ISupportsReaction
{
    Dictionary<string, ITrigger>? Reactions { get; set; }
}