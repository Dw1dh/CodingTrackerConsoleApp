namespace CodingTrackerConsoleApp.Model {

    public class CodingSession {

        public int Id {
            get; set;
        }

        public TimeOnly StartTime {
            get; set;
        }

        public TimeOnly EndTime {
            get; set;
        }

        public TimeOnly Duration {
            get; set;
        }
    }
}