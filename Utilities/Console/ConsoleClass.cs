namespace MusicPlayerSystem_v1_Database.Utilities.Console
{
    public class ConsoleClass
    {
        public static void PrintLine(string Message)
        {
            System.Console.WriteLine(Message);
        }
        public static void Print(string Message)
        {
            System.Console.Write(Message);
        }
        public static string ReadLine()
        {
            return System.Console.ReadLine();
        }
    }
}