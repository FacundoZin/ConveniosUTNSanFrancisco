
namespace APIconvenios.Helpers.Logger
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter == null) return;

            var logMessage = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} [{logLevel}] {formatter(state, exception)}{Environment.NewLine}";
            File.AppendAllText(_filePath, logMessage);
        }
    }
}
