using System.Text.RegularExpressions;
using static System.Console;

namespace MusicPlayerSystem_v1_Database.Validators
{
    public class InputValidator()
    {
        // Validating the user's inputs with prompt and returns if there any error or else the input will be returned to the ValidatedUserInput
        public static string ValidatedUserInput(string prompt, string regex, string errorMessage)
        {
            string input;
            while (true)
            {
                ForegroundColor = ConsoleColor.DarkMagenta;
                // Using Write method as a static method from Console class
                Write(prompt);
                ForegroundColor = ConsoleColor.White;
                // Using ReadLine method as a static method from Console class
                input = ReadLine();
                if (input != null && Regex.IsMatch(input, regex))
                {
                    break;
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    // Using WriteLine method as a static method from Console class
                    WriteLine(errorMessage);
                    ForegroundColor = ConsoleColor.White;
                }
            }
            return input;
        }
    }
}






// using System.ComponentModel.DataAnnotations;
// using System.Text.RegularExpressions;

// namespace MusicPlayerSystem_v1_Database.Validators
// {
//     class ValidatedUserInput : ValidationAttribute
//     {
//         private readonly string _pattern;
//         public ValidatedUserInput(string pattern)
//         {
//             _pattern = pattern;
//         }
//         protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//         {
//             if(value is string input)
//             {
//                 if(Regex.IsMatch(input, _pattern))
//                 {
//                     return ValidationResult.Success;
//                 }
//                 else
//                 {
//                     return new ValidationResult(ErrorMessage ?? "Input doesn't match the required format");
//                 }
//             }
//             // return base.IsValid(value, validationContext);
//             return new ValidationResult("Invalid input type");
//         }
//     }
// }