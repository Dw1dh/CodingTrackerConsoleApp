using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp {
    public static class TimeController {
        public static TimeOnly startTime;
        public static TimeOnly endTime;
        public static TimeOnly duration;
        public static DateTime dateTime = new DateTime();
        public static string timeStartString;
        public static string timeEndString;
        public static void Stopwatch() {
            Console.WriteLine("Enter any key to begin");
            Console.ReadLine();
            dateTime = DateTime.Now;
            timeStartString = dateTime.ToShortTimeString();

            Console.WriteLine("Your stopwatch started");
            Console.WriteLine($"Time start: {timeStartString}");

            Console.WriteLine("Enter any key to stop a stopwatch");
            Console.ReadLine();
            dateTime = DateTime.Now;
            timeEndString = dateTime.ToShortTimeString();
            Console.WriteLine("Your stopwatch ended");
            Console.WriteLine($"Time end: {timeEndString}");
        }
        public static void EnterTime() {
            Console.WriteLine("Write a start time of coding:\nFormat:\thh:mm");

            timeStartString = Console.ReadLine();

            Console.WriteLine("Write an end time of coding:\nFormat:\thh:mm");

            timeEndString = Console.ReadLine();
        }
        public static void MakeDuration() {
            startTime = TimeOnly.Parse(timeStartString);
            endTime = TimeOnly.Parse(timeEndString);
            duration = endTime;
            duration = duration.AddHours(-startTime.Hour);
            duration = duration.AddMinutes(-startTime.Minute);
            if (duration == new TimeOnly(0, 0)) {
                Console.WriteLine("Duration is 0:00");

            }
            Console.WriteLine($"Duration: {duration}");
        }
    }
}
