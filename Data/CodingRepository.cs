using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp.Data {
    public class CodingRepository {
        private string dbFileName = "codingdb.sqlite";
        private static SQLiteConnection conn;
        private static SQLiteCommand cmd;
        private string tableName = "CodingSessions";
        //static void Main(string[] args) {
        //    Init();
        //}
        public static void Init() {
            try {
                conn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                conn.Open();
                cmd = new SQLiteCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName}(id INTEGER PRIMARY KEY AUTOINCREMENT, author TEXT, book TEXT)";
                cmd.ExecuteNonQuery();
                Console.WriteLine("Connected");
            }
            catch (SQLiteException ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        }

    }
    
}
