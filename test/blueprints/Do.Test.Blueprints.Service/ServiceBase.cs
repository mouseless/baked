namespace Do.Test;

public abstract class ServiceBase(TimeProvider _timeProvider) : IService
{
    public DateTime GetNow() => _timeProvider.GetNow();
}
