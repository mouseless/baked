namespace Baked.Playground.CodingStyle.UriReturnIsRedirect;

public class RedirectSamples
{
    Uri _uri = default!;

    /// <param name="uri">
    /// uri description
    /// </param>
    public RedirectSamples With(Uri uri)
    {
        _uri = uri;

        return this;
    }

    /// <summary>
    /// Form post summary
    /// </summary>
    /// <remarks>
    /// Form post description
    /// </remarks>
    /// <param name="key">
    /// key description
    /// </param>
    public Uri FormPost(string key) =>
        new($"{_uri}?key={key}");

    public Task<Uri> FormPostAsync(string key) =>
        Task.FromResult(FormPost(key));

    public Uri Callback() =>
        _uri;

    public Task<Uri> CallbackAsync() =>
        Task.FromResult(Callback());
}