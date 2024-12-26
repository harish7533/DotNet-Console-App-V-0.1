using Microsoft.Data.SqlClient;

namespace MusicPlayerSystem_v1_Database.Models
{
    public class RetrieveUserAccount
    {
        // Fetch the users from the database table AccountCreator and perform some validations like validating the user's email and email in database
        // Also validating the password from the database which is a hashed one with the user's password 
        public static bool FetchUserFromServer(string userEmail, string userPassword)
        {
            using SqlCommand command = new("SELECT userPassword FROM AccountCreator WHERE userEmail = @userEmail", DataBaseConnectionManager.dataBaseConnectionManager.Connection);
            command.Parameters.AddWithValue("@userEmail", userEmail);

            if (command.ExecuteScalar() is string hashedPassword)
            {
                return BCrypt.Net.BCrypt.Verify(userPassword, hashedPassword);
            }
            return false;
        }
    }
}