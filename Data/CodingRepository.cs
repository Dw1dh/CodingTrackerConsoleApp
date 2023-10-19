using CodingTrackerConsoleApp.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp.Data {
    public class CodingRepository {
        //private static string dbFileName = "coding db.sqlite";
        //private static SQLiteConnection conn;
        //private static SQLiteCommand cmd;
        //private static string tableName = "CodingSessions";
        private static List<CodingSession> codingSessions = new List<CodingSession>();

        public static void Init() {
            //try {
            //    conn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            //    conn.Open();
            //    cmd = new SQLiteCommand();
            //    cmd.Connection = conn;
            //    Console.WriteLine("Connected");
            //    CreateTable();
            //}
            //catch (SQLiteException ex) {
            //    Console.WriteLine($"Error: {ex.Message}");
            //}
            
        }

        private static void CreateTable() {
            try {
                Console.WriteLine("Table was created");
            }
            catch (SQLiteException ex) {
                Console.WriteLine($"Error in creating a table: {ex.Message}");
            }
            //try {
            //    cmd.CommandText = $"DROP TABLE IF EXISTS {dbFileName};";
            //    cmd.ExecuteNonQuery();
            //    cmd.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName}(Id INTEGER PRIMARY KEY AUTOINCREMENT, StartTime TEXT NOT NULL, EndTime TEXT NOT NULL, Duration TEXT NOT NULL)";
            //    cmd.ExecuteNonQuery();
            //    Console.WriteLine("Table was initialized");
            //}
            //catch (SQLiteException ex) {
            //    Console.WriteLine($"Error in creating a table: {ex.Message}");
            //}


        }
            public static void Create(CodingSession codingSession) {
            try {
                codingSessions.Add(codingSession);
            } catch(Exception ex) {
                Console.WriteLine($"Error in creating a new coding session: {ex.Message}");
            }
                
           
            //cmd.Parameters.AddWithValue(":StartTime", startTime.ToShortTimeString());
            //cmd.Parameters.AddWithValue(":EndTime", endTime.ToShortTimeString());
            //cmd.Parameters.AddWithValue(":Duration", duration.ToShortTimeString());

            //cmd.CommandText = $"INSERT INTO {tableName} (StartTime, EndTime, Duration) VALUES (:StartTime, :EndTime, :Duration)";
            //cmd.ExecuteNonQuery();
        }
        public static void Read() {
            try {
                foreach (CodingSession codingSession in codingSessions) {
                    Console.WriteLine($"ID: {codingSession.Id}");
                    Console.WriteLine($"Start Time: {codingSession.StartTime}");
                    Console.WriteLine($"End Time: {codingSession.EndTime}");
                    Console.WriteLine($"Duration: {codingSession.Duration}");

                }
                Program.MainMenu();
            } catch(Exception ex) {
                Console.WriteLine($"Error in reading coding sessions: {ex.Message}");
                Program.MainMenu();
            }
            
            //cmd.CommandText = $"SELECT * FROM {tableName}";
            //using (SQLiteDataReader rdr = cmd.ExecuteReader()) {
            //    while (rdr.Read()) {
            //        Console.WriteLine($"Start Time: {rdr["StartTime"]}");
            //        Console.WriteLine($"End Time: {rdr["EndTime"]}");
            //        Console.WriteLine($"Duration: {rdr["Duration"]}\n");

            //    }

            //}
        }
    }
    
}
