//using System.Configuration;
//using System.Collections.Specialized;
//string sAttr;
//sAttr = ConfigurationManager.AppSettings.Get("Key0"); //Only one key-value
//Console.WriteLine("The value of Key0 is " + sAttr);
//NameValueCollection sAll;
//sAll = ConfigurationManager.AppSettings; //All keys-values
//foreach (string s in sAll.AllKeys)
//    Console.WriteLine("Key: " + s + " Value: " + sAll.Get(s));

//Console.ReadLine();

//EXAMPLE OF CONFIGURATION FILE IN C#




//CODING TRACKER

using CodingTrackerConsoleApp;
using CodingTrackerConsoleApp.Data;
using System.Linq.Expressions;

internal class Program {
    private static void Main(string[] args) {
        CodingRepository.Init();
        MainMenu();
    }
    private static void MainMenu() {
        Console.WriteLine("\nThis is a Coding Tracker Console App");
        Console.WriteLine("MAIN MENU");
        Console.WriteLine("---------------------------");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("0 - Close the application\n1 - Create a coding session\n2 - Delete coding session\n3 - Read coding sessions");
        try {
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput) {
                case 0:
                    Console.WriteLine("Goodbye");
                    Console.ReadLine();
                    break;
                case 1:
                    CreateCodingSession.CreateSession();
                    break;
                case 2:
                    new DeleteCodingSession();
                    break;
                case 3:
                    new ViewCodingSessions();
                    break;
                default:
                    throw new Exception("Please choose one of the options.");
            }
        }
        catch (Exception ex){
            Console.WriteLine($"Error in choice: {ex.Message}\nTry again\n");
            MainMenu();
        }
        
    }
}