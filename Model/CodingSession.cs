namespace CodingTrackerConsoleApp.Model {

    public class CodingSession {

        public int Id {
            get; set;
        }


        public TimeOnly Duration {
            get; set;
        }
        public DateOnly Date {
            get;
            set;
        }
    }
}