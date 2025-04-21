using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pizzeria.Models.Boleta_cab;
using Pizzeria.Models.Boleta_det;
using Pizzeria.Models.Orders;
using Pizzeria.Models.Serie;

namespace Pizzeria.Controllers
{

    public class OrdersController : Controller
    {
        OrdersDAO ordersDAO = new OrdersDAO();
        

        // GET: Pedidos
        public ActionResult Orders()
        {
            List<Orders> lista = new List<Orders>();
            lista = ordersDAO.MostrarOrdenesRegistradas();
            return View(lista);
        }

        public ActionResult Pagar(int id ,int s, string dni,int cant, double total, int pizza_id)
        {
            if (ordersDAO.PagarOrden(id)){
                Serie serie = new Serie();
                SerieDAO serieDAO = new SerieDAO();
                Boleta_cabDAO boleta_cabDAO = new Boleta_cabDAO();
                Boleta_cab boleta_cab = new Boleta_cab();
                Boleta_det boleta_det = new Boleta_det();
                Boleta_detDAO boleta_detDAO = new Boleta_detDAO();
                //Generamos la serie
                serie.serie_id = s;
                serie = serieDAO.Serie(serie);
                serieDAO.ActualizarSerie(serie);
                string numbol=serieDAO.GenerarNumBolDesdeSerie(serie);
                //Generamos la cabecera de la boleta
                boleta_cab.NumBol = numbol;
                boleta_cab.Fecha = DateTime.Now;
                boleta_cab.DNIcli = dni;
                boleta_cab.Igv = total * 0.18;
                boleta_cab.Total = total + boleta_cab.Igv;
                boleta_cab.Estado = "0";
                //Generamos el detalle de la boleta
                boleta_det.NumBol = numbol;
                boleta_det.CodProd = pizza_id;
                boleta_det.Cant = cant;
                boleta_det.Punt = total/cant;
                boleta_det.Importe = total;
                //Creamos nuestras boleta cab y boleta det
                boleta_cabDAO.AgregarBoletaCab(boleta_cab);
                boleta_detDAO.AgregarBoletaDet(boleta_det);



                TempData["Mensaje"] = "Pedido pagado correctamente.";
                
            }
            else
            {
                TempData["Mensaje"] = "No se pudo pagar el pedido.";
 
            }
            return RedirectToAction("Orders", "Orders");
        }
    }
}