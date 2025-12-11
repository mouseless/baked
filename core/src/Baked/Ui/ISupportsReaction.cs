namespace Baked.Ui;

public interface ISupportsReaction
{
    Dictionary<string, ITrigger>? Reactions { get; set; }

    public void ReloadOn(string @event,
        IConstraint? constraint = default
    ) => Add("reload", new OnTrigger(@event) { Constraint = constraint });

    public void ReloadWhen(string key,
        IConstraint? constraint = default
    ) => Add("reload", new WhenTrigger(key) { Constraint = constraint });

    public void ShowOn(string @event,
        IConstraint? constraint = default
    ) => Add("show", new OnTrigger(@event) { Constraint = constraint });

    public void ShowWhen(string key,
        IConstraint? constraint = default
    ) => Add("show", new WhenTrigger(key) { Constraint = constraint });

    void Add(string reaction, ITrigger trigger)
    {
        Reactions ??= new();

        Reactions.TryGetValue(reaction, out var current);
        Reactions[reaction] = current + trigger;
    }
}