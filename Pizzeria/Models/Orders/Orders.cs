using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzeria.Models.Orders
{
    public class Orders
    {
        public int order_id { get; set; }
        public int quantity { get; set; }
        public double total { get; set; }
        public int state { get; set; }
        public int customer_customer_id { get; set; }
        public int pizza_pizza_id { get; set; }
        //Atributos para realizar la vista de las ordenes
        public string dni { get; set; }
        public string customer_name { get; set; }
        public string last_name { get; set; }
        public string pizza_name { get; set; }

    }
}