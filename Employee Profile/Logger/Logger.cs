using log4net.Config;
using log4net.Core;
using log4net;
using System.Reflection;

namespace Employee_Profile.Logger
{
    public interface ILogger
    {
        public void LogError(string message);
        public void LogDebug(string message);
    }
    public class Logger : ILogger
    {
        ILog _logger;
        public Logger() {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            _logger = LogManager.GetLogger(typeof(LoggerManager));
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }
    }
}
