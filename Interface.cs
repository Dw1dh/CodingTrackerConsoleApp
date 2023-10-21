namespace CodingTrackerConsoleApp {

    public static class Interface {

        public static void MainMenu() {
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("---------------------------");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("0 - Close the application\n1 - Create a coding session\n2 - Delete coding session\n3 - Update coding sessions\n4 - View coding sessions");
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput) {
                case 0:
                    Console.WriteLine("Goodbye");
                    break;

                case 1:
                    CRUDController.Create();

                    break;

                case 2:
                    CRUDController.Delete();

                    break;

                case 3:
                    CRUDController.Update();

                    break;
                case 4:
                    CRUDController.Read();
                    break;
                default:
                    Console.WriteLine("Please choose one of the options.");
                    MainMenu();
                    break;
            }
        }
    }
}