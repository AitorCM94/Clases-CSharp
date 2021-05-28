using System;
using System.Threading.Tasks;

namespace Formacion.CSharp.ConsoleAppTareas
{
    class Program
    {
        static void Main(string[] args)
        {
            Task tarea1 = new Task(new Action(Saludo)); //Objeto que contiene código a ejecutar. Action -> Delegado. Saludo -> Método)
            Task tarea2 = new Task(delegate { //Delegado.
                Console.WriteLine("Tarea 2 ejecutandose"); 
            });
            Task tarea3 = new Task(() => { //Función lambda.
                Console.WriteLine("Tarea 3 ejecutandose");
            });
        }

        static void Saludo()
        {
            Console.WriteLine("Hola mundo");
        }
    }
}
