using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Pizzeria.Models.Boleta_cab
{
    public class Boleta_cabDAO
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
        public Boolean AgregarBoletaCab(Boleta_cab bol)
        {
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO BOLETA_CAB (NUMBOL,FECHA,DNICLI,IGV,TOTAL,ESTADO) " +
                "VALUES('" + bol.NumBol + "','" + bol.Fecha.ToString("yyyy-MM-dd HH:mm:ss") + "'," + bol.DNIcli + "," + bol.Igv + "," + bol.Total + ","+bol.Estado+")";
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
        public Boolean ActualizarEstadoBoleta()
        {
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE BOLETA_CAB SET ESTADO = 1 WHERE ESTADO = 0";
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