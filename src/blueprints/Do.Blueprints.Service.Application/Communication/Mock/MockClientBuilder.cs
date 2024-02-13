using Do.Testing;
using Moq;
using Newtonsoft.Json;
using System.Reflection;

namespace Do.Communication.Mock;

public class MockClientBuilder
{
    static readonly MethodInfo _setupClient = typeof(MockClientBuilder).GetMethod("SetupClient", BindingFlags.Static | BindingFlags.NonPublic) ??
        throw new("SetupClient<T, TClient> should have existed");

    static void SetupClient<T, TClient>(Mock<TClient> mock, List<(object? response, Func<Request, bool> when)> setups)
        where TClient : class, IClient<T>
    {
        foreach (var (response, when) in setups)
        {
            mock.Setup(c => c.Send(It.Is<Request>(r => when(r)))).ReturnsAsync(new Response(JsonConvert.SerializeObject(response)));
        }
    }

    readonly Dictionary<Type, List<(object? response, Func<Request, bool> when)>> _list = [];

    public void ForClient<T>(object response) where T : class => ForClient<T>(response, _ => true);
    public void ForClient<T>(object response, Func<Request, bool> when) where T : class
    {
        if (_list.TryGetValue(typeof(T), out var setups))
        {
            setups.Add((response, when));

            return;
        }

        _list[typeof(T)] = [(response, when)];
    }

    internal IMockCollection Build()
    {
        IMockCollection result = new MockCollection();

        foreach (var (type, setups) in _list)
        {
            var clientType = typeof(IClient<>).MakeGenericType(type);

            result.Add(
                service: clientType,
                singleton: true,
                setup: mock => _setupClient.MakeGenericMethod(type, clientType).Invoke(null, [mock, setups])
            );
        }

        return result;
    }
}
