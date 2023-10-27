using System.Data.Entity.Infrastructure;
using System.Globalization;

namespace CodingTrackerConsoleApp {

    public static class InputManager {
        public static int GetIntUserInput() {
            bool isAgain = true;
            int intUserInput = new();
            while(isAgain) {
                try {
                    intUserInput = Convert.ToInt32(Console.ReadLine());
                    isAgain = false;

                }
                catch (Exception e) {
                    Console.WriteLine("Enter an int value");
                    isAgain = true;
                }
                
                
            }
            return intUserInput;
            
            
        }
        public static string GetStringUserInput() {
            bool isAgain = true;
            string stringUserInput = null;
            while (isAgain) {
                try {
                    stringUserInput = Console.ReadLine();
                    
                    if (String.IsNullOrEmpty(stringUserInput)) {
                        throw new Exception();
                    } else {
                        isAgain = false;
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("Enter an string value");
                    isAgain = true;
                }


            }
            return stringUserInput;
        }
        public static DateTime GetDateInput() {
            DateTime date = new();
            bool isAgain = true;
            string dateInput = null;
            while (isAgain) {
                try {
                    Console.WriteLine("Write a date\t Format: dd.mm.yyyy");
                    dateInput = Console.ReadLine();
                    
                    if (String.IsNullOrEmpty(dateInput)) {
                        throw new Exception();
                    } else {
                        date = DateTime.Parse(dateInput);
                        isAgain = false;
                    }

                }
                catch (Exception e) {
                    Console.WriteLine($"Error:  {e.Message}");
                    isAgain = true;
                }
            }
            return date;
        }
        public static DateTime GetTimeInput() {
            DateTime time = new();
            bool isAgain = true;
            string timeInput = null;
            while (isAgain) {
                try {
                    Console.WriteLine("Enter a time\t Format hh:mm");    
                    timeInput = Console.ReadLine();
                    
                    if (String.IsNullOrEmpty(timeInput)) {
                        throw new Exception();
                    } else {
                        isAgain = false;
                        time = DateTime.Parse(timeInput);
                    }

                }
                catch (Exception e) {
                    Console.WriteLine($"Error:  {e.Message}");
                    isAgain = true;
                }
            }
            return time;
        }
        

















public static DateTime GetDateInput2() {
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
            Console.WriteLine("Which report would you like to choose:\n1 - View records by day\n2 - View records by month\n3 - View records by year\n4 - View records by duration\n5 - View total duration\n6 - Goals\n0 - Back to main menu");
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