/*
@title			Dulcet - Music Player
@author			Harish B
@createdOn		[ 17-12-2024, 20-12-2024 ]
@modifiedOn		-
@reviewedBy		-
@reviewedOn		-
*/

using MusicPlayerSystem_v1_Database.Validators;

namespace MusicPlayerSystem_v1_Database.Models
{
    public class User
    {
        public string UserId { get; set; }

        // [ValidatedUserInput(@"^(?!.(.).\\1{2})[a-zA-Z][a-zA-Z0-9_-]{3,15}$", ErrorMessage = "Invalid Name. Must be 3-30 alphabetic characters.")]
        public string Name { get; set; }

        // [ValidatedUserInput(@"^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$",ErrorMessage = "Invalid Email format.")]
        public string Email { get; set; }

        // [ValidatedUserInput(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}", ErrorMessage = "Invalid Password. Must meet the criteria.")]
        public string Password { get; set; }
        // [ValidatedUserInput(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}", ErrorMessage = "Invalid Confirmation.")]
        public string ConfirmPassword { get; set; }

        // [ValidatedUserInput(@"^[0-9]{10}$", ErrorMessage = "Invalid Phone Number. Must be 10 digits.")]
        public string MobileNumber { get; set; }
        public DateTime Timestamp { get; set; }

        public User(string userId, string name, string email, string password, string mobileNumber)
        {
            UserId = userId;
            Name = name;
            Email = email;
            Password = password;
            MobileNumber = mobileNumber;
            Timestamp = DateTime.Now;
        }

        public User()
        {
        }
    }
}