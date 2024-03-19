using Do.Architecture;
using Do.Core;
using Do.Core.Mock;
using Do.Testing;
using Newtonsoft.Json;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Do;

public static class CoreExtensions
{
    public static void AddCore(this List<IFeature> source, Func<CoreConfigurator, IFeature<CoreConfigurator>> configure) => source.Add(configure(new()));

    public static int AnInteger(this Stubber _) => 42;

    public static string AnEmail(this Stubber _) => "info@test.com";

    public static string AString(this Stubber _,
        string? value = default
    ) => value ?? "test string";

    public static Guid ToGuid(this string source) => Guid.Parse(source);

    public static DateTime ADateTime(this Stubber _,
        int year = 2023,
        int month = 9,
        int day = 17,
        int hour = 13,
        int minute = 29,
        int second = 00
    ) => new(year, month, day, hour, minute, second);

    public static Dictionary<string, string> ADictionary(this Stubber giveMe) => giveMe.ADictionary<string, string>();
    public static Dictionary<TKey, TValue> ADictionary<TKey, TValue>(this Stubber _, params (TKey, TValue)[] pairs)
        where TKey : notnull
    => pairs.ToDictionary(pair => pair.Item1, pair => pair.Item2);

    public static Guid AGuid(this Stubber _,
        string? starts = default
    )
    {
        starts ??= string.Empty;

        const string template = "4d13bbe0-07a4-4b64-9d31-8fef958fbef1";

        return Guid.Parse($"{starts}{template[starts.Length..]}");
    }

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

    public static Uri AUrl(this Stubber giveMe,
        string? url = default
    )
    {
        url ??= $"https://www.{Regex.Replace(giveMe.AGuid().ToString(), "[0-9]", "x")}.com";

        return new(url);
    }

    public static void ShouldBe(this Uri? uri, string urlString) => uri?.ToString().ShouldBe(urlString);

    public static void ShouldDeeplyBe(this object? payload, object? json) => payload.ToJsonString().ShouldBe(json.ToJsonString());

    [return: NotNullIfNotNull("payload")]
    public static string? ToJsonString(this object? payload) => payload is null ? null : JsonConvert.SerializeObject(payload);
    [return: NotNullIfNotNull("payload")]
    public static object? ToJsonObject(this object? payload) => JsonConvert.DeserializeObject(payload.ToJsonString() ?? string.Empty);
}
