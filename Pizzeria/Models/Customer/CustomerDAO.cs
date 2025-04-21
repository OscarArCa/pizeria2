using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Pizzeria.Models.Customer
{
    public class CustomerDAO
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

        public Boolean AgregarCliente(Customer cus)
        {

            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO CUSTOMER (DNI,NAME,LAST_NAME) VALUES('" + cus.dni + "','" + cus.name + "','" + cus.last_name + "')";
            int row = cmd.ExecuteNonQuery();
            if (row>0)
            {
                con.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
        public int MostrarIdCustomer(string dni)
        {
            int id = 0;
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT CUSTOMER_ID FROM CUSTOMER WHERE DNI='" + dni + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                id = Convert.ToInt32(dr["CUSTOMER_ID"]);
            }
            dr.Close();
            return id;
        }
    }
}