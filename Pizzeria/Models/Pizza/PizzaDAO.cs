using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Pizzeria.Models.Pizza
{
    public class PizzaDAO
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
        public List<Pizza> MostrarPizzas()
        {
            List<Pizza> lista = new List<Pizza>();
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM PIZZA";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Pizza p = new Pizza();
                p.pizza_id = Convert.ToInt32(dr["PIZZA_ID"]);
                p.name = dr["NAME"].ToString();
                p.price = Convert.ToDouble(dr["PRICE"]);
                lista.Add(p);
            }
            con.Close();
            return lista;
        }
        public Double MostrarPrecioPizza(int pizza_id)
        {
            Double precio = 0;
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT PRICE FROM PIZZA WHERE PIZZA_ID='" + pizza_id + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                precio = Convert.ToDouble(dr["PRICE"]);
            }
            dr.Close();
            return precio;
        }


    }
}