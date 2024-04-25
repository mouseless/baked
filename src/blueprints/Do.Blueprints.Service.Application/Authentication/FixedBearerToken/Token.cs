namespace Do.Authentication.FixedBearerToken;

public record Token(string Name, List<string> Claims);