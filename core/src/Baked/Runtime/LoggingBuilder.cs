using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Baked.Runtime;

public class LoggingBuilder(IServiceCollection _services)
    : ILoggingBuilder
{
    public IServiceCollection Services => _services;
}