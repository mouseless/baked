using Baked.Authorization;

namespace Baked.Test.Theme;

[AllowAnonymous]
public class FormSample(TimeProvider _timeProvider)
{
    static readonly List<StateLog> _states = [];

    public void AddState(string state, string reason, int count,
        CountOptions? countOptions = default
    )
    {
        foreach (var i in Enumerable.Range(0, count))
        {
            _states.Add(new($"{state}-{_timeProvider.GetNow()}", $"State is switched to {state}-{reason}-{i}-{countOptions}: {_timeProvider.GetNow()}"));
        }
    }

    public void ClearStates()
    {
        _states.Clear();
    }

    public List<StateLog> GetStates() =>
        _states;
}

public record StateLog(string Id, string Status);