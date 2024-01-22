namespace Do.Test;

public abstract class Base(TimeProvider _timeProvider) : ISingleton
{
    public DateTime GetNow() => _timeProvider.GetNow();
}
