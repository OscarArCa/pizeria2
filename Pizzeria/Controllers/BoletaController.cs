using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pizzeria.Models.Boleta;
using Pizzeria.Models.Boleta_cab;

namespace Pizzeria.Controllers
{

    public class BoletaController : Controller
    {
        // GET: Boleta
        public ActionResult Boletas()
        {
            List<Boleta> boletas = new List<Boleta>();
            BoletaDAO boletaDAO = new BoletaDAO();
            boletas = boletaDAO.MostrarBoletas();
            int totalBoletas = boletas.Select(b => b.numero_boleta).Distinct().Count();

            ViewBag.TotalBoletas = totalBoletas;
            return View(boletas);
        }
        public ActionResult Cerrar()
        {
           Boleta_cabDAO boleta_CabDAO = new Boleta_cabDAO();
            if (boleta_CabDAO.ActualizarEstadoBoleta())
            {
                TempData["Mensaje"] = "Caja cerrada exitosamente.";

            }
            else
            {
                TempData["Mensaje"] = "No se pudo cerrar la caja.";

            }
            return RedirectToAction("Boletas");
        }
        public ActionResult Descargar(string orden, string nombre, string dni, string fecha, string codPro, string nombrePro, string cantidad, string precioUni, string imp, string igv, string total)
        {
            BoletaDAO dao = new BoletaDAO();

            string rutaPDF = dao.GenerarBoletaPDF(orden, nombre, dni, fecha, codPro, nombrePro, cantidad, precioUni, imp, igv, total);

            byte[] fileBytes = System.IO.File.ReadAllBytes(rutaPDF);

            string fileName = "Boleta_" + orden + ".pdf";

            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);

            return File(fileBytes, "application/pdf");
        }



    }
}