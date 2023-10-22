using CodingTrackerConsoleApp.Model;
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

                cmd.CommandText = $"CREATE TABLE IF NOT EXISTS CodingSessions(Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, Duration TEXT NOT NULL,Day INTEGER NOT NULL, Month INTEGER NOT NULL, Year INTEGER NOT NULL)";
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table was created");
                Interface.MainMenu();
            }
            catch (SQLiteException ex) {
                Console.WriteLine($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
            }
        }
        public static void Delete() {
            Read();
            int id = InputManager.GetIdInput();
            cmd.CommandText = $"DELETE FROM CodingSessions WHERE Id = {id}";
            cmd.ExecuteNonQuery();
            Console.WriteLine("Deleting was successful");
        }
        public static void DeleteAll() {
            cmd.CommandText = "DELETE FROM CodingSessions";
            cmd.ExecuteNonQuery();
            Console.WriteLine("Deleting all was successful");
        }
        public static void Create() {
            DateTime date = InputManager.GetDateInput();
            int day = date.Day;
            int month = date.Month;
            int year = date.Year;
            string duration = InputManager.MakeDurationInput().ToShortTimeString();
            cmd.CommandText = $"INSERT INTO CodingSessions(Duration, Day, Month, Year) VALUES(:duration, :day, :month, :year)";
            cmd.Parameters.AddWithValue(":day", day);
            cmd.Parameters.AddWithValue(":month", month);
            cmd.Parameters.AddWithValue(":year", year);
            cmd.Parameters.AddWithValue(":duration", duration);
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
       
        public static void Update() {
            Read();
            int id = InputManager.GetIdInput();
            if (CheckExistance(id)) {
                DateTime date = InputManager.GetDateInput();
                string day = date.Day.ToString();
                string month = date.Month.ToString();
                string year = date.Year.ToString();
                string duration = InputManager.GetDurationInput().ToShortTimeString();
                
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
        public static bool CheckExistance(int id) {
            cmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM CodingSessions WHERE Id = {id})";
            int checkQuery = Convert.ToInt32(cmd.ExecuteScalar());

            if (checkQuery == 0) {

                return false;
            } else {
                return true;
            }
        }
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
                    codingSessions.Add(new CodingSession{
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
        public static void Reports() {
            int report = InputManager.GetReportNumber();
            switch(report) {
                case 1:
                    ReportByDay();
                    break;
                case 2:
                    ReportByMonth();
                    break;
                case 3:
                    ReportByYear();
                    break;
                case 4:
                    ReportByDuration();
                    break;
            }
        }
    }
    
    }
