// /*
// @title			Dulcet - Music Player
// @author			Harish B
// @createdOn		17-12-2024
// @modifiedOn		-
// @reviewedBy		-
// @reviewedOn		-
// */

// using MusicPlayerSystem_v1_Database.Models;

// namespace MusicPlayerSystem_v1_Database.Services
// {
//     public class UserService
//     {
//         public static void MergeUsers(List<User> newUsers)
//         {
//             List<User> existingUsers = FileStoringDatabase.LoadExisitingUsersFromFile();
//             foreach (var newUser in newUsers)
//             {
//                 if (!existingUsers.Any(existingUser => existingUser.Email == newUser.Email))
//                 {
//                     existingUsers.Add(newUser);
//                 }
//                 else
//                     existingUsers.Add(newUser);
//             }
//             FileStoringDatabase.UploadUsersToFile(existingUsers);
//         }
//     }
// }