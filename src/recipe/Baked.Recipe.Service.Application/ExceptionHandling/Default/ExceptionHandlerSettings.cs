using Baked.Runtime.Configuration;

namespace Baked.ExceptionHandling.Default;

public record ExceptionHandlerSettings(Setting<string>? TypeUrlFormat);