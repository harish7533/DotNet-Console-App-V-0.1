/*
@title			Dulcet - Music Player
@author			Harish B
@createdOn		17-12-2024
@modifiedOn		-
@reviewedBy		-
@reviewedOn		-
*/

using Microsoft.Extensions.Logging;
namespace MusicPlayerSystem_v1_Database.Logger
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;
        public FileLogger(string filepath)
        {
            _filePath = filepath;
        }
        public IDisposable BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel logLevel) => true;
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            var message = formatter(state, exception);
            var logMessage = $"{DateTime.Now} [{logLevel}] {message}";
            File.AppendAllText(_filePath, logMessage + Environment.NewLine);
        }
    }
}