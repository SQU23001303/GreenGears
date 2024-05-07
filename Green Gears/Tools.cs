using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Green_Gears
{
    internal class Tools
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Available { get; set; }

        public Tools (int id, string name, bool available)
        {
            Id = id;
            Name = name;
            Available = true;
        }
    }
}
