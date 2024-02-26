using Do.Communication;
using Do.Core.Mock;
using Do.MockOverrider;
using Do.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Do;

public static class ServiceSpecExtensions
{
    #region Authentication

    public static Do.Authentication.FixedToken.Middleware AFixedTokenMiddleware(this Stubber giveMe,
       string[]? tokenNames = default
    )
    {
        tokenNames ??= [];

        return new(_ => Task.CompletedTask, giveMe.The<IConfiguration>(), new(TokenNames: [.. tokenNames]));
    }

    #endregion

    #region Client

    public static IClient<T> TheClient<T>(this Mocker mockMe,
        string? url = default,
        string? path = default,
        string? urlEndsWith = default,
        object? response = default,
        string? responseString = default,
        Exception? throws = default,
        List<object>? responses = default,
        bool? noResponse = default
    )
    {
        var mock = Mock.Get(mockMe.Spec.GiveMe.The<IClient<T>>());

        var setup = () => mock.Setup(c => c.Send(It.Is<Request>(r =>
            (url == default || r.UrlOrPath == url) &&
            (path == default || r.UrlOrPath == path) &&
            (urlEndsWith == default || r.UrlOrPath.EndsWith(urlEndsWith))
        )));

        if (throws is not null)
        {
            setup().ThrowsAsync(throws);
        }
        else if (response is not null)
        {
            setup().ReturnsAsync(new Response(response.ToJsonString()));
        }
        else if (responseString is not null)
        {
            setup().ReturnsAsync(new Response(responseString));
        }
        else if (responses is not null)
        {
            setup().ReturnsAsync(responses.Select(r => new Response(r.ToJsonString())).ToArray());
        }
        else if (noResponse == true)
        {
            setup().ReturnsAsync(new Response(string.Empty));
        }

        return mock.Object;
    }

    public static void VerifySent<T>(this IClient<T> source,
        string? path = default,
        string? url = default,
        HttpMethod? method = default,
        object? content = default,
        string? contentContains = default,
        Dictionary<string, string>? form = default,
        (string key, string value)? header = default,
        string? excludesHeader = default,
        int? times = default
    ) => Mock.Get(source).Verify(
        c => c.Send(It.Is<Request>(r =>
            (path == default || r.UrlOrPath == path) &&
            (url == default || r.UrlOrPath == url) &&
            (method == default || r.Method == method) &&
            (content == default || new Content(content, null).Equals(r.Content)) &&
            (contentContains == default || r.Content != null && r.Content.Body.Contains(contentContains)) &&
            (form == default || new Content(form).Equals(r.Content)) &&
            (!header.HasValue || r.Headers[header.GetValueOrDefault().key] == header.GetValueOrDefault().value) &&
            (excludesHeader == default || !r.Headers.ContainsKey(excludesHeader))
        )),
        times is null ? Times.AtLeastOnce() : Times.Exactly(times.Value)
    );

    public static void VerifyNotSent<T>(this IClient<T> source) =>
        Mock.Get(source).Verify(c => c.Send(It.IsAny<Request>()), Times.Never());

    public static void VerifyNoContentIsSent<T>(this IClient<T> source) =>
        Mock.Get(source).Verify(c => c.Send(It.Is<Request>(r => r.Content == null)));

    #endregion

    #region DateTime

    public static DateTime ADateTime(this Stubber _,
        int year = 2023,
        int month = 9,
        int day = 17,
        int hour = 13,
        int minute = 29,
        int second = 00
    ) => new(year, month, day, hour, minute, second);

    #endregion

    #region Dictionary

    public static Dictionary<string, string> ADictionary(this Stubber giveMe) => giveMe.ADictionary<string, string>();
    public static Dictionary<TKey, TValue> ADictionary<TKey, TValue>(this Stubber _, params (TKey, TValue)[] pairs)
        where TKey : notnull
    => pairs.ToDictionary(pair => pair.Item1, pair => pair.Item2);

    #endregion

    #region Entity

    public static void ShouldBeDeleted(this object @object) =>
        ServiceSpec.Session.Contains(@object).ShouldBeFalse($"{@object} should've been deleted, but it's STILL in the session");

    public static void ShouldBeInserted(this object @object) =>
        ServiceSpec.Session.Contains(@object).ShouldBeTrue($"{@object} should've been inserted, but it's NOT in the session");

    #endregion

    #region Guid Extensions

    public static Guid AGuid(this Stubber _,
        string? starts = default
    )
    {
        starts ??= string.Empty;

        const string template = "4d13bbe0-07a4-4b64-9d31-8fef958fbef1";

        return Guid.Parse($"{starts}{template[starts.Length..]}");
    }

    #endregion

    #region HttpRequest

    public static HttpRequest AnHttpRequest(this Stubber giveMe,
    string? schema = default,
    string? host = default,
    string? path = default,
    string? method = default,
    Dictionary<string, string>? query = default,
    Dictionary<string, string>? header = default,
    Dictionary<string, string>? form = default,
    object? body = default,
    object[]? metadata = default
)
    {
        schema ??= "http";
        host ??= "test.com";
        path ??= string.Empty;
        method ??= form is null && body is null ? "GET" : "POST";

        var result = giveMe.AnHttpContext(metadata: metadata).Request;

        result.Scheme = schema;
        result.Host = new(host);
        result.Path = path;
        result.Method = method;

        if (query is not null)
        {
            result.QueryString = new($"?{query.ToQueryString()}");
        }

        if (header is not null)
        {
            foreach (var (key, value) in header)
            {
                result.Headers.Append(key, value);
            }
        }

        if (form is not null)
        {
            result.ContentType = "application/x-www-form-urlencoded";
            result.Form = new FormCollection(form.ToDictionary(kvp => kvp.Key, kvp => new StringValues(kvp.Value)));
        }

        if (body is not null)
        {
            result.ContentType = "application/json";
            result.Body = new MemoryStream(Encoding.UTF8.GetBytes(body.ToJsonString()));
        }

        return result;
    }

    public static HttpContext AnHttpContext(this Stubber giveMe,
        object[]? metadata = default
    )
    {
        var features = new FeatureCollection();
        features.Set(giveMe.Spec.MockMe.AnEndpointFeature(metadata));
        features.Set<IHttpRequestFeature>(new HttpRequestFeature());

        return new DefaultHttpContext(features);
    }

    public static IEndpointFeature AnEndpointFeature(this Mocker mockMe,
        object[]? metadata = default
    )
    {
        metadata ??= [];

        var mock = new Mock<IEndpointFeature>();
        var endpoint = new Endpoint(_ => Task.CompletedTask, new(metadata), mockMe.Spec.GiveMe.AString());

        mock.Setup(ef => ef.Endpoint).Returns(endpoint);

        return mock.Object;
    }

    public static RequestDelegate ARequestDelegate(this Stubber _) =>
        _ => Task.CompletedTask;

    #endregion

    #region Integer

    public static int AnInteger(this Stubber _) => 42;

    #endregion

    #region Json

    public static void ShouldDeeplyBe(this object? payload, object? json) => payload.ToJsonString().ShouldBe(json.ToJsonString());

    [return: NotNullIfNotNull("payload")]
    public static string? ToJsonString(this object? payload) => payload is null ? null : JsonConvert.SerializeObject(payload);
    [return: NotNullIfNotNull("payload")]
    public static object? ToJsonObject(this object? payload) => JsonConvert.DeserializeObject(payload.ToJsonString() ?? string.Empty);

    #endregion

    #region MemoryCache

    public static IMemoryCache AMemoryCache(this Stubber giveMe,
        bool clear = false
    )
    {
        var getMemoryCache = giveMe.The<Func<IMemoryCache>>();
        var memoryCache = getMemoryCache();

        if (clear)
        {
            (memoryCache as MemoryCache)?.Clear();
        }

        return memoryCache;
    }

    public static void ShouldHaveCount(this IMemoryCache memoryCache, int count) =>
        ((MemoryCache)memoryCache).Count.ShouldBe(count);

    #endregion

    #region MockOverrider

    public static T The<T>(this Stubber _, params object?[] mockOverrides) where T : notnull =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService<T>(mockOverrides);

    public static object The(this Stubber _, Type type, params object?[] mockOverrides) =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService(type, mockOverrides);

    public static T An<T>(this Stubber _, params object?[] mockOverrides) where T : notnull =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService<T>(mockOverrides);

    public static object An(this Stubber _, Type type, params object?[] mockOverrides) =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService(type, mockOverrides);

    public static T A<T>(this Stubber _, params object?[] mockOverrides) where T : notnull =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService<T>(mockOverrides);

    public static object A(this Stubber _, Type type, params object?[] mockOverrides) =>
        ServiceSpec.ServiceProvider.OverrideMocksAndGetRequiredService(type, mockOverrides);

    static T OverrideMocksAndGetRequiredService<T>(this IServiceProvider serviceProvider, params object?[] mockOverrides) where T : notnull =>
        (T)serviceProvider.OverrideMocksAndGetRequiredService(typeof(T), mockOverrides);

    static object OverrideMocksAndGetRequiredService(this IServiceProvider serviceProvider, Type type, params object?[] mockOverrides)
    {
        var overrider = serviceProvider.GetRequiredService<IMockOverrider>();

        foreach (var mocked in mockOverrides)
        {
            if (mocked is null) { continue; }

            overrider.Override(mocked);
        }

        return serviceProvider.GetRequiredService(type);
    }

    #endregion

    #region Reflection

    public static PropertyInfo? PropertyOf<T>(this Stubber _, string name) =>
        typeof(T).GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

    public static void ShouldBe<T>(this Type source) => source.ShouldBe(typeof(T));

    public static void ShouldBeAbstract(this PropertyInfo source)
    {
        var getMethod = source.GetGetMethod(true);

        getMethod.ShouldNotBeNull();
        getMethod.ShouldBeAbstract();
    }

    public static void ShouldBeVirtual(this PropertyInfo source)
    {
        var getMethod = source.GetGetMethod(true);

        getMethod.ShouldNotBeNull();
        getMethod.ShouldBeVirtual();
    }

    public static MethodInfo? MethodOf<T>(this Stubber _, string name) =>
        typeof(T).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

    public static void ShouldBeAbstract(this MethodInfo source)
    {
        source.IsAbstract.ShouldBeTrue();
    }

    public static void ShouldBeVirtual(this MethodInfo source)
    {
        source.IsVirtual.ShouldBeTrue();
    }

    public static void ShouldHaveOneParameter<T>(this MethodInfo source)
    {
        source.GetParameters().Length.ShouldBe(1);
        source.GetParameters().First().ParameterType.ShouldBe<T>();
    }

    #endregion

    #region Settings

    public static void ASetting<T>(this Mocker mockMe,
        string? key = default,
        T? value = default
    ) => mockMe.ASetting(key: key, value: $"{value}");

    public static void ASetting(this Mocker mockMe,
        string? key = default,
        string? value = default
    )
    {
        key ??= "Test:Configuration";
        value ??= "value";

        var spec = (ServiceSpec)mockMe.Spec;

        spec.Settings[key] = value;
    }

    internal static IConfiguration TheConfiguration(this Mocker mockMe,
        Func<string, string?>? defaultValueProvider = default,
        Dictionary<string, string>? settings = default
    )
    {
        defaultValueProvider ??= _ => default;
        settings ??= [];

        var configuration = mockMe.Spec.GiveMe.The<IConfiguration>();

        Mock.Get(configuration)
           .Setup(c => c.GetSection(It.IsAny<string>())).Returns((string key) =>
           {
               var mockSection = new Mock<IConfigurationSection>();

               mockSection.Setup(s => s.Value).Returns(() =>
               {
                   if (settings.TryGetValue(key, out var result))
                   {
                       return result;
                   }

                   return defaultValueProvider(key);
               });

               return mockSection.Object;
           });

        return configuration;
    }

    #endregion

    #region String

    public static string AnEmail(this Stubber _) => "info@test.com";

    public static string AString(this Stubber _,
        string? value = default
    ) => value ?? "test string";

    public static Guid ToGuid(this string source) => Guid.Parse(source);

    #endregion

    #region TimeProvider

    public static TimeProvider TheTime(this Mocker mockMe,
        DateTime? now = default,
        bool passSomeTime = false,
        bool reset = false
    )
    {
        var fakeTimeProvider = (ResettableFakeTimeProvider)mockMe.Spec.GiveMe.The<TimeProvider>();

        if (reset)
        {
            fakeTimeProvider.Reset();
        }

        if (now is not null)
        {
            fakeTimeProvider.SetUtcNow(new(now.Value, fakeTimeProvider.LocalTimeZone.BaseUtcOffset));
        }

        if (passSomeTime)
        {
            fakeTimeProvider.Advance(TimeSpan.FromSeconds(1));
        }

        return fakeTimeProvider;
    }

    #endregion

    #region Url Extensions

    public static Uri AUrl(this Stubber giveMe,
        string? url = default
    )
    {
        url ??= $"https://www.{Regex.Replace(giveMe.AGuid().ToString(), "[0-9]", "x")}.com";

        return new(url);
    }

    public static void ShouldBe(this Uri? uri, string urlString) => uri?.ToString().ShouldBe(urlString);

    #endregion
}
