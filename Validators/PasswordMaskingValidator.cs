using System.Text;

namespace MusicPlayerSystem_v1_Database.Validators
{
    class PasswordMaskingValidator
    {
        // Method to read a masked password
        public static string ReadMaskedPassword(string prompt, string regexPattern, string errorMessage)
        {
            string password;

            while (true)
            {
                Console.Write(prompt);
                password = MaskedInput(); // Call the method to read masked input

                if (System.Text.RegularExpressions.Regex.IsMatch(password, regexPattern))
                {
                    break; // Exit loop if password is valid
                }

                Console.WriteLine(errorMessage); // Show error message if invalid
            }

            return password;
        }

        // Method to handle masked input
        private static string MaskedInput()
        {
            StringBuilder input = new StringBuilder();
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true); // Read key without displaying it

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    input.Append(key.KeyChar); // Append character to input
                    Console.Write("*"); // Display asterisk
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0) // Ensure there's something to delete
                    {
                        input.Remove(input.Length - 1, 1); // Remove last character
                        Console.Write("\b \b"); // Move cursor back, overwrite with space, move back again
                    }
                }

            } while (key.Key != ConsoleKey.Enter); // Exit loop on Enter key

            Console.WriteLine(); // Move to the next line after Enter
            return input.ToString(); // Return the entered password
        }
    }
}