﻿using Baked.Testing;
using Moq;
using Newtonsoft.Json;
using System.Reflection;

namespace Baked.Communication.Mock;

public class DefaultResponseBuilder
{
    static readonly MethodInfo _setupClient = typeof(DefaultResponseBuilder).GetMethod(nameof(SetupClient), BindingFlags.Static | BindingFlags.NonPublic)
        ?? throw new("SetupClient<T> should have existed");

    static void SetupClient<T>(Mock<IClient<T>> mock, List<(string response, Func<Request, bool> when)> setups)
        where T : class
    {
        foreach (var (response, when) in setups)
        {
            mock.Setup(c => c.Send(It.Is<Request>(r => when(r))))
                .ReturnsAsync(new Response(response));
        }
    }

    readonly Dictionary<Type, List<(string? response, Func<Request, bool> when)>> _setups = [];

    public void ForClient<T>(object response,
        Func<Request, bool>? when = default
    ) where T : class =>
        ForClient<T>(JsonConvert.SerializeObject(response), when);

    public void ForClient<T>(string responseString,
        Func<Request, bool>? when = default
    ) where T : class
    {
        when ??= _ => true;

        if (!_setups.TryGetValue(typeof(T), out var setups))
        {
            setups = _setups[typeof(T)] = [];
        }

        setups.Add((responseString, when));
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