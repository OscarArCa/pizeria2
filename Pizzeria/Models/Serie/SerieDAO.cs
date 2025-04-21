using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Pizzeria.Models.Serie
{
    public class SerieDAO
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
        public Serie Serie(Serie ser)
        {
            Serie serie = new Serie();

            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM  SERIE WHERE SERIE_ID = " + ser.serie_id + "";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                serie.serie_id = Convert.ToInt32(dr["SERIE_ID"]);
                serie.name = dr["NAME"].ToString();
                serie.quantity = Convert.ToInt32(dr["QUANTITY"]);

            }
            con.Close();
            return serie;


        }
        public Boolean ActualizarSerie(Serie ser)
        {
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE SERIE SET QUANTITY = QUANTITY + 1 WHERE SERIE_ID="+ser.serie_id+"";
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
        public string GenerarNumBolDesdeSerie(Serie ser)
        {
            
            Serie serieActual = Serie(ser); 

           
            string correlativo = (serieActual.quantity + 1).ToString("D4");

            
            return serieActual.name + "-" + correlativo;
        }

    }
}