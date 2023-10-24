//using system.configuration;
//using system.collections.specialized;
//string sattr;
//sattr = configurationmanager.appsettings.get("key0"); //only one key-value
//console.writeline("the value of key0 is " + sattr);
//namevaluecollection sall;
//sall = configurationmanager.appsettings; //all keys-values
//foreach (string s in sall.allkeys)
//    console.writeline("key: " + s + " value: " + sall.get(s));

//console.readline();

//example of configuration file in c#

//coding tracker

using CodingTrackerConsoleApp;

internal class Program {

    private static void Main(string[] args) {
        DatabaseManager.CreateDatabase();
    }
}