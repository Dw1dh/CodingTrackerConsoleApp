﻿using CodingTrackerConsoleApp.Model;
using ConsoleTableExt;
using System.Data;
using System.Data.SQLite;

namespace CodingTrackerConsoleApp {

    internal class DatabaseManager {
        private static SQLiteCommand cmd;

        public static void CreateDatabase() {
            try {
                var conn = new SQLiteConnection("Data Source=codingBase.sqlite;Version=3; FailIfMissing=False");
                Console.WriteLine("Connected");
                conn.Open();
                cmd = new SQLiteCommand(conn);
                //cmd.CommandText = " DROP Table 'CodingSessions'";

                //cmd.ExecuteNonQuery();

                cmd.CommandText = $"CREATE TABLE IF NOT EXISTS CodingSessions(Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, Duration TEXT NOT NULL,Date TEXT NOT NULL)";
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table was created");
                Interface.MainMenu();
            }
            catch (SQLiteException ex) {
                Console.WriteLine($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
            }
        }
        public static void Delete(int id) {
            cmd.CommandText = $"DELETE FROM CodingSessions WHERE Id = {id}";
            cmd.ExecuteNonQuery();
            Console.WriteLine("Deleting was successful");
        }
        public static void DeleteAll() {
            cmd.CommandText = "DELETE FROM CodingSessions";
            cmd.ExecuteNonQuery();
            Console.WriteLine("Deleting all was successful");
        }
        public static void Create(CodingSession codingSession) {
            cmd.CommandText = "INSERT INTO CodingSessions(Duration,Date) VALUES(:Duration,:Date)";
            cmd.Parameters.AddWithValue(":Duration",
                codingSession.Duration.ToShortTimeString());
            cmd.Parameters.AddWithValue(":Date",
                codingSession.Date.ToShortDateString());

            cmd.ExecuteNonQuery();
        }
        public static void Read() {

            List<CodingSession> codingSessions = new List<CodingSession>();
            cmd.CommandText = "SELECT * FROM CodingSessions";
                    var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                if (rdr.HasRows) {
                    codingSessions.Add(new CodingSession {
                        Id = rdr.GetInt32(0),
                        Duration = TimeOnly.Parse(rdr.GetString(1)),
                        Date = DateOnly.Parse(rdr.GetString(2))
                    });
                       
                    }
                    
                }
            rdr.Close();
            ConsoleTableBuilder.From(codingSessions)
                       .WithCharMapDefinition(CharMapDefinition.FramePipDefinition)
                       .WithCharMapDefinition(
                           CharMapDefinition.FramePipDefinition,
                           new Dictionary<HeaderCharMapPositions, char> {
                        {HeaderCharMapPositions.TopLeft, '╒' },
                        {HeaderCharMapPositions.TopCenter, '═' },
                        {HeaderCharMapPositions.TopRight, '╕' },
                        {HeaderCharMapPositions.BottomLeft, '╞' },
                        {HeaderCharMapPositions.BottomCenter, '╤' },
                        {HeaderCharMapPositions.BottomRight, '╡' },
                        {HeaderCharMapPositions.BorderTop, '═' },
                        {HeaderCharMapPositions.BorderRight, '│' },
                        {HeaderCharMapPositions.BorderBottom, '═' },
                        {HeaderCharMapPositions.BorderLeft, '│' },
                        {HeaderCharMapPositions.Divider, ' ' },
                           })
                       .ExportAndWriteLine();
        }
        public static bool CheckExistance(int id) {
            cmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM CodingSessions WHERE Id = {id})";
            int checkQuery = Convert.ToInt32(cmd.ExecuteScalar());

            if (checkQuery == 0) {
                Console.WriteLine($"\n\nRecord with Id {id} doesn't exist.\n\n");
                return false;
            } else {
                return true;
            }
        }
        public static void Update(DateOnly date, TimeOnly duration,int id) {
            cmd.CommandText = $"UPDATE CodingSessions SET Date = {date.ToLongDateString}, Duration = {duration.ToShortTimeString}, Id = {id}";
            cmd.ExecuteNonQuery();
            Console.WriteLine("Updating was successfully done");
        }
    
    }
    
    }
