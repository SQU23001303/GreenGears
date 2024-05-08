using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                toolManager.UpdateToolAvailability(toolId, false);
                Console.WriteLine($"Tool '{toolToLoan.Name}' has been loaned out.");
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
    }
}
