using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Green_Gears
{
    class InteractWithItems
    {
        public DateTime LoanTool(ToolManager toolManager, int toolId)
        {
            Tools toolToLoan = toolManager.GetToolById(toolId);
            if (toolToLoan != null && toolToLoan.Available)
            {
                DateTime returnDate = DateTime.Now.AddDays(7); // Assuming a loan period of 7 days
                toolToLoan.LoanTimestamp = DateTime.Now; // Record the loan timestamp
                toolManager.UpdateToolAvailability(toolId, false);

                toolToLoan.CurrentCustomer = Customer.customerId;
                Console.WriteLine($"Tool '{toolToLoan.Name}' has been loaned out to customer ID: {Customer.customerId}.");

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
            return DateTime.Now; // Return a default value if the tool cannot be loaned
        }

        public void ReturnTool(ToolManager toolManager)
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
                double overdueAmount = OverdueItems(toolManager); // Get the overdue amount
                double totalPrice = overdueAmount + toolToReturn.Price;
                Console.WriteLine($"Tool '{toolToReturn.Name}' has been returned, the total price is {totalPrice}");
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
                        // Calculate the number of days overdue
                        int overdueDays = (int)(DateTime.Now - tool.LoanTimestamp.AddDays(7)).TotalDays;

                        // Calculate the overdue amount
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
    }
}
