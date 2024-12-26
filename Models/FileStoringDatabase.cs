// /*
// @title			Dulcet - Music Player
// @author			Harish B
// @createdOn		17-12-2024
// @modifiedOn		-
// @reviewedBy		-
// @reviewedOn		-
// */

// using System.Text.Json;

// namespace MusicPlayerSystem_v1_Database.Models
// {
//     public class FileStoringDatabase
//     {
//         private static List<User> s_retrievedUsersFromFile = [];
//         private static readonly string s_databaseFilePath = Program.databaseFilePath;
//         private static readonly string s_logFilePath = Program.logFilePath;
//         private static string s_existingLogMessages = "", s_fileContent = "";
//         static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
//         {
//             WriteIndented = true
//         };
//         public static List<User> RetrievedUsersFromFile()
//         {
//             return s_retrievedUsersFromFile;
//         }
//         public static List<User> LoadExisitingUsersFromFile()
//         {
//             if (File.Exists(s_databaseFilePath))
//             {
//                 s_existingLogMessages = File.ReadAllText(s_databaseFilePath);
//                 return s_retrievedUsersFromFile = JsonSerializer.Deserialize<List<User>>(s_existingLogMessages) ?? [];
//             }
//             else
//             {
//                 return s_retrievedUsersFromFile = [];
//             }
//         }
//         public static void UploadUsersToFile(List<User> existingUsers)
//         {
//             s_fileContent = JsonSerializer.Serialize(existingUsers, s_jsonSerializerOptions);
//             File.WriteAllText(s_databaseFilePath, s_fileContent);
//         }
//         public static void EntryLog(User user)
//         {
//             List<User> logDetailsList = LoadExisitingUsersFromFile();
//             logDetailsList.Add(user);
//             string logMessage = JsonSerializer.Serialize(logDetailsList, s_jsonSerializerOptions);
//             File.WriteAllText(s_logFilePath, logMessage);
//         }

//         public static void DisplayLogFile()
//         {
//             if (File.Exists(s_databaseFilePath))
//             {
//                 string logContent = File.ReadAllText(s_databaseFilePath);
//                 Console.WriteLine("Current Log File Contents:");
//                 Console.WriteLine(logContent);
//             }
//             else
//             {
//                 Console.WriteLine("Log file does not exist.");
//             }
//         }
//     }
// }
