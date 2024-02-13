namespace Do.Communication.Mock;

public record MockClientSetup(object? Response, Func<Request, bool> When)
{
    public MockClientSetup(object? response) : this(response, _ => true) { }
}
