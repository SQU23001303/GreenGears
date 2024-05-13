using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Green_Gears
{
    class InteractWithItems
    {
        string name = "";

        public DateTime LoanTool(ToolManager toolManager, List<Customer> customers, int toolId)
        {
            Tools toolToLoan = toolManager.GetToolById(toolId);
            if (toolToLoan != null && toolToLoan.Available)
            {
                DateTime returnDate = DateTime.Now.AddDays(7);
                toolToLoan.LoanTimestamp = DateTime.Now;
                toolManager.UpdateToolAvailability(toolId, false);

                Console.WriteLine("Enter the customer ID who is borrowing the tool:");
                if (int.TryParse(Console.ReadLine(), out int customerId))
                {
                    Customer customer = customers.FirstOrDefault(c => c.Id == customerId);
                    if (customer != null)
                    {
                        Console.WriteLine($"Customer ID {customerId} matches customer: {customer.Name}");
                        Console.WriteLine($"Is {customer.Name} your name? (yes/no)");
                        string choice = Console.ReadLine().ToLower();

                        if (choice == "yes")
                        {
                            Console.WriteLine("Your details are correct, advancing to loaning the tool.");
                            toolToLoan.CurrentCustomer = customer;
                        }
                        else
                        {
                            toolManager.UpdateToolAvailability(toolId, true);
                            Console.WriteLine("Please try again as the details are incorrect");
                            return DateTime.Now;
                        }
                    }
                    else
                    {
                        toolManager.UpdateToolAvailability(toolId, true);
                        Console.WriteLine("Customer ID is invalid, please try again");
                        return DateTime.Now;
                    }
                }
                else
                {
                    toolManager.UpdateToolAvailability(toolId, true);
                    Console.WriteLine("Invalid input. Please enter a valid customer ID.");
                    return DateTime.Now;
                }

                Console.WriteLine($"Tool '{toolToLoan.Name}' has been loaned out to customer ID: {toolToLoan.CurrentCustomer.Id}.");
                Console.WriteLine($"Tool '{toolToLoan.Name}' has been loaned out, with the following return date: {returnDate}");
                return returnDate;
            }
            else if (toolToLoan != null && !toolToLoan.Available)
            {
                Console.WriteLine($"Tool '{toolToLoan.Name}' is not available for loan.");
            }
            else
            {
                Console.WriteLine("Invalid tool ID.");
            }

            return DateTime.Now;
        }

        public void ReturnTool(ToolManager toolManager, List<Customer> customers)
        {
            DisplayUnavailableTools(toolManager);
            Console.WriteLine("Enter the tool ID to return:");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int toolId))
            {
                Console.WriteLine("Invalid input. Please enter a valid tool ID.");
                return;
            }
            Tools toolToReturn = toolManager.GetToolById(toolId);
            if (toolToReturn != null && !toolToReturn.Available)
            {
                toolManager.UpdateToolAvailability(toolId, true);
                double overdueAmount = ItemReturned(toolManager);
                double totalPrice = overdueAmount + toolToReturn.Price;

                Console.WriteLine($"Tool '{toolToReturn.Name}' has been returned by {name}, the total price is {totalPrice}");
                toolToReturn.CurrentCustomer = null;
            }
            else if (toolToReturn != null && toolToReturn.Available)
            {
                Console.WriteLine($"Tool '{toolToReturn.Name}' is already available.");
            }
            else
            {
                Console.WriteLine($"Tool with ID '{toolId}' not found.");
            }
        }

        public void DisplayUnavailableTools(ToolManager toolManager)
        {
            int count = 0;

            foreach (var tool in toolManager.tools)
            {
                if (!tool.Available)
                {
                    count++; 
                }
            }

            if (count > 0)
            {
                Console.WriteLine("Tools to return:");
                foreach (var tool in toolManager.tools)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    if (!tool.Available)
                    {
                        Console.WriteLine($"{tool.Id}: {tool.Name} {tool.LoanTimestamp.AddDays(7)}");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.WriteLine("All tools are available.");
            }
        }

        public double OverdueItems(ToolManager toolManager)
        {
            int count = 0;

            foreach (var tool in toolManager.tools)
            {
                if (!tool.Available && DateTime.Now > tool.LoanTimestamp.AddDays(7))
                {
                    count++;
                }
            }

            if (count > 0)
            {
                foreach (var tool in toolManager.tools)
                {
                    if (!tool.Available && DateTime.Now > tool.LoanTimestamp.AddDays(7))
                    {
                        int overdueDays = (int)(DateTime.Now - tool.LoanTimestamp.AddDays(7)).TotalDays;
                        double overdueAmount = overdueDays * 10.0;

                        Console.WriteLine($"Tool '{tool.Name}' is overdue by {overdueDays} days. Overdue fine: £{overdueAmount}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No overdue items");
            }

            return 0;
        }

        public double ItemReturned(ToolManager toolManager)
        {
            int count = 0;

            foreach (var tool in toolManager.tools)
            {
                if (!tool.Available && DateTime.Now > tool.LoanTimestamp.AddDays(7))
                {
                    count++;
                }
            }

            if (count > 0)
            {
                foreach (var tool in toolManager.tools)
                {
                    if (!tool.Available && DateTime.Now > tool.LoanTimestamp.AddDays(7))
                    {
                        int overdueDays = (int)(DateTime.Now - tool.LoanTimestamp.AddDays(7)).TotalDays;
                        double overdueAmount = overdueDays * 10.0;

                        Console.WriteLine($"Tool '{tool.Name}' is overdue by {overdueDays} days. Overdue fine: £{overdueAmount}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Item is within return date so no fee added on");
            }

            return 0;
        }
    }
}
