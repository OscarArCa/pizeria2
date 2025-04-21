using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pizzeria.Models.Orders;

namespace Pizzeria.Controllers
{

    public class CerrarCocinaController : Controller
    {
        // GET: CerrarCocina
        public ActionResult Cocinados()
        {
            List<Orders> lista = new List<Orders>();
            OrdersDAO ordersDAO = new OrdersDAO();
            lista = ordersDAO.MostrarOrdenesCerrarCocina();

            int totalPizzas = lista.Sum(o => o.quantity);

            ViewBag.TotalPizzas = totalPizzas;

            return View(lista);
        }
        public ActionResult Cerrar()
        {
            OrdersDAO ordersDAO = new OrdersDAO();
            if (ordersDAO.CerrarCocina())
            {
                TempData["Mensaje"] = "Cocina cerrada correctamente.";
                return RedirectToAction("Cocinados");
            }
            else
            {
                TempData["Mensaje"] = "Error al cerrar la cocina.";
                return RedirectToAction("Cocinados");
            }
        }
    }
}