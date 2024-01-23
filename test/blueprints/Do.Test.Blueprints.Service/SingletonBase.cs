namespace Do.Test;

public abstract class SingletonBase(TimeProvider _timeProvider) : ISingleton
{
    public DateTime GetNow() => _timeProvider.GetNow();
}
