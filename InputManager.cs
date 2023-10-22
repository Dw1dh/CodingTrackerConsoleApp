using CodingTrackerConsoleApp.Data;
using CodingTrackerConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp {
    public static class InputManager {
        public static string GetDateInput() {
            DateTime date;
            Console.WriteLine("Write a date\nFormat dd.mm.yyyy\nWrite 0 to enter this day");
            string dateInput = Console.ReadLine();
            //if (Convert.ToInt32(dateInput) == 0) {
            //    date = DateTime.Now;
            //} else {
            //    
            //}
            date = DateTime.Parse(dateInput);
            return date.ToShortDateString();
        }
        public static string GetDurationInput() {
            DateTime duration;
            Console.WriteLine("Write a duration\nFormat: hh:mm");
            string durationInput = Console.ReadLine();
            duration = DateTime.Parse(durationInput);
            Console.WriteLine($"Duration: {duration}");
            return duration.ToShortTimeString();
        }
        public static int GetIdInput() {
            Console.WriteLine("Write an id");

            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }
        public static string MakeDurationInput() {
            DateTime startTime = new();
            DateTime endTime = new();
            DateTime duration = new();
            TimeSpan durationTime = new();
            Console.WriteLine("Would you like to start a time now or to enter a time?\n1 - Stopwatch\n2 - Enter a time");
            int userDecision = Convert.ToInt32(Console.ReadLine());
            if (userDecision == 1 || userDecision == 2) {
                switch (userDecision) {
                    case 1:

                        Console.WriteLine("Enter any key to begin");
                        Console.ReadLine();
                        startTime = DateTime.Now;
                        Console.WriteLine($"StartTime:{startTime}");

                        Console.WriteLine("Enter any key to stop a stopwatch");
                        Console.ReadLine();
                        endTime = DateTime.Now;
                        Console.WriteLine($"EndTime:{endTime}");
                        break;

                    case 2:
                        CultureInfo provider = new CultureInfo("en-US");
                        Console.WriteLine("Write a start time of coding:\nFormat:\thh:mm");

                        startTime = DateTime.ParseExact(Console.ReadLine(), "HH:mm", provider);
                        Console.WriteLine($"StartTime:{startTime}");
                        Console.WriteLine("Write an end time of coding:\nFormat:\thh:mm");

                        endTime = DateTime.ParseExact(Console.ReadLine(), "HH:mm", provider);
                        Console.WriteLine($"EndTime:{endTime}");
                        break;


                }
                durationTime = endTime.Subtract(startTime);
                duration = duration + durationTime;

            }
            Console.WriteLine($"Duration: {duration.ToShortTimeString()}");
            return duration.ToShortTimeString();

        }

    }
}
