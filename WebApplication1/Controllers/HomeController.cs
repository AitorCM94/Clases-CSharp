using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller // Classe HomeController que hereda de Controller. [hijo] : [padre]
    {
        public IActionResult Index(int id) //Método [Index] que retorna IActionResult (tipo de objeto interfaz) -> Parámetros entre paréntesis.
        {
            //Traspasamos información a la vista utilizada ViewBag:
            ViewBag.numero = id;
            ViewBag.mensaje = $"Tabla de Multiplicar del {id}"; //Esta información la tenemos automáticamente en la vista.

            //Traspasamos información a la vista como MODELO DE DATOS:
            return View(id); //Todos los métodos tienen que estar asociados a una vista.
        }

        public IActionResult Demo() //Un segundo método: Demo (vista)
        {
            return View(); //Retorno de la función (elemento heredado de controller).
        }
    }
}