using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Models; //2. Instanciar el objeto contexto de la base de datos.
using Newtonsoft.Json; //Para enviar el objeto encontrado en JSON.

namespace WebApplication3.api.v1._0
{
    /// <summary>
    /// Summary description for Empleados
    /// </summary>
    public class Empleados : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try //POR SI LA LLAMADA SE PRODUCE DE MANERA INCORRECTA.
            {
                //1. Coger el parámetro id de la url, que determina el id del empleado:
                int id = Convert.ToInt32(context.Request.Params["id"]); //Lo obtenemos del contexto (entorno de trabajo), del mensaje de petición.

                //2. Para buscar el id en la base de datos:
                //Instanciar el contexto de la base de datos:
                var db = new ModelNorthwind();

                db.Configuration.LazyLoadingEnabled = false; //Todo lo marcado como virtual lo Carga.

                //Buscar el empleado en la base de datos:
                var empleado = db.Employees
                    .Where(r => r.EmployeeID == id) //Que coincida el identificador del empleado con el id (url).
                    .FirstOrDefault();

                //4. Resultados de la búsqueda:
                if (empleado == null) //Empleado no encontrado.
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Empleado no encontrado.");
                    context.Response.StatusCode = 200; //Mandar el código de estado de la conexión.
                }
                else //Empleado encontrado.
                {
                    context.Response.ContentType = "application/json"; //Objeto en formato JSON.
                    context.Response.Write(JsonConvert.SerializeObject(empleado)); //Enviamos el objeto encontrado convertido en JSON.
                    context.Response.StatusCode = 200;
                }
            }
            catch (Exception e)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(e.Message); //Enviamos el mensaje del error.
                context.Response.StatusCode = 500;
            }
        }

        //Propiedad para ver si es reusable:
        //Si cada vez que hay una petición se instancia de la clase y se invoca el método ProcessRequest.
        //O si se instancia una vez y usamos siempre la misma.
        public bool IsReusable
        {
            get
            {
                return false; //Cada vez que hay una petición se instancia.
                //True -> Siempre instanciado. No siempre es válido un valor a true.
            }
        }
    }
}