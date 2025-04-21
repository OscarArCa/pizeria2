using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzeria.Models.Pizza
{
    public class Pizza
    {
        public int pizza_id { get; set; }
        public string name { get; set; }
        public double price { get; set; }

        public string name_price
        {
            get { return $"{name} - S/ {price}"; }
        }
    }
}