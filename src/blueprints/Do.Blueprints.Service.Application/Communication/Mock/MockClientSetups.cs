namespace Do.Communication.Mock;

public class MockClientSetups
{
    readonly Dictionary<Type, List<(object? response, Func<Request, bool> when)>> _setups = [];

    public void ForClient<T>(object response) where T : class => ForClient<T>(response, _ => true);
    public void ForClient<T>(object response, Func<Request, bool> when) where T : class
    {
        if (_setups.TryGetValue(typeof(T), out var setups))
        {
            setups.Add((response, when));

            return;
        }

        _setups[typeof(T)] = [(response, when)];
    }

    internal Dictionary<Type, List<(object? response, Func<Request, bool> when)>> Values => _setups;
}
