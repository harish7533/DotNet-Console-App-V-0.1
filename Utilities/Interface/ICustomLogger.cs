using MusicPlayerSystem_v1_Database.Utilities.Enum;

namespace MusicPlayerSystem_v1_Database.Utilities.Interface
{
    interface ICustomLogger
    {
        void WriteCore(LogLevel level, string message);

        void WriteInformation(string message)
        {
            WriteCore(LogLevel.Information, message);
        }
        void WriteWarning(string message)
        {
            WriteCore(LogLevel.Warning, message);
        }
        void WriteError(string message)
        {
            WriteCore(LogLevel.Error, message);
        }
    }
}