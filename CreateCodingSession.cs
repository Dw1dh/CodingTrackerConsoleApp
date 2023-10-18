using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp {
    public static class CreateCodingSession {
        static TimeOnly startTime;
        static TimeOnly endTime;
        static TimeOnly duration;
        public static void CreateSession() {
            Console.WriteLine("Creation new coding session");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Write a start time of coding:\nFormat:\thh:mm");
            try {
                string startTimeInput = Console.ReadLine();
                startTime = TimeOnly.Parse(startTimeInput);
            } catch(Exception ex) {
                Console.WriteLine($"Error in creation: {ex.Message}\nTry again");
                CreateSession();
            }
            Console.WriteLine("Write an end time of coding:\nFormat:\thh:mm");
            try {
                string endTimeInput = Console.ReadLine();
                endTime = TimeOnly.Parse(endTimeInput);
            }
            catch (Exception ex) {
                Console.WriteLine($"Error in creation: {ex.Message}\nTry again");
                CreateSession();
            }
            duration = endTime;
            duration = duration.AddHours(-startTime.Hour);        // 7:33
            duration = duration.AddMinutes(-startTime.Minute);
            Console.WriteLine($"Duration: {duration}");
        }
    }
}
