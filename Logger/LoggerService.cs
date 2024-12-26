/*
@title			Dulcet - Music Player
@author			Harish B
@createdOn		16-12-2024
@modifiedOn		-
@reviewedBy		-
@reviewedOn		-
*/

using Microsoft.Extensions.Logging;

namespace MusicPlayerSystem_v1_Database.Logger
{
    public class LoggerService(ILogger<LoggerService> logger)
    {
        private readonly ILogger<LoggerService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public void LogSuccessMessage(string message)
        {
            _logger.LogInformation(message);
        }
        public void LogErrorMessage(string message)
        {
            _logger.LogError(message);
        }
        public void LogExceptionMessage(string exceptionMessage)
        {
            _logger.LogWarning(exceptionMessage);
        }
    }
}
