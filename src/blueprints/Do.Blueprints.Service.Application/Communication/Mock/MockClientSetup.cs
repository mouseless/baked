namespace Do.Communication.Mock;

public record MockClientSetup(Func<Request, bool> Match, object? Response);
