using CodingTrackerConsoleApp.Data;

namespace CodingTrackerConsoleApp {

    public static class ReadCodingSessions {

        public static void Read() {
            CodingRepository.Read();
            Program.MainMenu();
        }
    }
}