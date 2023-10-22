namespace CodingTrackerConsoleApp {

    public static class TimeController {
        public static TimeOnly startTime;
        public static TimeOnly endTime;
        public static TimeOnly duration;
        public static string date;
        public static DateOnly dateTime = new DateOnly();
        public static string timeStartString;
        public static string timeEndString;

        public static void Stopwatch() {
            Console.WriteLine("Enter any key to begin");
            Console.ReadLine();
            dateTime = DateOnly.FromDateTime(DateTime.Now);
            timeStartString = TimeOnly.FromDateTime(DateTime.Now).ToShortTimeString();

            Console.WriteLine("Your stopwatch started");
            Console.WriteLine($"Time start: {timeStartString}");

            Console.WriteLine("Enter any key to stop a stopwatch");
            Console.ReadLine();
            dateTime = DateOnly.FromDateTime(DateTime.Now);
            timeEndString = TimeOnly.FromDateTime(DateTime.Now).ToShortTimeString();
            Console.WriteLine("Your stopwatch ended");
            Console.WriteLine($"Time end: {timeEndString}");
            date = dateTime.ToShortDateString();
            Console.WriteLine($"Date: {date}");
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

        public static void MakeDate() {
            Console.WriteLine("Write a date:\nFormat: dd.mm.yyyy\nOr press 0 to write today date");
            string dateInput = Console.ReadLine();
            try {
                if (Convert.ToInt32(dateInput) == 0) {
                    dateTime = DateOnly.FromDateTime(DateTime.Now);
                    Console.WriteLine("Today");
                }
            }
            catch {
                dateTime = DateOnly.Parse(dateInput);

                Console.WriteLine("Another");
            }
            date = dateTime.ToShortDateString();

            Console.WriteLine($"Date: {dateTime}");
        }
    }
}