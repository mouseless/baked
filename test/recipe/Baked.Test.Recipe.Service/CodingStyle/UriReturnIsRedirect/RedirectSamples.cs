namespace Baked.Test.CodingStyle.UriReturnIsRedirect;

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

    public Task<Uri> FormPostAsync(string key) =>
        Task.FromResult(FormPost(key));

    public Uri Callback() =>
        _uri;

    public Task<Uri> CallbackAsync() =>
        Task.FromResult(Callback());
}