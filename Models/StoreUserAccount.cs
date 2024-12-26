using Microsoft.Data.SqlClient;

namespace MusicPlayerSystem_v1_Database.Models
{
    public class StoreUserAccount
    {
        public static void RegisterUserToServer(string userId, string userName, string userEmail, string userPassword, string userMobileNumber)
        {
            using (SqlCommand command = new("INSERT INTO AccountCreator (userId, userName, userEmail, userPassword, userMobileNumber, registeredDate) VALUES (@userId, @userName, @userEmail, @userPassword, @userMobileNumber, @registeredDate)", DataBaseConnectionManager.dataBaseConnectionManager.Connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@userName", userName);
                command.Parameters.AddWithValue("@userEmail", userEmail);
                command.Parameters.AddWithValue("@userPassword", userPassword);
                command.Parameters.AddWithValue("@userMobileNumber", userMobileNumber);
                command.Parameters.AddWithValue("@registeredDate", DateTime.Now);
                command.ExecuteNonQuery();
            }
        }
    }
}