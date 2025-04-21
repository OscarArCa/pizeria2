using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pizzeria.Models.Orders;

namespace Pizzeria.Controllers
{
    public class CocinaController : Controller
    {
        OrdersDAO ordersDAO = new OrdersDAO();
        // GET: Cocina
        public ActionResult Cocina()
        {
            List<Orders> lista = new List<Orders>();
            lista=ordersDAO.MostrarOrdenesCocina();
            return View(lista);
        }
        public ActionResult Cocinado(int id)
        {
            if (ordersDAO.CocinarOrden(id))
            {
                TempData["Mensaje"] = "Pedido marcado como listo correctamente.";

            }
            else
            {
                TempData["Mensaje"] = "Ocurrio un error.";
            }
            return RedirectToAction("Cocina", "Cocina");
        }
        public ActionResult Cerrar()
        {
            return RedirectToAction("Cocinados", "CerrarCocina");
        }
    }
}