using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Green_Gears
{
    class Tools
    {
        //Tool class
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Available { get; set; }
        public double Price { get; set; }
        public Customer CurrentCustomer { get; set; }
        public DateTime LoanTimestamp { get; set; }

        public Tools(int id, string name, bool available, double price)
        {
            Id = id;
            Name = name;
            Available = available;
            Price = price;
        }
    }
}
