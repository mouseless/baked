namespace Do.Test;

public class Authentication(TimeProvider _timeProvider)
{
    public DateTime GetTime() => _timeProvider.GetNow();

    public object TestFormPostAuthentication(object value) => value;
}
