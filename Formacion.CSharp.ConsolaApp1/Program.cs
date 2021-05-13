using System; //import -> using. System es el Espacio de Nombre.
using Formacion.CSharp.ConsolaApp1;

namespace Formacion.CSharp.ConsolaApp1 //Espacio de Nombres -> Contiene las diferentes clases/objetos.
{
    class Program //Clase inicial. Siempre trabajamos con clases.
    {
        static void Main(string[] args) //Método (función) estático -> Donde se inicia el programa.
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