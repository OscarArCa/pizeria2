using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mysqlx.Crud;
using Pizzeria.Models.Customer;
using Pizzeria.Models.Orders;
using Pizzeria.Models.Pizza;


namespace Pizzeria.Controllers
{

    public class HomeController : Controller
    {
        PizzaDAO pizzaDAO = new PizzaDAO();
        
        public ActionResult Index()
        {
            List<Pizza> lista = pizzaDAO.MostrarPizzas();
            ViewBag.Pizzas = new SelectList(lista, "pizza_id","name_price");
            return View();
        }

        [HttpPost]
        public ActionResult Create(string dni, string name, string last_name,int pizza_id, int cant)
        {
            Customer customer = new Customer();
            CustomerDAO customerDAO = new CustomerDAO();
            Orders orders = new Orders();
            OrdersDAO ordersDAO = new OrdersDAO();

            customer.dni = dni;
            customer.name = name;
            customer.last_name = last_name;

            if (customerDAO.AgregarCliente(customer))
            {
                orders.quantity = cant;
                orders.total = cant * pizzaDAO.MostrarPrecioPizza(pizza_id);
                orders.state = 0;
                orders.customer_customer_id = customerDAO.MostrarIdCustomer(dni);
                orders.pizza_pizza_id = pizza_id;
                if(ordersDAO.AgregarOrden(orders))
                {
                    TempData["Mensaje"] = "Pedido registrado correctamente.";
                    ViewBag.Pizzas = new SelectList(pizzaDAO.MostrarPizzas(), "pizza_id", "name_price");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Mensaje"] = "No se pudo registar el pedido correctamente.";
                    ViewBag.Pizzas = new SelectList(pizzaDAO.MostrarPizzas(), "pizza_id", "name_price");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Mensaje"] = "No se pudo registrar al cliente.";
                ViewBag.Pizzas = new SelectList(pizzaDAO.MostrarPizzas(), "pizza_id", "name_price");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}