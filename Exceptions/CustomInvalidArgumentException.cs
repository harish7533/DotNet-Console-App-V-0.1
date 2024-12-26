/*
@title			Dulcet - Music Player
@author			Harish B
@createdOn		18-12-2024
@modifiedOn		-
@reviewedBy		-
@reviewedOn		-
*/
namespace MusicPlayerSystem_v1_Database.Exceptions {
public class CustomInvalidArgumentException : Exception
{
    public CustomInvalidArgumentException() : base() { }
    public CustomInvalidArgumentException(string exceptionMessage) : base(exceptionMessage) { }
    public CustomInvalidArgumentException(string exceptionMessage, Exception innerException) : base(exceptionMessage, innerException) { }
}
}
