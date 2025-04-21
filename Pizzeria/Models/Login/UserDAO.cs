using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Pizzeria.Models.Login
{
    public class UserDAO
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

        public User ConsultarUsuario(string user)
        {
            User usu = null;

           ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM  USER WHERE USERNAME = '"+user+"'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                usu = new User();
                usu.user_id = Convert.ToInt32(dr["USER_ID"]);
                usu.username = dr["USERNAME"].ToString();
                usu.password = dr["PASSWORD"].ToString();

            }
            con.Close();
            return usu;
        }
        public Boolean VerificarUsuario(string user,string pass)
        {
            User usu=ConsultarUsuario(user);
            if (usu != null && usu.username == user && usu.password == pass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean AgregarUsuario(User usu)
        {

           ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO USER (USERNAME,PASSWORD) VALUES('" + usu.username + "','" + usu.password + "')";
            int row = cmd.ExecuteNonQuery();
            if (row > 0)
            {
                con.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}