/*
@title			Dulcet - Music Player
@author			Harish B
@createdOn		10-12-2024
@modifiedOn		20-12-2024
@reviewedBy		-
@reviewedOn		-
*/
using static System.Console;
namespace MusicPlayerSystem_v1_Database
{
    class Welcome
    {
        public static string[] messages = [
            "*****************************************",
            "            Welcome to DULCET",
            "            Music Player app",
            "*****************************************",
            "            Let's Play Music!",
            "*****************************************"
        ];
        static void PrintMessageWithDelay(string message)
        {
            foreach (char letter in message)
            {
                ForegroundColor = ConsoleColor.DarkGreen;
                Write(letter);
                // Thread.Sleep(30);
            }
            WriteLine();
        }
        public static void WelcomePage()
        {
            foreach (string message in messages)
            {
                PrintMessageWithDelay(message);
                ForegroundColor = ConsoleColor.White;
            }
        }
        public static void Loading()
        {
            PrintMessageWithDelay(messages[0]);
        }
    }
}