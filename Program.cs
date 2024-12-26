/*
@title			Dulcet - Music Player
@author			Harish B
@createdOn		10-12-2024
@modifiedOn		[ 12-12-2024, 18-12-2024, 20-12-2024, ]
@reviewedBy		Sabapathi
@reviewedOn		[ 19-12-2024, 26-12-2024 ] 

@reviewInputs on 19-12-2024
* Implement ADO .NET, Custom Logger, User choice validation if user gives except 1, 2, 3 it should be validated.
* Validations should be in SOC not in same assembly.

@reviewInputs on 19-12-2014 had been completed on 26-12-2024

@reviewInputs on 26-12-2024
* MobileNumber validation should starts with 6-9 not with 1.
* Search Method should also include if user gives wrong song names with one letter missing it should search for a relevant song.
*/

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using MusicPlayerSystem_v1_Database.Services;
using MusicPlayerSystem_v1_Database.Logger;
using Microsoft.Extensions.Configuration;
using MusicPlayerSystem_v1_Database.Models;
using MusicPlayerSystem_v1_Database.Utilities.Interface;
using static System.Console;

namespace MusicPlayerSystem_v1_Database
{
    class Program
    {
        // Initializing the Logger file path to store application logs locally
        public static string logFilePath = "";

        // Loading the Application with a main Use-case method
        public static void LoadApplication(AccountCreator accountCreator, AccountHandler accountHandler)
        {
            List<byte> validChoices = [1, 2, 3];
            byte userChoice;

            while (true)
            {
                WriteLine("Enter 1 to Register\nEnter 2 to Login\nEnter 3 to Exit");
                if (byte.TryParse(ReadKey(true).KeyChar.ToString(), out userChoice))
                {
                    if (validChoices.Contains(userChoice))
                    {
                        break;
                    }
                }
                WriteLine("Invalid Number: Use numbers of range from 1 to 3");
                Welcome.Loading();
            }

            switch (userChoice)
            {
                case 1:
                    accountCreator.RegisterUser();
                    break;
                case 2:
                    accountHandler.LoginUser();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }

        // Application main method, running in async type with a Task method
        static async Task Main(string[] args)
        {

            // Configuring the appsettings.json inside the ConfigurationBuilder
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Declaring log file path from appsettings.json
            logFilePath = configuration["LoggerFilePath"];

            // Creating common Host for my application and registering the services
            // Configured the IConfiguration as a Singleton object and will be accessible in every location of my project
            // you can add args only if you're passing any arguments from console otherwise no need      
            var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddTransient<LoggerService>();
                services.AddTransient<AccountCreator>();
                services.AddTransient<AccountHandler>();
                services.AddSingleton<IConfiguration>(configuration);
                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.AddProvider(new FileLoggerProvider(logFilePath));
                });
            })
            .Build();

            // Getting the required service for the service functionalities for handling users
            var accountCreator = host.Services.GetRequiredService<AccountCreator>();
            var accountHandler = host.Services.GetRequiredService<AccountHandler>();
            var _loggerService = host.Services.GetRequiredService<LoggerService>();

            // Initializing the DataBaseConnectionManager
            DataBaseConnectionManager.dataBaseConnectionManager.Initialize(configuration);

            // Method = WelcomePage() -> Welcome the customer with small animation and site name
            Welcome.WelcomePage();

            // Custom logger function implemented
            ICustomLogger logger = new AnotherLogger();
            logger.WriteInformation("Application started successfully");

            // Implemented logs from ILogger
            _loggerService.LogSuccessMessage("Application started successfully");

            // Application switch case handling the user choices
            LoadApplication(accountCreator, accountHandler);

            await host.RunAsync();

            // CLosing the DataBaseConnectionManager when the app gets terminated with await usage
            DataBaseConnectionManager.dataBaseConnectionManager.CloseConnection();

            _loggerService.LogSuccessMessage("Application terminated and DataBaseConnectionManager has also been terminated successfully");
        }
    }
}


// Instantiating logger services to the application
// var loggerFactory = LoggerFactory.Create(builder => {
//     builder.AddProvider(new FileLoggerProvider(logFilePath));
// });
// var logger = loggerFactory.CreateLogger<Program>();

// Hosting and Binding Services concept need to explore


// using var client = new HttpClient();
// Console.WriteLine(client.ToString());
// var response = await client.GetAsync("https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using");

// if (response.IsSuccessStatusCode)
// {
//     var result = await response.Content.ReadAsStringAsync();
//     Console.WriteLine($"Response: {response}");
//     Console.WriteLine($"Result: {result}");
// }
// else
// {
//     Console.WriteLine($"Error: {response.StatusCode}");
// }