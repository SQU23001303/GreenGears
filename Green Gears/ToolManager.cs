using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Green_Gears
{
    internal class ToolManager
    {
        private List<Tools> tools;
        private int toolId;

        public ToolManager()
        {
            tools = new List<Tools>();
        }

        public void AddTool(Tools tool)
        {
            tools.Add(tool);
        }

        public bool UpdateTools(int toolId, bool available)
        { 
            Tools toolToUpdate = tools.Find(t => t.Id == toolId);
            if (toolToUpdate != null)
            {
                toolToUpdate.Available = false;
                Console.WriteLine($"Tool {toolId} availability updated to {(available ? "Available" : "Not Available")}.");
                return available;
                //need to return the outcome into tool class data so i can pull the information into display tools
            }
            else
            {
                Console.WriteLine($"Tool {toolId} not found.");
                return false;
            }
        }

        public void DisplayTools()
        {
            Console.WriteLine("Available Tools for hire:");
            foreach (var tool in tools)
            {
                Console.WriteLine($"{tool.Id}: {tool.Name} - {(tool.Available ? "Available" : "Not Available")}.");
                //Pull this code from Tools as it is and make sure update method outputs it properly
            }
        }

        public void LoanTool(int toolId)
        {
            Tools tool = tools.Find(t => t.Id == toolId);
            if (tool != null && tool.Available)
            {
                tool.Available = false;
                Console.WriteLine($"Tool '{tool.Name}' has been loaned out.");
            }
            else
            {
                Console.WriteLine("Invalid tool ID or tool is not available.");
            }
        }

        public void ReturnTool(int toolId)
        {
            Tools tool = tools.Find(t => t.Id == toolId);
            if (tool != null && !tool.Available)
            {
                tool.Available = true;
                Console.WriteLine($"Tool '{tool.Name}' has been returned.");
                //Return the availbility as true
            }
            else
            {
                Console.WriteLine("Invalid tool ID or tool is already available.");
            }
        }
    }
}
