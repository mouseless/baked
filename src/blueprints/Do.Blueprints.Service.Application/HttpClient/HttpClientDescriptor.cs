namespace Do.HttpClient;

public record HttpClientDescriptor(
    string Name,
    Uri? BaseAddress = default,
    Dictionary<string, string>? DefaultHeaders = default
);
