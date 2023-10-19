using CodingTrackerConsoleApp.Data;
using CodingTrackerConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp {
    public static class CRUDController {
        public static void Read() {
            CodingRepository.Read();
            Interface.MainMenu();
        }


        public static void Create() {

            Console.WriteLine("Creation new coding session");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Would you like to start a stopwatch now or to enter a time?\n1 - Stopwatch\n2 - Enter a time");
            int userDecision = Convert.ToInt32(Console.ReadLine());
            if (userDecision == 1 || userDecision == 2) {
                switch (userDecision) {
                    case 1:
                        TimeController.Stopwatch();
                        break;

                    case 2:
                        TimeController.EnterTime();
                        break;
                }
                TimeController.MakeDuration();
                CodingSession codingSession = new CodingSession {
                    Id = CodingRepository.Id,
                    StartTime = TimeController.startTime,
                    EndTime = TimeController.endTime,
                    Duration = TimeController.duration
                };

                CodingRepository.Create(codingSession);
                CodingRepository.Id++;

            }
            Interface.MainMenu();
        }
        public static void Delete() {
            CodingRepository.Read();
            Console.WriteLine("Write an id of a coding session, which you would like to delete\nOr write 0 for deleting all");
            int csId = Convert.ToInt32(Console.ReadLine());
                if (csId == 0) {
                    CodingRepository.DeleteAll();
                } else {
                    CodingRepository.Delete(csId);
                }
            Interface.MainMenu();
        }
    }
}