using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Green_Gears
{
    class ToolManager
    {
        public List<Tools> tools;

        public ToolManager()
        {
            tools = new List<Tools>
        {
            new Tools(1, "Ladder", true, 15.00),
            new Tools(2, "Lawnmower", true, 20.00),
            new Tools(3, "Strimmer", true, 25.00),
            new Tools(4, "Wheel Barrow", true, 10.00),
            new Tools(5, "Watering Can", true, 5.00)
        };
        }

        public void DisplayAvailableTools()
        {
            Console.WriteLine("\nAvailable Tools for hire:");
            foreach (var tool in tools)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"{tool.Id}: {tool.Name} - {(tool.Available ? "Available" : "Not Available")}");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public Tools GetToolById(int toolId)
        {
            return tools.Find(t => t.Id == toolId);
        }

        public void UpdateToolAvailability(int toolId, bool available)
        {
            Tools toolToUpdate = tools.Find(t => t.Id == toolId);
            if (toolToUpdate != null)
            {
                toolToUpdate.Available = available;
                Console.WriteLine($"Tool {toolId} availability updated to {(available ? "Available" : "Not Available")}");
            }
            else
            {
                Console.WriteLine($"Tool {toolId} not found.");
            }
        }
    }
}
