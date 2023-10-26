using CodingTrackerConsoleApp.Model;
using ConsoleTableExt;
using System.Data.SQLite;

namespace CodingTrackerConsoleApp {

    public static class GoalManager {
        public static List<Goal> goals = new();
        private static SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.conn);

        /// <summary>
        /// Initializing a goal manager table
        /// </summary>
        public static void Init() {
            //cmd.CommandText = " DROP Table 'Goals'";
            //cmd.ExecuteNonQuery();
            cmd.CommandText = $"CREATE TABLE IF NOT EXISTS Goals(Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, Name TEXT NOT NULL, Time INTEGER NOT NULL)";
            cmd.ExecuteNonQuery();
        }


        /// <summary>
        /// Show goals
        /// </summary>
        public static void Show() {
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

        /// <summary>
        /// Read goals
        /// </summary>
        public static void Read() {
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

        /// <summary>
        /// Create a goal
        /// </summary>
        public static void Create() {
            Console.WriteLine("Name");
            string name = InputManager.GetStringUserInput();
            Console.WriteLine("Amount of hours to achieve");
            int time = InputManager.GetIntUserInput();
            cmd.CommandText = $"INSERT INTO Goals(Name, Time) VALUES(:name, :time)";
            cmd.Parameters.AddWithValue(":name", name);
            cmd.Parameters.AddWithValue(":time", time);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Update a goal
        /// </summary>

        public static void Update() {
            Read();
            Console.WriteLine("Id");
            int id = InputManager.GetIntUserInput();
            if (DatabaseManager.CheckIdExistance(id, "Goals")) {
                string name = InputManager.GetStringUserInput();
                int time = InputManager.GetIntUserInput();

                cmd.CommandText = $"UPDATE Goals(Name, Time) SET Name = :name, Time = :time";
                cmd.Parameters.AddWithValue(":name", name);
                cmd.Parameters.AddWithValue(":time", time);
                cmd.ExecuteNonQuery();
            } else {
                Console.WriteLine($"\nRecord with Id {id} doesn't exist.\n");
            }
        }

        /// <summary>
        /// Delete goals
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
                    if (DatabaseManager.CheckIdExistance(id, "Goals")) {
                        cmd.CommandText = $"DELETE FROM Goals WHERE Id = {id}";
                        cmd.ExecuteNonQuery();
                    } else {
                        Console.WriteLine($"\nRecord with Id {id} doesn't exist.\n");
                    }
                    break;

                case 2:
                    DatabaseManager.DeleteAll("Goals");
                    break;
            }
           
        }

        /// <summary>
        /// Check goals existence 
        /// </summary>
        /// <returns></returns>
        public static void RemainingHours() {
            int totalHours = 0;
            DateTime duration;
            CodingSessionsManager.Read();
            foreach (CodingSession codingSession in CodingSessionsManager.codingSessions) {
                duration = DateTime.Parse(codingSession.Duration);
                totalHours += duration.Hour;
            }
            Read();
            if (DatabaseManager.CheckExistance("Goals")) {
                foreach (Goal goal in goals) {
                    if (totalHours >= goal.Time) {
                        Console.WriteLine($"You've achieved a goal {goal.Name}");
                    } else {
                        int leftTime = goal.Time - totalHours;
                        Console.WriteLine($"You need {leftTime} to achieve a goal:{goal.Name}");
                    }
                }

            }
        }
    }
}