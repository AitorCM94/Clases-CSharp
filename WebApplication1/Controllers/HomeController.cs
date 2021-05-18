using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller // Classe HomeController que hereda de Controller. [hijo] : [padre]
    {
        public IActionResult Index(int id) //Método Index que retorna IActionResult (tipo de objeto interfaz)
        {
            //Traspasamos información a la vista utilizada ViewBag
            ViewBag.numero = id;
            ViewBag.mensaje = $"Tabla de Multiplicar del {id}";

            //Traspasamos información como modelo de datos
            return View(id); //Todos los métodos tienen que estar asociados a una vista.
        }

        public IActionResult Demo()
        {
            return View();
        }
    }
}