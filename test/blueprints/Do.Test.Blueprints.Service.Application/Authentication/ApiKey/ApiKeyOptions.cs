namespace Do.Test.Authentication.ApiKey;

public record ApiKeyOptions(
    string IdentityName,
    IEnumerable<string> Claims
);