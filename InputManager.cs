﻿using System;
using System.Diagnostics;
using System.Globalization;
using static CodingTrackerConsoleApp.ReportsManager;

namespace CodingTrackerConsoleApp {

    public static class InputManager {

        public static DateTime GetDateInput() {
            DateTime date;
            Console.WriteLine("Write a date\nFormat dd.mm.yyyy\nWrite 0 to enter this day");
            string dateInput = Console.ReadLine();
            if (dateInput == "0") {
                date = DateTime.Now;
            } else {
                date = DateTime.Parse(dateInput);
            }

            return date;
        }

        public static DateTime GetDurationInput() {
            DateTime duration;
            Console.WriteLine("Write a duration\nFormat: hh:mm");
            string durationInput = Console.ReadLine();
            duration = DateTime.Parse(durationInput);
            Console.WriteLine($"Duration: {duration}");
            return duration;
        }

        public static int GetIdInput() {
            Console.WriteLine("Write an id");

            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public static DateTime MakeDurationInput() {
            DateTime startTime = new();
            DateTime endTime = new();
            DateTime duration = new();
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
                        startTime = DateTime.Parse(Console.ReadLine());
                        
                        Console.WriteLine($"StartTime:{startTime}");
                        Console.WriteLine("Write an end time of coding:\nFormat:\thh:mm");
                        endTime = DateTime.Parse(Console.ReadLine());
                        
                        Console.WriteLine($"EndTime:{endTime}");
                        break;
                }
                duration = endTime.AddHours(-startTime.Hour).AddMinutes(-startTime.Minute);
            }
            Console.WriteLine($"Duration: {duration.ToShortTimeString()}");
            return duration;
        }

        public static int GetReportNumber() {
            Console.WriteLine("Which report would you like to choose:\n1 - Report by day\n2 - Report by month\n3 - Report by year\n4 - Report by duration\n5 - Report total duration\n6 - Report goals\n0 - Back to main menu");
            int userInput = Convert.ToInt32(Console.ReadLine());
            return userInput;
        }
        public static string GetNameGoalInput() {
            Console.WriteLine("Name your goal");
            string goalName = Console.ReadLine();
            return goalName;
        }
        public static int GetTimeGoalInput() {
            Console.WriteLine("Put the amount of hours to achieve this goal");
            int progress = Convert.ToInt32(Console.ReadLine());
            return progress;
        }
    }
}