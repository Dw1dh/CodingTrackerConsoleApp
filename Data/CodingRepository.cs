using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp.Data {
    public class CodingRepository {
        private static string dbFileName = "codingdb.sqlite";
        private static SQLiteConnection conn;
        private static SQLiteCommand cmd;
        private static string tableName = "CodingSessions";
        
        public static void Init() {
            try {
                conn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                conn.Open();
                cmd = new SQLiteCommand();
                cmd.Connection = conn;
                Console.WriteLine("Connected");
                CreateTable();
            }
            catch (SQLiteException ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        }

        private static void CreateTable() {
            try {
                cmd.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName}(Id INTEGER PRIMARY KEY AUTOINCREMENT, StartTime TEXT NOT NULL, EndTime TEXT NOT NULL)";
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table was initialized");
            }
            catch (SQLiteException ex) {
                Console.WriteLine($"Error in creating a table: {ex.Message}");
            }

        }
        private void Create(DateTime StartTime, DateTime EndTime) {
            
        }

    }
    
}
