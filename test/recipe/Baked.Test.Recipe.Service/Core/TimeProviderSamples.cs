namespace Baked.Test.Core;

public class TimeProviderSamples(TimeProvider _timeProvider)
{
    public DateTime GetNow() =>
        _timeProvider.GetNow();

    public DateTimeOffset GetUtcNow() =>
        _timeProvider.GetUtcNow();

    public DateTimeOffset GetLocalNow() =>
        _timeProvider.GetLocalNow();
}