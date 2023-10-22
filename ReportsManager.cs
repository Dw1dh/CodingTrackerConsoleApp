using CodingTrackerConsoleApp.Model;
using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp {
    public class ReportsManager {
        public class Goal() {
            public string Name {
                get; set;
            }
            public int Time {
            get; set; }
        };
        public static List<Goal> goals = new();
        private static SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.conn); 
        public static void ReportByDay() {
            List<CodingSession> codingSessions = new List<CodingSession>();
            cmd.CommandText = "SELECT * FROM CodingSessions ORDER BY Day";
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

        public static void ReportByMonth() {
            List<CodingSession> codingSessions = new List<CodingSession>();
            cmd.CommandText = "SELECT * FROM CodingSessions ORDER BY Month";
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

        public static void ReportByYear() {
            List<CodingSession> codingSessions = new List<CodingSession>();
            cmd.CommandText = "SELECT * FROM CodingSessions ORDER BY Year";
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

        public static void ReportByDuration() {
            List<CodingSession> codingSessions = new List<CodingSession>();
            List<CodingSession> orderedCodingSessions = new List<CodingSession>();
            List<DateTime> dateTimeList = new List<DateTime>();
            cmd.CommandText = "SELECT * FROM CodingSessions";
            var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                if (rdr.HasRows) {
                    codingSessions.Add(new CodingSession {
                        Id = rdr.GetInt32(0),
                        Duration = rdr.GetString(1),
                        Day = rdr.GetInt32(2),
                        Month = rdr.GetInt32(3),
                        Year = rdr.GetInt32(4),
                    });
                    dateTimeList.Add(DateTime.Parse(rdr.GetString(1)));
                }
            }

            rdr.Close();
            List<DateTime> orderedDateTimeList = dateTimeList.OrderBy(x => x.TimeOfDay).ToList();
            for (int i = 0; i < codingSessions.Count; i++) {
                orderedCodingSessions = codingSessions.OrderBy(o => o.Duration == orderedDateTimeList[i].ToShortTimeString()).ToList();
            }
            ConsoleTableBuilder.From(orderedCodingSessions)
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
        public static void ReportTotal() {
            int totalHours = 0;
            int totalMinutes = 0;
            DateTime duration;
            DatabaseManager.Read();
            foreach (CodingSession codingSession in DatabaseManager.codingSessions) {
                duration = new();
                duration = DateTime.Parse(codingSession.Duration);
                totalHours += duration.Hour;
                totalMinutes += duration.Minute;
            }
            Console.WriteLine($"The total duration time: {totalHours} hours, {totalMinutes} minutes");
        }
        public static void ReportGoals(){
            Console.WriteLine("1 - View goals\n2 - Make a new goal");
            int userInput = Convert.ToInt32( Console.ReadLine() );
            switch (userInput) {
                case 1:
                    if (goals.Count != 0) {
                        foreach (Goal goal1 in goals) {
                            Console.WriteLine($"The goal: {goal1.Name}");
                            Console.WriteLine($"The time to achieve: {goal1.Time} hours");
                        }
                    } else {
                        Console.WriteLine("You don't have any goals");
                    }
                    
                    break;

                case 2:
                    string goalName = InputManager.GetNameGoalInput();
                    int progress = InputManager.GetTimeGoalInput();
                    Goal goal = new Goal() { Name = goalName, Time = progress };
                    goals.Add(goal);
                    break;
            }
            
        }
    }
}
