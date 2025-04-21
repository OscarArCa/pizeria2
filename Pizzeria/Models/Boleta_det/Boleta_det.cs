using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzeria.Models.Boleta_det
{
    public class Boleta_det
    {
        public string NumBol { get; set; }
        public int CodProd { get; set; }
        public double Cant { get; set; }
        public double Punt { get; set; }
        public double Importe { get; set; }
    }
}