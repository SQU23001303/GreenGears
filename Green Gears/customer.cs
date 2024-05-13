using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Green_Gears
{
    class Customer
    {
        //Customer class
        public static Customer customerId;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public static int Count { get; internal set; }

        public Customer(int id, string name, string details)
        {
            Id = id;
            Name = name;
            Details = details;
        }
    }
}
