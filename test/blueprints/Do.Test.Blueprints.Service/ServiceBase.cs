namespace Do.Test;

public abstract class ServiceBase(TimeProvider _timeProvider)
{
    public DateTime GetNow() => _timeProvider.GetNow();
}
