using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzeria.Models.Customer
{
    public class Customer
    {
        public int customer_id { get; set; }
        public string dni { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
    }
}