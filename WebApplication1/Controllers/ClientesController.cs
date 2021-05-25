using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class ClientesController : Controller
    {
        ModelNorthwind context;

        //Mostrar listado de clientes:
        public IActionResult Index()
        {
            var clientes = context.Customers.ToList();
            //Pasar a vista:
            //ViewBag.Clientes = clientes;
            ViewBag.Title = "Listado de Clientes";
            return View(clientes);
        }

        public IActionResult Ficha(string id)
        {
            var cliente = context.Customers
                .Where(r => r.CustomerID == id)
                .FirstOrDefault();

            ViewBag.Title = $"Ficha de {cliente.CompanyName}";

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Grabar(Customers cliente)
        {
            context.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return View("Ficha", cliente);
            //return RedirectToAction("Ficha", new { id = cliente.CustomerID });
            //return RedirectToAction
        }

        public ClientesController(ModelNorthwind context)
        {
            this.context = context;
        }
    }
}
