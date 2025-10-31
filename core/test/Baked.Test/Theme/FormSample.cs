using Baked.Authorization;

namespace Baked.Test.Theme;

[AllowAnonymous]
public class FormSample(TimeProvider _timeProvider)
{
    static readonly List<StateLog> _stateLogs = [];
    static bool _on = false;

    public void ChangeState()
    {
        _on = !_on;

        _stateLogs.Add(new($"{_on}-{_timeProvider.GetNow()}", $"State is switched to {_on}"));
    }

    public List<StateLog> GetStates() =>
        _stateLogs;
}

public record StateLog(string Id, string Status);