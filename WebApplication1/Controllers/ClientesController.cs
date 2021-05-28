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
        //Accedemos al contexto ya instanciado en Startup:
        ModelNorthwind context; //Creamos la variable contexto. 
        
        public IActionResult Index()
        {
            //var context = new ModelNorthwind(); Evitamos la opción de instanciar la clase contexto en cada método.

            //1. Mostrar listado de clientes:
            var clientes = context.Customers.ToList();
            //2. Pasar el listado de clientes a la vista:
            //ViewBag.Clientes = clientes; //Opción 1.
            ViewBag.Title = "Listado de Clientes"; //Pasar un valor para el título de la página.
            return View(clientes); //Opción 2: Pasarlo como modelo de datos. Retornamos el objeto clientes para mostrarlo en la vista Index.
        }

        //Creamos un nuevo método para el hipervínculo:
        [HttpGet] //Limitamos el método en modo get.
        public IActionResult Ficha(string id) //Recibe por parámetro el identificador (definido en la url del Startup).
        {
            //Para pintar la ficha del cliente buscamos en la base de datos:
            var cliente = context.Customers
                .Where(r => r.CustomerID == id) //Que coincida con el id que pasa por parámetro.
                .FirstOrDefault();

            ViewBag.Title = $"Ficha de {cliente.CompanyName}"; //Modificamos el título para la página.

            return View(cliente); //Retornamos el objeto cliente para mostrarlo en la vista Ficha.
        }

        //Método para el botón de grabar:
        [HttpPost] //Limitar el método en modo post.
        public IActionResult Grabar(Customers modelCli) //Recibe el formulario web, representado por el modelo de datos de la vista.
        {
            //Actualizar datos que no provienen de la base de datos:
            context.Entry(modelCli).State = Microsoft.EntityFrameworkCore.EntityState.Modified; //Cambiar el estado a modificado.
            context.SaveChanges(); //Confirmamos los cambios.

            return View("Ficha", modelCli); //Retorno la vista ficha y pasamos como modelo de datos modelCli.
            //El redirect volvería a ejecutar el método Ficha:
            //return RedirectToAction("Ficha", new { id = cliente.CustomerID }); //El método requiere el id.
            //return RedirectToAction("Index"); //Para volver al listado después de grabar.
        }

        //Constructor de la clase:
        public ClientesController(ModelNorthwind context) //Añadimos el contexto entre paréntesis.
        {
            this.context = context; //El this.context del objeto es igual al context que recivimos por parámetro.
        }
    }
}
