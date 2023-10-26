using CodingTrackerConsoleApp.Model;
using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp {
    public static class CodingSessionsManager {
        public static List<CodingSession> codingSessions = new();
        private static SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.conn);
        
        /// <summary>
        /// Initializing a coding sessions table
        /// </summary>
        public static void Init() {
            try {

                
                cmd.CommandText = $"CREATE TABLE IF NOT EXISTS CodingSessions(Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, Duration TEXT NOT NULL,Day INTEGER NOT NULL, Month INTEGER NOT NULL, Year INTEGER NOT NULL)";
                cmd.ExecuteNonQuery();
                //GoalManager.CreateGoalDatabase();
                Console.WriteLine("Table CodingSessions was created");
            }
            catch (SQLiteException ex) {
                Console.WriteLine($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
            }
        }

        /// <summary>
        /// Create a record
        /// </summary>
        public static void Create() {
            DateTime startTime = new();
            DateTime endTime = new();
            DateTime date;
            int day = new();
            int month = new();
            int year = new();
            Console.WriteLine("0 - Back to main menu\n1 - Enter a time\n2 - Start stopwatch");
            int userInput = InputManager.GetIntUserInput();
            switch (userInput) {
                case 0:
                    Interface.MainMenu();
                    break;
                    case 1:
                    Console.WriteLine("Start");
                    startTime = InputManager.GetTimeInput();
                    Console.WriteLine("End");
                    endTime = InputManager.GetTimeInput();
                    Console.WriteLine("Date");
                    date = InputManager.GetDateInput();
                    day = date.Day;
                    month = date.Month;
                    year = date.Year;

                    break;
                case 2:
                    startTime = StopWatchStart();
                    endTime = StopWatchEnd();
                    date = DateTime.Now;
                    break;
            }
            
            string duration = MakeDuration(startTime, endTime).ToShortTimeString();
            cmd.CommandText = $"INSERT INTO CodingSessions(Duration, Day, Month, Year) VALUES(:duration, :day, :month, :year)";
            cmd.Parameters.AddWithValue(":day", day);
            cmd.Parameters.AddWithValue(":month", month);
            cmd.Parameters.AddWithValue(":year", year);
            cmd.Parameters.AddWithValue(":duration", duration);
            cmd.ExecuteNonQuery();
        }

        
        /// <summary>
        /// Update a record
        /// </summary>
        public static void Update() {
            Console.WriteLine("Id");
            int id = InputManager.GetIntUserInput();
            if (DatabaseManager.CheckIdExistance(id, "CodingSessions")) {
                DateTime date = InputManager.GetDateInput();
                int day = date.Day;
                int month = date.Month;
                int year = date.Year;
                string duration = InputManager.GetTimeInput().ToShortTimeString();

                cmd.CommandText = $"UPDATE CodingSessions SET Duration = :duration, Day = :day, Month = :month, Year = :year WHERE Id = :id";
                cmd.Parameters.AddWithValue(":id", id);
                cmd.Parameters.AddWithValue(":day", day);
                cmd.Parameters.AddWithValue(":month", month);
                cmd.Parameters.AddWithValue(":year", year);
                cmd.Parameters.AddWithValue(":duration", duration);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Updating was successfully done");
            } else {
                Console.WriteLine($"\nRecord with Id {id} doesn't exist.\n");
            }
        }


        /// <summary>
        /// Read records
        /// </summary>
        public static void Read() {
            codingSessions = new List<CodingSession>();
            cmd.CommandText = "SELECT * FROM CodingSessions";
            var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                if (rdr.HasRows) {
                    codingSessions.Add(new CodingSession {
                        Id = rdr.GetInt32(0),
                        Duration = rdr.GetString(1),
                        Day = rdr.GetInt32(2),
                        Month = rdr.GetInt32(3),
                        Year = rdr.GetInt32(4)
                    });
                } else {
                    Console.WriteLine("No records");
                }
            }
            rdr.Close();
        }

        /// <summary>
        /// Show records
        /// </summary>
        public static void Show() {
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

        /// <summary>
        /// Delete records
        /// </summary>
        public static void Delete() {
            Read();
            Show();
            Console.WriteLine($"1 - Delete all goals\n2 - Delete a specific goal");
            int userInput = InputManager.GetIntUserInput();
            switch (userInput) {
                case 1:
                    Console.WriteLine("Id");
                    int id = InputManager.GetIntUserInput();
                    if (DatabaseManager.CheckIdExistance(id, "CodingSessions")) {
                        cmd.CommandText = $"DELETE FROM CodingSessions WHERE Id = {id}";
                        cmd.ExecuteNonQuery();
                    } else {
                        Console.WriteLine($"\nRecord with Id {id} doesn't exist.\n");
                    }
                    break;

                case 2:
                    DatabaseManager.DeleteAll("CodingSessions");
                    break;
            }
        }


        /// <summary>
        /// Make duration method
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DateTime MakeDuration(DateTime startTime, DateTime endTime) {
            DateTime duration = endTime.AddHours(-startTime.Hour).AddMinutes(-startTime.Minute);
            return duration;
        }
        
        
        /// <summary>
        /// StopWatch start value 
        /// </summary>
        /// <returns></returns>
        public static DateTime StopWatchStart() {
            Console.WriteLine("Enter any key to begin");
            Console.ReadLine();
            DateTime startTime = DateTime.Now;
            Console.WriteLine($"StartTime:{startTime}");
            return startTime;

        }

        /// <summary>
        /// StopWatch end value
        /// </summary>
        /// <returns></returns>
        public static DateTime StopWatchEnd() {
            Console.WriteLine("Enter any key to stop a stopwatch");
            Console.ReadLine();
            DateTime endTime = DateTime.Now;
            Console.WriteLine($"EndTime:{endTime}");
            return endTime;


        }

    }
}
