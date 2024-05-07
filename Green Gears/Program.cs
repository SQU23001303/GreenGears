﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Green_Gears
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Green Gears");
            Console.WriteLine("The place where we loan you gardening equipment");
            InitializeToolManager();
            selections(args);
        }

        static ToolManager InitializeToolManager()
        {
            ToolManager toolManager = new ToolManager();
            toolManager.AddTool(new Tools(1, "Ladder", true, 15.00));
            toolManager.AddTool(new Tools(2, "Lawnmower", true, 20.00));
            toolManager.AddTool(new Tools(3, "Strimmer", true, 25.00));
            toolManager.AddTool(new Tools(4, "Wheel Barrow", true, 10.00));
            toolManager.AddTool(new Tools(5, "Watering Can", true, 5.00));

            return toolManager;
        }

        static void selections(string[] args)
        {

            bool running = true;

            while (true)
            {
                Console.WriteLine("Please select what you would like to do on our system today?");
                Console.WriteLine("1 - Loan a piece of equipment");
                Console.WriteLine("2 - Return a piece of equipment");
                Console.WriteLine("3 - Update a customer record");
                Console.WriteLine("4 - Current Loan records");
                Console.WriteLine("5 - Exit");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int userInput))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                    continue;
                }

                switch (userInput)
                {
                    case 1:
                        Console.WriteLine("You have picked option 1");
                        loanEquipment(args);
                        break;
                    case 2:
                        Console.WriteLine("You have picked option 2");
                        break;
                    case 3:
                        Console.WriteLine("You have picked option 3");
                        break;
                    case 4:
                        Console.WriteLine("You have picked option 4");
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                }

            }
        }

        static void loanEquipment(string[] args)
        {
            bool running = true;

            while (true)
            {
                ToolManager toolManager = new ToolManager();

                Console.WriteLine("What Tool number would you like to borrow?");
                toolManager.DisplayTools();
                string userChoice = Console.ReadLine();

                int toolId;
                if (int.TryParse(userChoice, out toolId))
                {
                    Console.WriteLine("You have chose tool number " + userChoice);
                    toolManager.UpdateTools(toolId, false);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the tool ID.");
                }

                Console.WriteLine("Do you want to borrow another tool? (yes/no)");
                string answer = Console.ReadLine().ToLower();
                if (answer != "yes")
                {
                    loanEquipment(args);
                    break;
                }
                else
                {
                    Console.WriteLine("Are you done with the app? (yes/no");
                    string answer1 = Console.ReadLine().ToLower();
                    if (answer1 != "no")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        selections(args);
                        break;
                    }
                }
                Console.ReadKey();
            }
        }

    }
}
