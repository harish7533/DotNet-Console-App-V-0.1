/*
@title			Dulcet - Music Player
@author			Harish B
@createdOn		10-12-2024
@modifiedOn		[ 18-12-2024, 20-12-2024 ]
@reviewedBy		-
@reviewedOn		-
*/

using System.Text.RegularExpressions;
using MusicPlayerSystem_v1_Database.Logger;
using MusicPlayerSystem_v1_Database.Models;
using MusicPlayerSystem_v1_Database.Validators;
using static MusicPlayerSystem_v1_Database.Utilities.Console.ConsoleClass;

namespace MusicPlayerSystem_v1_Database.Services
{

    // Simplified constructor direct initialization
    // class User(string name, string email, string password, string mobileNumber) {
    //     public string Name { get; set; } = name;
    //     public string Email { get; set; } = email;
    //     public string Password { get; set; } = password;
    //     public string MobileNumber { get; set; } = mobileNumber;
    // }

    // Instead of using comment lines we can use like summary tag to show details which is a XML fomrat

    /*<summary>
    User class has been created for initalizing the data members of Account with a constructor
    </summary>*/
    class AccountCreator
    {
        // Listing my collection with a control over in same class alone
        private static readonly List<User> s_users = [];
        private static readonly List<User> s_retrievedUsersFromFile = [];

        // Instantiating AccountHandler class readonly because it is without a set accessor
        private readonly LoggerService _loggerService;
        private readonly AccountHandler accountHandler;

        // Password hashing logic
        string HashPassword(string password)
        {
            int saltRounds = 10;
            string salt = BCrypt.Net.BCrypt.GenerateSalt(saltRounds);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
        public static string hashedpassword = "";
        public static List<User> GetUsers()
        {
            return s_users;
        }
        public AccountCreator(LoggerService loggerService, AccountHandler parameteraccountHandler)
        {
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
            accountHandler = parameteraccountHandler ?? throw new ArgumentNullException(nameof(parameteraccountHandler));
        }

        public AccountCreator() { }

        // Parent method in which Register logic occur inside that InputValidator.ValidatedUserInput method validate user with right credentials with 
        // proper usage of RegEx for Validation and store the user in database
        public void RegisterUser()
        {

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Welcome to Dulcet Account Registration");
            Console.ForegroundColor = ConsoleColor.White;

            const string characterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            Random random = new();
            string userId = "";
            int length = 8;

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(characterSet.Length);
                char randomChar = characterSet[index];
                userId += randomChar;
            }

            string name = InputValidator.ValidatedUserInput(
                    "Enter your Name (Only letters allowed): ",
                    // "^[A-Za-z ]{3,30}$",
                    "^(?!.(.).\\1{2})[a-zA-Z][a-zA-Z0-9_-]{3,15}$",
                    "Invalid Name. Must be 3-30 alphabetic characters."
                   );

            string email = InputValidator.ValidatedUserInput(
                    "Enter your Email: ",
                    "^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$",
                    "Invalid Email format."
                    );

            string password = PasswordMaskingValidator.ReadMaskedPassword(
                    "Enter your Password (8-12 characters, must include one uppercase, one special character, one lowercase, and one number): ",
                    "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
                    "Invalid Password. Must meet the criteria."
                    );
            string confirmPassword;
            do
            {
                confirmPassword = PasswordMaskingValidator.ReadMaskedPassword(
                       "Confirm your Password: ",
                       ".*",
                       "Invalid Confirmation."
                       );
                if (!confirmPassword.Equals(password))
                {
                    Console.WriteLine("Passwords do not match. Please try again.");
                }
            } while (!confirmPassword.Equals(password));

            string userMobileNumber = InputValidator.ValidatedUserInput(
                    "Enter your Phone Number (10 digits): ",
                    "^[0-9]{10}$",
                    "Invalid Phone Number. Must be 10 digits."
                    );

            hashedpassword = HashPassword(password);

            // Store the users in the database tables
            StoreUserAccount.RegisterUserToServer(userId, name, email, hashedpassword, userMobileNumber);
            // Logs the user regsitered in the database
            _loggerService.LogSuccessMessage($"User: {name} has been registered successfully!");

            PrintLine($"\n\nKindly Login our app for further process.");
            // After user registered calling the LoginUser method from AccountHandler class to perform the login also
            accountHandler.LoginUser();
        }
    }
}

// string name = GetValidatedInput("Enter your Name (Only letters allowed): ", nameof(User.Name));
// string email = GetValidatedInput("Enter your Name (Only letters allowed): ", nameof(User.Email));
// string password = GetValidatedInput("Enter your Name (Only letters allowed): ", nameof(User.Password));
// string userMobileNumber = GetValidatedInput("Enter your Name (Only letters allowed): ", nameof(User.MobileNumber));
// string confirmPassword = GetValidatedInput("Confirm your Password: ", nameof(User.ConfirmPassword));


// static string GetValidatedInput(string prompt, string propertyName)
// {
//     string input;
//     List<ValidationResult> validationResults = new List<ValidationResult>();
//     var context = new ValidationContext(new User());

//     while (true)
//     {
//         Console.Write(prompt);
//         input = Console.ReadLine();

//         // Set the property value to validate
//         if (propertyName == nameof(User.Name))
//             ((User)context.ObjectInstance).Name = input;
//         else if (propertyName == nameof(User.Email))
//             ((User)context.ObjectInstance).Email = input;
//         else if (propertyName == nameof(User.Password))
//             ((User)context.ObjectInstance).Password = input;
//         else if (propertyName == nameof(User.MobileNumber))
//             ((User)context.ObjectInstance).MobileNumber = input;

//         // Validate the object property using reflection
//         bool isValid = Validator.TryValidateProperty(
//             input,
//             new ValidationContext(context.ObjectInstance) { MemberName = propertyName },
//             validationResults);

//         if (isValid)
//             break; // Valid input, exit loop

//         // Display validation errors
//         foreach (var validationResult in validationResults)
//         {
//             Console.WriteLine(validationResult.ErrorMessage);
//         }

//         validationResults.Clear(); // Clear previous results for next iteration
//     }

//     return input;
// }