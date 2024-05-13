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

        //Loan tools to customer
        public DateTime LoanTool(ToolManager toolManager, List<Customer> customers, int toolId)
        {
            //Gets tool by ID
            Tools toolToLoan = toolManager.GetToolById(toolId);
            if (toolToLoan != null && toolToLoan.Available)
            {
                //Adds time stamp
                DateTime returnDate = DateTime.Now.AddDays(7); //return date
                toolToLoan.LoanTimestamp = DateTime.Now; //timestamp
                toolManager.UpdateToolAvailability(toolId, false);

                Console.WriteLine("Enter the customer ID who is borrowing the tool:"); //sets up user input
                if (int.TryParse(Console.ReadLine(), out int customerId))
                {
                    Customer customer = customers.FirstOrDefault(c => c.Id == customerId);
                    if (customer != null)
                    {
                        //Links customer with tool
                        Console.WriteLine($"Customer ID {customerId} matches customer: {customer.Name}");
                        Console.WriteLine($"Is {customer.Name} your name? (yes/no)");
                        string choice = Console.ReadLine().ToLower(); //converts user response to lowercase

                        if (choice == "yes")
                        {
                            //message to user 
                            Console.WriteLine("Your details are correct, advancing to loaning the tool.");
                            toolToLoan.CurrentCustomer = customer;
                        }
                        else
                        {
                            //error message
                            toolManager.UpdateToolAvailability(toolId, true);
                            Console.WriteLine("Please try again as the details are incorrect");
                            return DateTime.Now;
                        }
                    }
                    else
                    {
                        //error message
                        toolManager.UpdateToolAvailability(toolId, true);
                        Console.WriteLine("Customer ID is invalid, please try again");
                        return DateTime.Now;
                    }
                }
                else
                {
                    //error message
                    toolManager.UpdateToolAvailability(toolId, true);
                    Console.WriteLine("Invalid input. Please enter a valid customer ID.");
                    return DateTime.Now;
                }

                //message to let user know success
                Console.WriteLine($"Tool '{toolToLoan.Name}' has been loaned out to customer ID: {toolToLoan.CurrentCustomer.Id}.");
                Console.WriteLine($"Tool '{toolToLoan.Name}' has been loaned out, with the following return date: {returnDate}");
                return returnDate;
            }
            else if (toolToLoan != null && !toolToLoan.Available)
            {
                //error message
                Console.WriteLine($"Tool '{toolToLoan.Name}' is not available for loan.");
            }
            else
            {
                //error mesage
                Console.WriteLine("Invalid tool ID.");
            }

            return DateTime.Now;
        }

        //Return tools from customer
        public void ReturnTool(ToolManager toolManager, List<Customer> customers)
        {
            DisplayUnavailableTools(toolManager);
            Console.WriteLine("Enter the tool ID to return:");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int toolId))
            {
                //error message
                Console.WriteLine("Invalid input. Please enter a valid tool ID.");
                return;
            }
            Tools toolToReturn = toolManager.GetToolById(toolId);
            if (toolToReturn != null && !toolToReturn.Available)
            {
                //updates tool availability
                toolManager.UpdateToolAvailability(toolId, true);
                double overdueAmount = ItemReturned(toolManager);
                //returns total price for user
                double totalPrice = overdueAmount + toolToReturn.Price;

                //Confirmation message
                Console.WriteLine($"Tool '{toolToReturn.Name}' has been returned by {name}, the total price is {totalPrice}");
                toolToReturn.CurrentCustomer = null;
            }
            else if (toolToReturn != null && toolToReturn.Available)
            {
                //error message
                Console.WriteLine($"Tool '{toolToReturn.Name}' is already available.");
            }
            else
            {
                //error message
                Console.WriteLine($"Tool with ID '{toolId}' not found.");
            }
        }

        //Dsiplays the unavailable tools list
        public void DisplayUnavailableTools(ToolManager toolManager)
        {
            int count = 0; // counter

            foreach (var tool in toolManager.tools)
            {
                if (!tool.Available)
                {
                    count++; 
                }
            }

            if (count > 0)
            {
                //tools to return
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
                //error message
                Console.WriteLine("All tools are available.");
            }
        }

        //Displays the overdue items list
        public double OverdueItems(ToolManager toolManager)
        {
            int count = 0; //counter

            foreach (var tool in toolManager.tools)
            {
                if (!tool.Available && DateTime.Now > tool.LoanTimestamp.AddDays(7))
                {
                    count++; //increment counter
                }
            }

            if (count > 0)
            {
                foreach (var tool in toolManager.tools)
                {
                    if (!tool.Available && DateTime.Now > tool.LoanTimestamp.AddDays(7))
                    {
                        int overdueDays = (int)(DateTime.Now - tool.LoanTimestamp.AddDays(7)).TotalDays;
                        double overdueAmount = overdueDays * 10.0; //overdue amount

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

        //Items returned
        public double ItemReturned(ToolManager toolManager)
        {
            int count = 0; //counter

            foreach (var tool in toolManager.tools)
            {
                if (!tool.Available && DateTime.Now > tool.LoanTimestamp.AddDays(7))
                {
                    count++; //increment counter
                }
            }

            if (count > 0)
            {
                foreach (var tool in toolManager.tools)
                {
                    if (!tool.Available && DateTime.Now > tool.LoanTimestamp.AddDays(7))
                    {
                        int overdueDays = (int)(DateTime.Now - tool.LoanTimestamp.AddDays(7)).TotalDays;
                        double overdueAmount = overdueDays * 10.0; //overdue amount

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
