using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzeria.Models.Boleta_cab
{
    public class Boleta_cab
    {
        public string NumBol { get; set; }
        public DateTime Fecha{ get; set; }
        public string DNIcli { get; set; }
        public double Igv { get; set; }
        public double Total { get; set; }
        public string Estado { get; set; }
    }
}