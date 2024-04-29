namespace Do.Test.Authentication;

public record IdentityData(string Name, IEnumerable<ClaimData> Claims);