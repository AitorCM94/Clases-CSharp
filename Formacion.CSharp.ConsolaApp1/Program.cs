using System;
using Formacion.CSharp.ConsolaApp1;

namespace Formacion.CSharp.ConsolaApp1
{
    class Program
    {
        static void Main(string[] args) //Inicio del programa.
        {
            Objects.Alumno alumno = new Objects.Alumno(); //Instanciar el objeto.

            Console.WriteLine("Edad: {0}", alumno.Edad);
        }
    }
}

namespace Formacion.CSharp.Objects
{
    public class Alumno
    {
        string Nombre = "Aitor";
        string Apellidos = "Cerdán";
        public int Edad = 26;
    }
}