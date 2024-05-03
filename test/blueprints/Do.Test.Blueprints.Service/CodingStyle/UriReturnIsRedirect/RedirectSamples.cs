namespace Do.Test.CodingStyle.UriReturnIsRedirect;

public class RedirectSamples
{
    Uri _uri = default!;

    public RedirectSamples With(Uri uri)
    {
        _uri = uri;

        return this;
    }

    public Uri FormPost(string key) =>
        new($"{_uri}?key={key}");

    public Uri Callback() =>
        _uri;
}