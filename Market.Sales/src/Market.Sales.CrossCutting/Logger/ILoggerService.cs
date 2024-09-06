namespace Market.Sales.CrossCutting.Logger
{
    public interface ILoggerService<T> where T : class
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message, Exception? exception = null);
        void LogDebug(string message);
    }
}