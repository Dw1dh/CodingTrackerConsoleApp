using CodingTrackerConsoleApp.Model;
using ConsoleTableExt;
using System.Configuration;
using System.Data.SQLite;

namespace CodingTrackerConsoleApp {

    internal class DatabaseManager {
        private static SQLiteCommand cmd;
        public static SQLiteConnection conn;
        public static List<CodingSession> codingSessions = new();
        private static string connString = ConfigurationManager.AppSettings["ConnectionString"];

        /// <summary>
        /// Initializing a database manager
        /// </summary>
        public static void Init() {
            try {
                conn = new SQLiteConnection(connString);
                Console.WriteLine("Connected");
                conn.Open();
                cmd = new SQLiteCommand(conn);
                CodingSessionsManager.Init();
                GoalManager.Init();
            }
            catch (SQLiteException ex) {
                Console.WriteLine($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
            }

        }
        

        /// <summary>
        /// Deleting all function
        /// </summary>
        /// <param name="tableName"></param>
        public static void DeleteAll(string tableName) {
            cmd.CommandText = $"DELETE FROM {tableName}";
            cmd.ExecuteNonQuery();
            Console.WriteLine("Deleting all");
        }
       

        /// <summary>
        /// Checking id existence
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool CheckIdExistance(int id, string tableName) {
            cmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM CodingSessions WHERE Id = {id})";
            int checkQuery = Convert.ToInt32(cmd.ExecuteScalar());

            if (checkQuery == 0) {
                return false;
            } else {
                return true;
            }
        }

        /// <summary>
        /// Checking database existence for reading and showing
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool CheckExistance(string tableName) {
            cmd.CommandText = $"SELECT * FROM {tableName}";
            var rdr = cmd.ExecuteReader();
            if (rdr.HasRows) {
                rdr.Close();
                return true;
            } else {
                rdr.Close();
                return false;
            }
            
        }
    }
} 
