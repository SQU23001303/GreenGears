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

        public ToolManager()
        {
            tools = new List<Tools>();
        }

        public void AddTool(Tools tool)
        {
            tools.Add(tool);
        }

        public void UpdateTools(int toolId, bool Available)
        {
            Tools toolToUpdate = tools.Find(t => t.Id == toolId);
            if (toolToUpdate != null)
            {
                toolToUpdate.Available = true;
                Console.WriteLine($"Tool {toolId} availability updated to {(Available ? "Available" : "Not Available")}.");
            }
            else
            {
                Console.WriteLine($"Tool {toolId} not found.");
            }
        }

        public void DisplayTools()
        {
            ToolManager.UpdateTools();
            
            Console.WriteLine("Available Tools for hire:");
            foreach (var tool in tools)
            {
                if (tool.Available == true)
                {

                    Console.WriteLine($"{tool.Id}: {tool.Name} - Available");
                }
                else
                {

                }
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
            }
            else
            {
                Console.WriteLine("Invalid tool ID or tool is already available.");
            }
        }
    }
}
