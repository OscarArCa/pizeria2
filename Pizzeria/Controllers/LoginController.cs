using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pizzeria.Models;
using Pizzeria.Models.Login;

namespace Pizzeria.Controllers
{
    public class LoginController : Controller
    {
        UserDAO usuDAO = new UserDAO();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
           User usu = usuDAO.ConsultarUsuario(user);
           if(usu == null)
            {
                ViewBag.Mensaje = "Usuario no registrado";
                return View("Login");
            }
            else
            {
                if (usuDAO.VerificarUsuario(user, pass))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Mensaje = "Usuario o contraseña incorrectos";
                    return View("Login");
                }
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User model)
        {
            if (usuDAO.AgregarUsuario(model))
            {
                TempData["Mensaje"] = "Usuario registrado correctamente.";
                return RedirectToAction("Register");
            }
            else
            {
                TempData["Mensaje"] = "No se pudo registrar al Usuario.";
                return RedirectToAction("Register");
            }
        }
    }
}