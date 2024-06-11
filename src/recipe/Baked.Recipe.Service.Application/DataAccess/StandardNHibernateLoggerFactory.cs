using Microsoft.Extensions.Logging;
using NHibernate;

using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace Baked.DataAccess;

public class StandardNHibernateLoggerFactory(ILoggerFactory _loggerFactory)
    : INHibernateLoggerFactory
{
    public INHibernateLogger LoggerFor(string keyName) =>
        new StandardLogger(_loggerFactory.CreateLogger(keyName));

    public INHibernateLogger LoggerFor(Type type) =>
        new StandardLogger(_loggerFactory.CreateLogger(type));

    class StandardLogger(ILogger _logger)
        : INHibernateLogger
    {
        public bool IsEnabled(NHibernateLogLevel logLevel) =>
            _logger.IsEnabled((LogLevel)logLevel);

        public void Log(NHibernateLogLevel logLevel, NHibernateLogValues state, Exception exception) =>
            _logger.Log((LogLevel)logLevel, exception, state.Format, state.Args);
    }
}