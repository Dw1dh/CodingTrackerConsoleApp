using CodingTrackerConsoleApp.Data;

namespace CodingTrackerConsoleApp {

    public class DeleteCodingSession {
        private static int Id;

        public static void Delete() {
            CodingRepository.Read();
            Console.WriteLine("Write an id of a coding session, which you would like to delete\nOr write 0 for deleting all");
            try {
                Id = Convert.ToInt32(Console.ReadLine());
                if (Id == 0) {
                    CodingRepository.DeleteAll();
                } else {
                    CodingRepository.Delete(Id);
                }

                Program.MainMenu();
            }
            catch (Exception ex) {
                Console.WriteLine($"Error in deleting a coding session: {ex.Message}\n Try again");
                Delete();
            }
        }
    }
}