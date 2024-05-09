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
        public void LoanTool(ToolManager toolManager, int toolId)
        {
            Tools toolToLoan = toolManager.GetToolById(toolId);
            if (toolToLoan != null && toolToLoan.Available)
            {
                DateTime returnDate = DateTime.Now.AddDays(7); // Assuming a loan period of 7 days
                toolToLoan.LoanTimestamp = DateTime.Now; // Record the loan timestamp
                toolManager.UpdateToolAvailability(toolId, false);
                Console.WriteLine($"Tool '{toolToLoan.Name}' has been loaned out, with the following return date: {returnDate}");
            }
            else if (toolToLoan != null && !toolToLoan.Available)
            {
                Console.WriteLine($"Tool '{toolToLoan.Name}' is not available for loan.");
            }
            else
            {
                Console.WriteLine("Invalid tool ID.");
            }
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
                Console.WriteLine($"Tool '{toolToReturn.Name}' has been returned.");
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
                   if (!tool.Available)
                    {
                        Console.WriteLine($"{tool.Id}: {tool.Name}");
                    }
                        
                }
            }
            else
            {
                Console.WriteLine("All tools are available.");
            }
        }
    }
}
