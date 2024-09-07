
using Microsoft.Extensions.Logging;

namespace Market.Inventory.CrossCutting.Logger
{
    public class LoggerService<T>(ILogger<T> logger) : ILoggerService<T> where T : class
    {

        private readonly ILogger<T> _logger = logger;
        public void LogDebug(string message)
        {
            _logger.LogDebug("{Message}", message);
        }

        public void LogError(string message, Exception? exception = null)
        {

            if (exception != null)
            {
                _logger.LogError(exception, "{Message}", message);
            }
            else
            {
                _logger.LogError("{Message}", message);
            }

        }

        public void LogInformation(string message)
        {
            _logger.LogInformation("{Message}", message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning("{Message}", message);
        }
    }
}