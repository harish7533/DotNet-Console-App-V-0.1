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
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _filepath;
        public FileLoggerProvider(string filepath)
        {
            _filepath = filepath;
        }
        public ILogger CreateLogger(string categoryName) => new FileLogger(_filepath);
        public void Dispose() { }
    }
}