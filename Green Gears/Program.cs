using System;
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
            ToolManager toolManager = new ToolManager();
            InteractWithItems interactWithItems = new InteractWithItems();

            Console.WriteLine("Welcome to Green Gears");
            Console.WriteLine("The place where we loan you gardening equipment");

            while (true)
            {
                DisplayOptions();

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
                        toolManager.DisplayAvailableTools();
                        int toolId = GetToolIdFromUser();
                        interactWithItems.LoanTool(toolManager, toolId);
                        break;
                    case 2:
                        Console.WriteLine("You have picked option 2");
                        interactWithItems.ReturnTool(toolManager);
                        break;
                    case 3:
                        Console.WriteLine("You have picked option 3");
                        // Update customer record
                        break;
                    case 4:
                        Console.WriteLine("You have picked option 4");
                        interactWithItems.DisplayUnavailableTools(toolManager);
                        break;
                    case 5:
                        Console.WriteLine("Exiting program...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please select a valid option.");
                        break;
                }
            }
        }

        static void DisplayOptions()
        {
            Console.WriteLine("\nPlease select what you would like to do on our system today?");
            Console.WriteLine("1 - Borrow a tool");
            Console.WriteLine("2 - Return a tool");
            Console.WriteLine("3 - Update customer record");
            Console.WriteLine("4 - Current loan records");
            Console.WriteLine("5 - Exit");
        }

        static int GetToolIdFromUser()
        {
            Console.WriteLine("Enter the tool ID you want to borrow:");
            string input = Console.ReadLine();
            int toolId;
            while (!int.TryParse(input, out toolId))
            {
                Console.WriteLine("Invalid input. Please enter a valid tool ID.");
                input = Console.ReadLine();
            }
            return toolId;
        }
    }
}
