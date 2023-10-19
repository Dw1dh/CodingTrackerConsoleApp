﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp {
    public static class Interface {
        public static void MainMenu() {
            Console.WriteLine("\nThis is a Coding Tracker Console App");
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("---------------------------");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("0 - Close the application\n1 - Create a coding session\n2 - Delete coding session\n3 - Read coding sessions");
            try {
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
                        CRUDController.Read();
                        break;

                    default:
                        throw new Exception("Please choose one of the options.");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error in choice: {ex.Message}\nTry again\n");
                MainMenu();
            }
        }
    }
}
