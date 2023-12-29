using Do.Configuration;

namespace Do.ExceptionHandling.Default;

public record ExceptionSettings(Setting<string>? TypeUrlFormat);