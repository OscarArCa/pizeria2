using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Pizzeria.Models.Boleta_det
{
    public class Boleta_detDAO
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
        public Boolean AgregarBoletaDet(Boleta_det bol)
        {
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO BOLETA_DET (NUMBOL,CODPROD,CANT,PUNT,IMPORTE) " +
                "VALUES('" + bol.NumBol + "'," + bol.CodProd + "," + bol.Cant + "," + bol.Punt + "," + bol.Importe + ")";
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