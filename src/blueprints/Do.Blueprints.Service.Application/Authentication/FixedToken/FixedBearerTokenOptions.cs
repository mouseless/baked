namespace Do.Authentication.FixedToken;

public record FixedBearerTokenOptions(
    List<string> TokenNames,
    ClaimsPrincipalFactoryOptions ClaimsPrincipalFactoryOptions
);
