namespace Baked.Ui;

public interface ISupportsReaction
{
    Dictionary<Reaction, ITrigger>? Reactions { get; set; }

    public void ReloadOn(string @event,
        IConstraint? payloadConstraint = default
    ) => React(Reaction.Reload, new OnTrigger(@event, PayloadConstraint: payloadConstraint));

    public void ShowOn(string @event,
        IConstraint? payloadConstraint = default
    ) => React(Reaction.Show, new OnTrigger(@event, PayloadConstraint: payloadConstraint));

    public void ReloadWhen(string key,
        IConstraint? valueConstraint = default
    ) => React(Reaction.Reload, new WhenTrigger(key, ValueConstraint: valueConstraint));

    public void ShowWhen(string key,
        IConstraint? valueConstraint = default
    ) => React(Reaction.Show, new WhenTrigger(key, ValueConstraint: valueConstraint));

    void React(Reaction reaction, ITrigger trigger)
    {
        Reactions ??= new();

        Reactions.TryGetValue(reaction, out var current);
        Reactions[reaction] = current + trigger;
    }
}

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

public interface IConstraint;

public record IsConstraint(string Is);
public record IsNotConstraint(string IsNot);
public record ComposableConstraint(string Matches, IData? Options = default);

/*
{
    "reload": {
        "on": "selection-changed",
        "valueConstraint": { "composable": "useConstraint",  options: { "type": "Inline", "value": "test" } }
    },
    "show": {
        "when": "selection",
        "valueConstraint": { "is": "expected" }
    }
}
*/

public record OnTrigger(string On, IConstraint? PayloadConstraint = default) : ITrigger;
public record WhenTrigger(string When, IConstraint? ValueConstraint = default) : ITrigger;
public class Triggers : List<ITrigger>, ITrigger;