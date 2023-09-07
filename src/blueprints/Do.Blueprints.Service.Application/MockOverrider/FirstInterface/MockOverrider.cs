using Moq;
using System.Collections.Concurrent;

namespace Do.MockOverrider.FirstInterface;

public class MockOverrider : IMockOverrider
{
    readonly ConcurrentDictionary<Type, object> _overrides = new();
    readonly List<(Type, object)> _resetMocks = new();

    public object? Get(Type type)
    {
        _overrides.TryRemove(type, out var result);

        return result;
    }

    public void Override(object mocked)
    {
        _overrides.TryAdd(mocked.GetType().GetInterfaces().First(), mocked);
    }

    public void Reset()
    {
        _overrides.Clear();

        foreach (var (_, mock) in _resetMocks)
        {
            Mock.Get(mock).Reset();
        }
    }

    internal void ResetEventually(Type type, object mock)
    {
        _resetMocks.Add((type, mock));
    }
}
