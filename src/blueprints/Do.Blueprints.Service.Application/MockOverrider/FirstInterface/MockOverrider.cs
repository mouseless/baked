using Moq;
using System.Collections.Concurrent;
using System.Reflection;

namespace Do.MockOverrider.FirstInterface;

public class MockOverrider : IMockOverrider
{
    readonly ConcurrentDictionary<Type, object> _overrides = new();
    readonly ConcurrentBag<(Type, object)> _resetMocks = new();

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

        foreach (var (type, mockedObject) in _resetMocks)
        {
            var getMethod = typeof(Mock).GetMethod(nameof(Mock.Get), BindingFlags.Static | BindingFlags.Public) ?? throw new InvalidOperationException("method should not be null");
            var genericGetMethod = getMethod.MakeGenericMethod(type);

            Mock mock = (Mock)(genericGetMethod.Invoke(null, new[] { mockedObject }) ?? throw new InvalidOperationException("invoke result should not be null"));

            mock.Reset();
        }
    }

    internal void ResetEventually(Type type, object mock)
    {
        _resetMocks.Add((type, mock));
    }
}
