using Microsoft.Extensions.Time.Testing;

namespace Do.Core.Mock;

public class FakeFakeTimeProvider : TimeProvider
{
    public FakeTimeProvider Inner { get; set; } = new();

    public override TimeZoneInfo LocalTimeZone => Inner.LocalTimeZone;
    public override long TimestampFrequency => Inner.TimestampFrequency;

    public override ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period) =>
        Inner.CreateTimer(callback, state, dueTime, period);

    public override long GetTimestamp() =>
        Inner.GetTimestamp();

    public override DateTimeOffset GetUtcNow() =>
        Inner.GetUtcNow();
}