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

internal class Program {

    private static void Main(string[] args) {
        DatabaseManager.CreateDatabase();
    }
}