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
            return View();
        }
    }
}