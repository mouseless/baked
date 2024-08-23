using Baked.Testing;
using Moq;
using Newtonsoft.Json;
using System.Reflection;

namespace Baked.Communication.Mock;

public class DefaultResponseBuilder
{
    static readonly MethodInfo _setupClient = typeof(DefaultResponseBuilder).GetMethod(nameof(SetupClient), BindingFlags.Static | BindingFlags.NonPublic)
        ?? throw new("SetupClient<T> should have existed");

    static void SetupClient<T>(Mock<IClient<T>> mock, List<(string response, StatusCode statusCode, Func<Request, bool> when)> setups)
        where T : class
    {
        foreach (var (response, statusCode, when) in setups)
        {
            mock.Setup(c => c.Send(It.Is<Request>(r => when(r)), It.IsAny<bool>()))
                .ReturnsAsync(new Response(statusCode, response));
        }
    }

    readonly Dictionary<Type, List<(string? response, StatusCode statusCode, Func<Request, bool> when)>> _setups = [];

    public void ForClient<T>(object response,
        StatusCode? statusCode = default,
        Func<Request, bool>? when = default
    ) where T : class =>
        ForClient<T>(JsonConvert.SerializeObject(response), statusCode, when);

    public void ForClient<T>(string responseString,
        StatusCode? statusCode = default,
        Func<Request, bool>? when = default
    ) where T : class
    {
        statusCode ??= StatusCode.Success;
        when ??= _ => true;

        if (!_setups.TryGetValue(typeof(T), out var setups))
        {
            setups = _setups[typeof(T)] = [];
        }

        setups.Add((responseString, statusCode.GetValueOrDefault(), when));
    }

    internal IMockCollection BuildMockClients()
    {
        var result = new MockCollection();

        foreach (var (type, setups) in _setups)
        {
            result.Add(
                service: typeof(IClient<>).MakeGenericType(type),
                singleton: true,
                setup: mock => _setupClient.MakeGenericMethod(type).Invoke(null, [mock, setups])
            );
        }

        return result;
    }
}