using CodingTrackerConsoleApp.Data;
using CodingTrackerConsoleApp.Model;
using System.Security.Cryptography.X509Certificates;

namespace CodingTrackerConsoleApp {

    public static class CreateCodingSession {
        private static TimeOnly startTime;
        private static TimeOnly endTime;
        private static TimeOnly duration;
        private static int Id = 1;
        private static DateTime dateTime = new DateTime();
        private static string dateTimeStringStart;
        private static string dateTimeStringEnd;

        public static void CreateSession() {
            Console.WriteLine("Creation new coding session");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Would you like to start a stopwatch now or to enter a time?\n1 - Stopwatch\n2 - Enter a time");
            int userDecision = Convert.ToInt32(Console.ReadLine());
            if (userDecision == 1 || userDecision == 2) {
                switch (userDecision) {
                    case 1:

                        Console.WriteLine("Enter any key to begin");
                        Console.ReadLine();
                        dateTime = DateTime.Now;
                        dateTimeStringStart = dateTime.ToShortTimeString();

                        Console.WriteLine("Your stopwatch started");
                        Console.WriteLine($"Time start: {dateTimeStringStart}");

                        Console.WriteLine("Enter ant key to stop a stopwatch");
                        Console.ReadLine();
                        dateTime = DateTime.Now;
                        dateTimeStringEnd = dateTime.ToShortTimeString();
                        Console.WriteLine("Your stopwatch ended");
                        Console.WriteLine($"Time end: {dateTimeStringEnd}");
                        startTime = TimeOnly.Parse(dateTimeStringStart);
                        endTime = TimeOnly.Parse(dateTimeStringEnd);
                        break;

                    case 2:
                        Console.WriteLine("Write a start time of coding:\nFormat:\thh:mm");

                        string startTimeInput = Console.ReadLine();
                        startTime = TimeOnly.Parse(startTimeInput);

                        Console.WriteLine("Write an end time of coding:\nFormat:\thh:mm");

                        string endTimeInput = Console.ReadLine();
                        endTime = TimeOnly.Parse(endTimeInput);

                        break;
                }
                duration = endTime;
                duration = duration.AddHours(-startTime.Hour);
                duration = duration.AddMinutes(-startTime.Minute);
                Console.WriteLine($"Duration: {duration}");
                CodingSession codingSession = new CodingSession {
                    Id = Id,
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = duration
                };

                CodingRepository.Create(codingSession);
                Id++;
                Program.MainMenu();
            }
        }
    }
}