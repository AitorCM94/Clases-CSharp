using System;
using Formacion.CSharp.Objects; //Indicar el espacio de nombres donde se encuentre la clase del objeto. 

namespace Formacion.CSharp.ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Alumno alumno = new Alumno(); //Instanciar el objeto (creación de variables que contienen objetos).

            Console.WriteLine("Edad: {0}", alumno.Apellidos); //Podemos acceder a la variable pública.
        }
    }
}

namespace Formacion.CSharp.Objects
{
    class Alumno //Por defecto, clase privada.
    {
        string Nombre = "Aitor"; //Creación de variables que contienen alfanumericos.
        public string Apellidos = "Cerdán"; //Hacemos la variable pública
        int Edad = 46; //Creación de variables que contienen numéricos.
    }
}