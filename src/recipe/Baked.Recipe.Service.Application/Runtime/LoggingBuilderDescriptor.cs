using Microsoft.Extensions.Logging;

namespace Baked.Runtime;

public record LoggingBuilderDescriptor(Action<ILoggingBuilder> Configure);