using CodingTrackerConsoleApp.Model;
using ConsoleTableExt;
using System.Data.SQLite;

namespace CodingTrackerConsoleApp {

    public class ReportsManager {
        private static SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.conn);

        /// <summary>
        /// Sort by day
        /// </summary>
        public static void SortByDay() {
            CodingSessionsManager.codingSessions = new List<CodingSession>();
            cmd.CommandText = "SELECT * FROM CodingSessions ORDER BY Day";
            var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                if (rdr.HasRows) {
                    CodingSessionsManager.codingSessions.Add(new CodingSession {
                        Id = rdr.GetInt32(0),
                        Duration = rdr.GetString(1),
                        Day = rdr.GetInt32(2),
                        Month = rdr.GetInt32(3),
                        Year = rdr.GetInt32(4)
                    });
                }
            }
            rdr.Close();
            CodingSessionsManager.Show();
        }

        /// <summary>
        /// Sort by month
        /// </summary>
        public static void SortByMonth() {
            CodingSessionsManager.codingSessions = new List<CodingSession>();
            cmd.CommandText = "SELECT * FROM CodingSessions ORDER BY Month";
            var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                if (rdr.HasRows) {
                    CodingSessionsManager.codingSessions.Add(new CodingSession {
                        Id = rdr.GetInt32(0),
                        Duration = rdr.GetString(1),
                        Day = rdr.GetInt32(2),
                        Month = rdr.GetInt32(3),
                        Year = rdr.GetInt32(4)
                    });
                }
            }
            rdr.Close();
            CodingSessionsManager.Show();
        }

        
        /// <summary>
        /// Sort by year
        /// </summary>
        public static void SortByYear() {
            CodingSessionsManager.codingSessions = new List<CodingSession>();
            cmd.CommandText = "SELECT * FROM CodingSessions ORDER BY Year";
            var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                if (rdr.HasRows) {
                    CodingSessionsManager.codingSessions.Add(new CodingSession {
                        Id = rdr.GetInt32(0),
                        Duration = rdr.GetString(1),
                        Day = rdr.GetInt32(2),
                        Month = rdr.GetInt32(3),
                        Year = rdr.GetInt32(4)
                    });
                }
            }
            rdr.Close();

            CodingSessionsManager.Show();
        }


        /// <summary>
        /// Sorts the duration of the table by duration.
        /// </summary>
        public static void SortByDuration() {
            CodingSessionsManager.codingSessions = new List<CodingSession>();
            List<CodingSession> orderedCodingSessions = new List<CodingSession>();
            List<DateTime> dateTimeList = new List<DateTime>();
            cmd.CommandText = "SELECT * FROM CodingSessions";
            var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                if (rdr.HasRows) {
                    CodingSessionsManager.codingSessions.Add(new CodingSession {
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
            for (int i = 0; i < CodingSessionsManager.codingSessions.Count; i++) {
                orderedCodingSessions = CodingSessionsManager.codingSessions.OrderBy(o => o.Duration == orderedDateTimeList[i].ToShortTimeString()).ToList();
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

        /// <summary>
        /// View total time
        /// </summary>
        public static void ViewTotalTime() {
            int totalHours = 0;
            int totalMinutes = 0;
            DateTime duration;
            CodingSessionsManager.Read();
            foreach (CodingSession codingSession in CodingSessionsManager.codingSessions) {
                duration = DateTime.Parse(codingSession.Duration);
                totalHours += duration.Hour;
                totalMinutes += duration.Minute;
            }
            Console.WriteLine($"The total duration time: {totalHours} hours, {totalMinutes} minutes");
        }

        }
    }