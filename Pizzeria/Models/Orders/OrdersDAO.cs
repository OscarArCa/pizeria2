using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Pizzeria.Models.Customer;
using Pizzeria.Models.Pizza;

namespace Pizzeria.Models.Orders
{
    public class OrdersDAO
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        public string server = "localhost";
        public string puerto = "3306";
        public string database = "bdpizzeria";
        public string uid = "root";
        public string password = "usbw";
        void ConnectionString()
        {
            string constring;
            constring = "SERVER=" + server + ";" + "PORT=" + puerto + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            con = new MySqlConnection(constring);
        }
        public Boolean AgregarOrden(Orders ord)
        {
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO ORDERS (QUANTITY,TOTAL,STATE,CUSTOMER_CUSTOMER_ID,PIZZA_PIZZA_ID) " +
                "VALUES(" + ord.quantity + "," + ord.total + "," + ord.state + "," + ord.customer_customer_id + "," + ord.pizza_pizza_id + ")";
            dr = cmd.ExecuteReader();
            if (dr != null)
            {
                dr.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Orders> MostrarOrdenesRegistradas()
        {
            List<Orders> lista = new List<Orders>();
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText =
        "SELECT " +
            "o.order_id, " +
            "c.dni, " +
            "c.name AS customer_name, " +
            "c.last_name, " +
            "p.name AS pizza_name, " +
            "p.pizza_id, " +
            "o.quantity, " +
            "o.total " +
        "FROM ORDERS o " +
        "INNER JOIN CUSTOMER c ON o.customer_customer_id = c.customer_id " +
        "INNER JOIN PIZZA p ON o.pizza_pizza_id = p.pizza_id " +
        "WHERE o.state = 0";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Orders ord = new Orders();
                ord.order_id = Convert.ToInt32(dr["ORDER_ID"]);
                ord.dni = dr["DNI"].ToString();
                ord.customer_name = dr["CUSTOMER_NAME"].ToString();
                ord.last_name = dr["LAST_NAME"].ToString();
                ord.pizza_name = dr["PIZZA_NAME"].ToString();
                ord.pizza_pizza_id = Convert.ToInt32(dr["PIZZA_ID"]);
                ord.quantity = Convert.ToInt32(dr["QUANTITY"]);
                ord.total = Convert.ToDouble(dr["TOTAL"]);
                lista.Add(ord);
            }
            con.Close();
            return lista;
        }
        public Boolean PagarOrden(int order_id)
        {
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE ORDERS SET STATE=1 WHERE ORDER_ID='" + order_id + "'";
            dr = cmd.ExecuteReader();
            if (dr != null)
            {
                dr.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Orders> MostrarOrdenesCocina()
        {
            List<Orders> lista = new List<Orders>();
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText =
        "SELECT " +
            "o.order_id, " +
            "c.dni, " +
            "c.name AS customer_name, " +
            "c.last_name, " +
            "p.name AS pizza_name, " +
            "o.quantity, " +
            "o.total " +
        "FROM ORDERS o " +
        "INNER JOIN CUSTOMER c ON o.customer_customer_id = c.customer_id " +
        "INNER JOIN PIZZA p ON o.pizza_pizza_id = p.pizza_id " +
        "WHERE o.state = 1";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Orders ord = new Orders();
                ord.order_id = Convert.ToInt32(dr["ORDER_ID"]);
                ord.dni = dr["DNI"].ToString();
                ord.customer_name = dr["CUSTOMER_NAME"].ToString();
                ord.last_name = dr["LAST_NAME"].ToString();
                ord.pizza_name = dr["PIZZA_NAME"].ToString();
                ord.quantity = Convert.ToInt32(dr["QUANTITY"]);
                ord.total = Convert.ToDouble(dr["TOTAL"]);
                lista.Add(ord);
            }
            con.Close();
            return lista;
        }
        public Boolean CocinarOrden(int order_id)
        {
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE ORDERS SET STATE=2 WHERE ORDER_ID='" + order_id + "'";
            dr = cmd.ExecuteReader();
            if (dr != null)
            {
                dr.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Orders> MostrarOrdenesCerrarCocina()
        {
            List<Orders> lista = new List<Orders>();
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText =
        "SELECT " +
            "o.order_id, " +
            "c.dni, " +
            "c.name AS customer_name, " +
            "c.last_name, " +
            "p.name AS pizza_name, " +
            "o.quantity, " +
            "o.total " +
        "FROM ORDERS o " +
        "INNER JOIN CUSTOMER c ON o.customer_customer_id = c.customer_id " +
        "INNER JOIN PIZZA p ON o.pizza_pizza_id = p.pizza_id " +
        "WHERE o.state = 2";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Orders ord = new Orders();
                ord.order_id = Convert.ToInt32(dr["ORDER_ID"]);
                ord.dni = dr["DNI"].ToString();
                ord.customer_name = dr["CUSTOMER_NAME"].ToString();
                ord.last_name = dr["LAST_NAME"].ToString();
                ord.pizza_name = dr["PIZZA_NAME"].ToString();
                ord.quantity = Convert.ToInt32(dr["QUANTITY"]);
                ord.total = Convert.ToDouble(dr["TOTAL"]);
                lista.Add(ord);
            }
            con.Close();
            return lista;
        }
        public Boolean CerrarCocina()
        {
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE ORDERS SET STATE=3 WHERE STATE=2";
            dr = cmd.ExecuteReader();
            if (dr != null)
            {
                dr.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}