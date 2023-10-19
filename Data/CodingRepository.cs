using CodingTrackerConsoleApp.Model;
using ConsoleTableExt;

namespace CodingTrackerConsoleApp.Data {

    public class CodingRepository {
        public static List<CodingSession> codingSessions = new List<CodingSession>();
        public static int Id = 1;

        public static void Create(CodingSession codingSession) {
                codingSessions.Add(codingSession);

        }

        public static void Delete(int id) {
            CodingSession codingSession = codingSessions.Where(x => x.Id == id).First();
            codingSessions.Remove(codingSession);
            Console.WriteLine("Successfully removed");
        }

        public static void Read() {
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
                } else {
                    Console.WriteLine("Nothing to show");
                }

        }

        public static void DeleteAll() {
            codingSessions = new List<CodingSession>();
            Console.WriteLine("All sessions was deleted");
        }
    }
}