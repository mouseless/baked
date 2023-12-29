using Do.Configuration;

namespace Do.ExceptionHandling.Default;

public record ExceptionHandlerSettings(Setting<string>? TypeUrlFormat);
