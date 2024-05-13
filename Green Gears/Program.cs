using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Green_Gears
{
    internal class Program
    {
        public static List<Customer> customers = new List<Customer>();

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
                        interactWithItems.LoanTool(toolManager, customers, toolId);
                        break;
                    case 2:
                        Console.WriteLine("You have picked option 2");
                        interactWithItems.ReturnTool(toolManager, customers);
                        break;
                    case 3:
                        Console.WriteLine("You have picked option 3");
                        SignUp(customers);
                        break;
                    case 4:
                        Console.WriteLine("You have picked option 4");
                        interactWithItems.DisplayUnavailableTools(toolManager);
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.WriteLine("You have picked option 5");
                        interactWithItems.OverdueItems(toolManager);
                        Console.ReadKey();
                        return;
                    case 6:
                        Console.WriteLine("You have picked option 6");
                        DisplayCustomers();
                        Console.ReadKey();
                        return;
                    case 7:
                        Console.WriteLine("Exiting program...");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please select a valid option.");
                        break;
                }
            }
        }

        public static void DisplayOptions()
        {
            Console.WriteLine("\nPlease select what you would like to do on our system today?");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("1 - Borrow a tool");
            Console.WriteLine("2 - Return a tool");
            Console.WriteLine("3 - Customer sign up");
            Console.WriteLine("4 - Current loan records");
            Console.WriteLine("5 - Overdue Items");
            Console.WriteLine("6 - Show customers list");
            Console.WriteLine("7 - Exit");
            Console.ForegroundColor = ConsoleColor.White;
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

        public static Customer SignUp(List<Customer> customers)
        {
            Console.WriteLine("Please enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter your email:");
            string details = Console.ReadLine();

            int id = customers.Count + 1; // Generate a unique ID for the new customer

            Customer newCustomer = new Customer(id, name, details);
            customers.Add(newCustomer);

            return newCustomer;
        }
        public static void DisplayCustomers()
        {
            Console.WriteLine("List of Customers:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.Id}, Name: {customer.Name}, Email: {customer.Details}");
            }
        }
    }
}
