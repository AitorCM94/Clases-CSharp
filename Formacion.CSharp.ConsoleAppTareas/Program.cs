using System;
using System.Threading;
using System.Threading.Tasks;

namespace Formacion.CSharp.ConsoleAppTareas
{
    //DELEGADOS:
    //Definir el tipo de método que puede contener el delegado (tiene que coincidir en el tipo y número de parámetros, así como en lo que retorna el método).
    delegate void Demo1(); //No devolver nada y no tener parámetros.
    delegate void Demo2(string n); //No devolver nada y con un parámetro.

    class Program
    {
        static void Main(string[] args)
        {
            //TAREAS:
            Console.WriteLine("INICIO DE LA APP");
            CreacionTareas(); //El programa no se para para ejecutar las tareas.
            Console.WriteLine("FIN DE LA APP"); //Fin del hilo principal (código que genera las tareas).

            Console.ReadKey(); //Para que no pare de ejecutarse la consola.
        }

        //DELEGADOS:
        static void Saludo()
        {
            Console.WriteLine("Hola mundo (tarea 1 ejecutandose)");
        }               
        static void Saludo3()
        {
            Console.WriteLine("Hola a todos (tarea 3 ejecutandose)");
        }        
        
        static void Saludo2(string nombre)
        {
            Console.WriteLine($"Hola {nombre}");
        }


        static async void CreacionTareas() //Método en asíncrono para no bloaquear el hilo principal de la aplicación.
        {
            //DELEGADOS:
            //Crear variables del tipo delegado:
            Demo1 demo = Saludo; //Tipo Demo1 -> Almacena un método que no devuelve nada y que no tiene parámetros.
            Demo2 demo2 = Saludo2; //Tipo Demo2 -> Almacena un método que no devuelve nada y tiene un parámetro.

            //Al cambiar el contenido de la variable podemos implementar diferentes métodos:
            demo = Saludo3;

            //Una variable de tipo delegado funciona como un método, podemos invocarlo:
            //demo();

            //TAREAS:
            //1. Instanciar tareas (método de instancia):
            Task tarea1 = new Task(new Action(Saludo)); //Utilizando Action: Objeto global de tipo delegado. Contiene código -> Saludo (Método)
            TaskStatus estado = tarea1.Status; //Guardar el estado de la tarea.

            Task tarea2 = new Task(delegate { //Utilizando un delegado: Tipo de dato que contiene código.
                Console.WriteLine("Tarea 2 ejecutandose");
            });

            Task tarea3 = new Task(new Action(demo)); //Demo -> Método almacenado en una variable de tipo delegado.

            Task<bool> tarea4 = new Task<bool>(delegate { //Cuando las tareas retornan algo, tenemos que especificar el tipo de lo que retornan.
                Console.WriteLine("Tarea 4 ejecutandose");
                Thread.Sleep(3000); //Para que espere 2 segundos.
                Console.WriteLine("Tarea 4 finalizada");
                return true;
            });

            Task tarea5 = new Task(() => { //Utilizando un método anónimo con una expresión lambda.
                Console.WriteLine("Tarea 5 ejecutandose");
            });
            //2. Ponerlas en funcionamiento:
            tarea1.Start();
            tarea2.Start();
            tarea3.Start();
            tarea3.Wait(3000); //Esperar un máximo de 3000 milisegundos = 3 segundo (limitación) antes de pasar a la siguiente. 

            tarea4.Start(); //Antes del wait tenemos que iniciar la tarea.
            tarea4.Wait(2000); //Si no ha finalizado la tarea en 2 segundo ejecuta la siguiente.
            //Pintar el resultado de lo que retorna la tarea: 
            //Console.WriteLine("Resultado de la tarea 4: {0}", tarea4.Result); //NO HARIA FALTA EL WAIT, bloquea el HILO PRINCIPAL.
            Console.WriteLine("Resultado de la tarea 4: {0}", await tarea4); //Únicamente en métodos asíncronos -> Desvincula del HILO PRINCIPAL.

            tarea5.Start();

            //1./2. Crear una tarea y a la vez ponerla en funcionamiento (método estático, sin instanciar):
            Task tarea6 = Task.Run(() => { Console.WriteLine("Tarea 6 ejecutandose"); });

            //Iniciar diferentes tareas en PARALELO:
            Parallel.Invoke(
                () => tarea1.Start(), 
                () => tarea2.Start(), 
                () => tarea3.Start(), 
                () => { Console.WriteLine("Tarea en paralelo"); 
            });
        }
    }
}
