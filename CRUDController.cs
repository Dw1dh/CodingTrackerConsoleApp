//using CodingTrackerConsoleApp.Data;
//using CodingTrackerConsoleApp.Model;

//namespace CodingTrackerConsoleApp {

//    public static class CRUDController {

//        public static void Read() {
//            DatabaseManager.Read();
//            Interface.MainMenu();
//        }

//        public static void Create() {
//            Console.WriteLine("Creation new coding session");
//            Console.WriteLine("----------------------------");
//            Console.WriteLine("Would you like to start a time now or to enter a time?\n1 - Stopwatch\n2 - Enter a time");
//            int userDecision = Convert.ToInt32(Console.ReadLine());
//            if (userDecision == 1 || userDecision == 2) {
//                switch (userDecision) {
//                    case 1:
//                        TimeController.Stopwatch();
//                        break;

//                    case 2:
//                        TimeController.EnterTime();
//                        break;
//                }
//                TimeController.MakeDuration();
//                TimeController.MakeDate();
//                CodingSession codingSession = new CodingSession {
//                    Id = CodingRepository.Id,
//                    Duration = TimeController.duration,
//                    Date = TimeController.dateTime,
//                };
//                DatabaseManager.Create(codingSession );
//                CodingRepository.Create(codingSession);
//                CodingRepository.Id++;
//            }
//            Interface.MainMenu();
//        }

//        public static void Delete() {
//            DatabaseManager.Read();
//            Console.WriteLine("Write an id of a coding session, which you would like to delete\nOr write 0 for deleting all");
//            int csId = Convert.ToInt32(Console.ReadLine());
//            if (csId == 0) {
//                DatabaseManager.DeleteAll();
//            } else {
//                DatabaseManager.Delete(csId);
//            }
//            Interface.MainMenu();
//        }
//        public static void Update() {
            
//            DatabaseManager.Read();
//            Console.WriteLine("Please write an id or 0 to main menu");
//            int id = Convert.ToInt32(Console.ReadLine());
//            if (id == 0) {
//                Interface.MainMenu();
//            } else {

//                if (DatabaseManager.CheckExistance(id)) {
//                    Console.WriteLine("Input a date (format: dd.mm.yyyy):");
//                    string dateInput = Console.ReadLine();
//                    DateOnly date = DateOnly.Parse(dateInput);
//                    Console.WriteLine("Input a duration (format: hh:mm):");
//                    string durationInput = Console.ReadLine();
//                    TimeOnly duration = TimeOnly.Parse(durationInput);
//                    DatabaseManager.Update(date, duration, id);
//                } else {
//                    Interface.MainMenu();
//                }
//            }
//        }
//    }
//}