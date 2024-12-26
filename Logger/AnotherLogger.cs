/*
@title			Dulcet - Music Player
@author			Harish B
@createdOn		20-12-2024
@modifiedOn		-
@reviewedBy		-
@reviewedOn		-
*/

using System.Diagnostics;
using MusicPlayerSystem_v1_Database.Utilities.Enum;
using MusicPlayerSystem_v1_Database.Utilities.Interface;

namespace MusicPlayerSystem_v1_Database.Logger
{
    class AnotherLogger : ICustomLogger
    {
        public void WriteCore(LogLevel level, string message)
        {
            Console.WriteLine($"{level}: {message}");
        }
    }

    class TraceLogger : ICustomLogger
    {
        public void WriteCore(LogLevel level, string message)
        {
            switch(level)
            {
                case LogLevel.Information:
                    Trace.TraceInformation(message);
                    break;
                case LogLevel.Warning:
                    Trace.TraceWarning(message);
                    break;
                case LogLevel.Error:
                    Trace.TraceError(message);
                    break;
            }
        }
    }
}