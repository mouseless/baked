using System.Collections.Concurrent;

namespace Do.MockOverrider.FirstInterface;

public class MockOverrider : IMockOverrider
{
    readonly ConcurrentDictionary<Type, object> _overrides = new();

    public object? Get(Type type)
    {
        _overrides.TryRemove(type, out var result);

        return result;
    }

    public void Override(object mocked)
    {
        _overrides.TryAdd(mocked.GetType().GetInterfaces().First(), mocked);
    }
}
