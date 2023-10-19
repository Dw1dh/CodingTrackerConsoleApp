using CodingTrackerConsoleApp.Model;
using ConsoleTableExt;

namespace CodingTrackerConsoleApp.Data {

    public class CodingRepository {
        private static List<CodingSession> codingSessions = new List<CodingSession>();

        public static void Create(CodingSession codingSession) {
            try {
                codingSessions.Add(codingSession);
            }
            catch (Exception ex) {
                Console.WriteLine($"Error in creating a new coding session: {ex.Message}");
            }
        }

        public static void Delete(int id) {
            CodingSession codingSession = codingSessions.Where(x => x.Id == id).First();
            codingSessions.Remove(codingSession);
            Console.WriteLine("Successfully removed");
        }

        public static void Read() {
            try {
                if (codingSessions.Count > 0) {
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
                    //foreach (CodingSession codingSession in codingSessions) {
                    //    Console.WriteLine($"ID: {codingSession.Id}\t");
                    //    Console.WriteLine($"Start Time: {codingSession.StartTime}\t");
                    //    Console.WriteLine($"End Time: {codingSession.EndTime}\t");
                    //    Console.WriteLine($"Duration: {codingSession.Duration}\n");
                    //}
                } else {
                    Console.WriteLine("Nothing to show");
                    Program.MainMenu();
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error in reading coding sessions: {ex.Message}");
                Program.MainMenu();
            }
        }

        public static void DeleteAll() {
            codingSessions = new List<CodingSession>();
            Console.WriteLine("All sessions was deleted");
        }
    }
}