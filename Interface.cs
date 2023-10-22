namespace CodingTrackerConsoleApp {

    public static class Interface {

        public static void MainMenu() {
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("---------------------------");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("0 - Close the application\n1 - Create a record\n2 - Delete a record\n3 - Delete all records\n4 - Update a record\n5 - View records\n6 - Reports");
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput) {
                case 0:
                    Console.WriteLine("Goodbye");
                    break;

                case 1:
                    DatabaseManager.Create();
                    MainMenu();
                    break;

                case 2:
                    DatabaseManager.Delete();
                    MainMenu();

                    break;

                case 3:
                    DatabaseManager.DeleteAll();
                    MainMenu();
                    break;

                case 4:
                    DatabaseManager.Update();
                    MainMenu();

                    break;

                case 5:
                    DatabaseManager.Read();
                    MainMenu();

                    break;

                case 6:
                    DatabaseManager.Reports();
                    MainMenu();
                    break;

                default:
                    Console.WriteLine("Please choose one of the options.");
                    MainMenu();
                    break;
            }
        }
    }
}