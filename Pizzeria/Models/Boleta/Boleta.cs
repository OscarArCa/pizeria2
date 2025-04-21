using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzeria.Models.Boleta
{
    public class Boleta
    {
        public string numero_boleta { get; set; }
        public string fecha { get; set; }
        public string dni_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string apellido_cliente { get; set; }
        public int cod_producto { get; set; }
        public string nombre_producto { get; set; }
        public int cantidad_producto { get; set; }
        public double precio_producto { get; set; }
        public double sub_total { get; set; }
        public double igv { get; set; }
        public double total { get; set; }
        public string estado { get; set; }
    }
}