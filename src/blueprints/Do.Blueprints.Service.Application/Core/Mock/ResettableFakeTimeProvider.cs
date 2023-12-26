using Microsoft.Extensions.Time.Testing;

namespace Do.Core.Mock;

public class ResettableFakeTimeProvider : TimeProvider
{
    private FakeTimeProvider _inner = new();

    public DateTimeOffset Start => _inner.Start;
    public TimeSpan AutoAdvanceAmount
    {
        get => _inner.AutoAdvanceAmount;
        set => _inner.AutoAdvanceAmount = value;
    }

    public override TimeZoneInfo LocalTimeZone => _inner.LocalTimeZone;
    public override long TimestampFrequency => _inner.TimestampFrequency;

    public void Reset() =>
        _inner = new();

    public void SetUtcNow(DateTimeOffset time) =>
        _inner.SetUtcNow(time);

    public void Advance(TimeSpan delta) =>
        _inner.Advance(delta);

    public void SetLocalTimeZone(TimeZoneInfo localTimeZone) =>
        _inner.SetLocalTimeZone(localTimeZone);

    public override ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period) =>
        _inner.CreateTimer(callback, state, dueTime, period);

    public override long GetTimestamp() =>
        _inner.GetTimestamp();

    public override DateTimeOffset GetUtcNow() =>
        _inner.GetUtcNow();
}