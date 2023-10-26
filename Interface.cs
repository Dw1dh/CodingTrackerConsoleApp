namespace CodingTrackerConsoleApp {

    public static class Interface {

        public static void MainMenu() {
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("---------------------------");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("0 - Close the application\n1 - Create a record\n2 - Delete record\n3 - Update a record\n4 - View records\n5 - Other features");
            int userInput = InputManager.GetIntUserInput();
            switch (userInput)
                {
                case 0:
                    Console.WriteLine("Goodbye");
                    break;

                case 1:
                    CodingSessionsManager.Create();
                    MainMenu();
                    break;

                case 2:
                    CodingSessionsManager.Delete();
                    MainMenu();

                    break;

                case 3:
                    CodingSessionsManager.Update();
                    MainMenu();

                    break;

                case 4:
                    CodingSessionsManager.Read();
                    CodingSessionsManager.Show();
                    MainMenu();

                    break;

                case 5:
                    ReportsMenu();
                    MainMenu();
                    break;

                default:
                    Console.WriteLine("Please choose one of the options.");
                    MainMenu();
                    break;
            }
        }
        public static void GoalsMenu() {
            Console.WriteLine("0 - Back to main menu\n1 - View goals\n2 - Make a new goal\n3 - Delete a goal\n4 - Update a goal\n5 - Show remaining hours to achieve goal(s)");
            int userInput = InputManager.GetIntUserInput();
            switch (userInput) {
                case 0:
                    MainMenu();
                    break;

                case 1:
                    GoalManager.Read();
                    GoalManager.Show();
                    break;

                case 2:
                    GoalManager.Create();
                    break;

                case 3:

                    GoalManager.Delete();


                    break;

                case 4:
                    GoalManager.Update();
                    break;
                case 5:
                    GoalManager.RemainingHours();
                    break;
                default:
                    Console.WriteLine("Choose something from the list");
                    GoalsMenu();
                    break;
            }
        }
        public static void ReportsMenu() {
            Console.WriteLine("0 - Back to main menu\n1 - Sort by day\n2 - Sort by month\n3 - Sort by year\n4 - Sort by duration\n5 - View total time\n6 - Goals");
            int report = InputManager.GetIntUserInput();
            switch (report) {
                case 0:
                   MainMenu();
                    break;

                case 1:
                    ReportsManager.SortByDay();
                    break;

                case 2:
                    ReportsManager.SortByMonth();
                    break;

                case 3:
                    ReportsManager.SortByYear();
                    break;

                case 4:
                    ReportsManager.SortByDuration();
                    break;

                case 5:
                    ReportsManager.ViewTotalTime();
                    break;

                case 6:
                    GoalsMenu();
                    break;

                default:
                    Console.WriteLine("Choose something");
                    ReportsMenu();
                    break;
            }
        }
    }
}