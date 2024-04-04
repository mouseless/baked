namespace Do.Test;

public class AuthenticationTests(TimeProvider _timeProvider)
{
    public DateTime GetNow() => _timeProvider.GetNow();

    public object TestFormPostAuthentication(object value) => value;
}
