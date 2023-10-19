using CodingTrackerConsoleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp {
    public class DeleteCodingSession {
        static int Id;
        public static void Delete() {
            ReadCodingSessions.Read();
            Console.WriteLine("Write an id of a coding session, which you would like to delete");
            try {
                Id = Convert.ToInt32(Console.ReadLine());
                CodingRepository.Delete(Id);
            } catch(Exception ex) {
                Console.WriteLine($"Error in deleting a coding session: {ex.Message}\n Try again");
                Delete();
            }
        }
    }
}
