using Baked.Runtime;

namespace Baked.ExceptionHandling.ProblemDetails;

public record ExceptionHandlerSettings(Setting<string>? TypeUrlFormat);