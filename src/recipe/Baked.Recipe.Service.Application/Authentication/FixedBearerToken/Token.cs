namespace Do.Authentication.FixedBearerToken;

public record Token(string Name, IEnumerable<string> Claims);