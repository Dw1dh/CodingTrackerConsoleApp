using CodingTrackerConsoleApp.Model;
using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp {
    public class ReportsManager {
        public static List<Goal>? goals;
        private static SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.conn);
        public static void ReportByDay() {
            DatabaseManager.codingSessions = new List<CodingSession>();
            cmd.CommandText = "SELECT * FROM CodingSessions ORDER BY Day";
            var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                if (rdr.HasRows) {
                    DatabaseManager.codingSessions.Add(new CodingSession {
                        Id = rdr.GetInt32(0),
                        Duration = rdr.GetString(1),
                        Day = rdr.GetInt32(2),
                        Month = rdr.GetInt32(3),
                        Year = rdr.GetInt32(4)
                    });
                }
            }
            rdr.Close();
            DatabaseManager.Show();
        }

        public static void ReportByMonth() {
            DatabaseManager.codingSessions = new List<CodingSession>();
            cmd.CommandText = "SELECT * FROM CodingSessions ORDER BY Month";
            var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                if (rdr.HasRows) {
                    DatabaseManager.codingSessions.Add(new CodingSession {
                        Id = rdr.GetInt32(0),
                        Duration = rdr.GetString(1),
                        Day = rdr.GetInt32(2),
                        Month = rdr.GetInt32(3),
                        Year = rdr.GetInt32(4)
                    });
                }
            }
            rdr.Close();
            DatabaseManager.Show();
        }

        public static void ReportByYear() {
            DatabaseManager.codingSessions = new List<CodingSession>();
            cmd.CommandText = "SELECT * FROM CodingSessions ORDER BY Year";
            var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                if (rdr.HasRows) {
                    DatabaseManager.codingSessions.Add(new CodingSession {
                        Id = rdr.GetInt32(0),
                        Duration = rdr.GetString(1),
                        Day = rdr.GetInt32(2),
                        Month = rdr.GetInt32(3),
                        Year = rdr.GetInt32(4)
                    });
                }
            }
            rdr.Close();

            DatabaseManager.Show();
        }

        public static void ReportByDuration() {
            DatabaseManager.codingSessions = new List<CodingSession>();
            List<CodingSession> orderedCodingSessions = new List<CodingSession>();
            List<DateTime> dateTimeList = new List<DateTime>();
            cmd.CommandText = "SELECT * FROM CodingSessions";
            var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                if (rdr.HasRows) {
                    DatabaseManager.codingSessions.Add(new CodingSession {
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
            for (int i = 0; i < DatabaseManager.codingSessions.Count; i++) {
                orderedCodingSessions = DatabaseManager.codingSessions.OrderBy(o => o.Duration == orderedDateTimeList[i].ToShortTimeString()).ToList();
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
            if (DatabaseManager.CheckGoalsExistance()) {
                GoalRead();
                
                foreach(Goal goal in goals) {
                    int currentTime = goal.Time - totalHours;
                    
                    if (currentTime < 0) {
                        Console.WriteLine($"You've reached the goal: {goal.Name}, by achiving the {goal.Time} hours of coding");
                    } else {
                        Console.WriteLine($"You've left {currentTime} hours to achive goal: {goal.Name}");
                        Console.WriteLine($"You will need to coding {currentTime / 7} hours per day to achieve this goal in the next week");
                    }


                }
            }
        }
            public static void ReportGoals() {
            Console.WriteLine("0 - Back to main menu\n1 - View goals\n2 - Make a new goal\n3 - Delete a goal");
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput) {
                case 0: 
                    Interface.MainMenu();
                    break;
                case 1:
                    GoalRead();
                    GoalShow(); 
                    break;

                case 2:
                    string goalName = InputManager.GetNameGoalInput();
                    int time = InputManager.GetTimeGoalInput();
                    //Goal goal = new Goal() { Name = goalName, Time = progress };
                    cmd.CommandText = "INSERT INTO Goals(Name,Time) VALUES(:name,:time)";
                    cmd.Parameters.AddWithValue(":name", goalName);
                    cmd.Parameters.AddWithValue(":time", time);
                    cmd.ExecuteNonQuery();
                    //goals.Add(goal);
                    break;

                case 3:
                    GoalRead();
                    GoalShow();
                       
                    int id = InputManager.GetIdInput();
                    if (DatabaseManager.CheckExistance(id, "Goals")) {
                        cmd.CommandText = $"DELETE FROM Goals WHERE Id = {id}";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Deleting was successful");

                    } else {
                        Console.WriteLine($"\nRecord with Id {id} doesn't exist.\n");
                    }

                    break;
                   


                        default:
                    Console.WriteLine("Choose something from the list");
                    ReportGoals();
                    break;
                        }

        }

        private static void GoalShow() {
            ConsoleTableBuilder.From(goals)
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

        private static void GoalRead() {
            goals = new List<Goal>();
            cmd.CommandText = "SELECT * FROM Goals";
            var rdr = cmd.ExecuteReader();


            if (rdr.HasRows) {
                while (rdr.Read()) {
                    goals.Add(new Goal {
                        Id = rdr.GetInt32(0),
                        Name = rdr.GetString(1),
                        Time = rdr.GetInt32(2)
                    });
                }
            } else {
                Console.WriteLine("No goals");
            }
            rdr.Close();
        }
    }
        }
        
        
    


