/*
@title			Dulcet - Music Player
@author			Harish B
@createdOn		12-12-2024
@modifiedOn		23-12-2024
@reviewedBy		-
@reviewedOn		-
*/

using MusicPlayerSystem_v1_Database.Validators;
using MusicPlayerSystem_v1_Database.Logger;
using MusicPlayerSystem_v1_Database.Models;
using static System.Console;

namespace MusicPlayerSystem_v1_Database.Services
{
    public class AccountHandler
    {
        private readonly LoggerService _loggerService;
        public AccountHandler(LoggerService loggerService)
        {
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        }
        
        // Parent method in which Login logic occur inside that InputValidator.ValidatedUserInput method validate user with right credentials with 
        // proper usage of RegEx for Validation and retrieve the user in database and check with the user credentials
        public void LoginUser()
        {
            int maxAttempts = 3, userAttempts = 0;
            ForegroundColor = ConsoleColor.DarkCyan;
            // Using WriteLine method as a static method from Console class
            WriteLine("Welcome to Dulcet Account Login");
            ForegroundColor = ConsoleColor.White;

            while (userAttempts < maxAttempts)
            {
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

                bool isUserVerifiedFromDatabase = RetrieveUserAccount.FetchUserFromServer(email, password);
                if (isUserVerifiedFromDatabase)
                {
                    _loggerService.LogSuccessMessage($"{email} has been loggedin successfully");
                    SongManager songManager = new();
                    songManager.DisplaySongs();
                    songManager.LoadSongTrack();
                }
                else
                {
                    userAttempts++;
                    _loggerService.LogErrorMessage($"Incorrect credentials. Attempts remaining:  {maxAttempts - userAttempts}. Attempted by mail: {email}");
                }
            }
            _loggerService.LogErrorMessage("Too many failed attempts. Please try again after 3 seconds.");

            try
            {
                Thread.Sleep(3000);
            }
            catch (ThreadInterruptedException login_interrupted)
            {
                Thread.CurrentThread.Interrupt();
                _loggerService.LogErrorMessage($"Login interrupted for the user with wrong credentials {login_interrupted.Message}");
            }
        }
    }
}