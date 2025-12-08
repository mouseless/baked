namespace Baked.Ui;

public interface ISupportsReaction
{
    public Dictionary<string, Reaction>? On { get; set; }

    public void ReloadOn(string @event) =>
        OnEvent(@event, Reaction.Reload);

    public void ShowOn(string @event) =>
        OnEvent(@event, Reaction.Show);

    public void HideOn(string @event) =>
        OnEvent(@event, Reaction.Hide);

    public void OnEvent(string @event, Reaction reaction)
    {
        On ??= new();

        On[@event] = reaction;
    }
}