using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formacion.CSharp.ConsoleAppTareas2
{
    class Program
    {
        static void Main(string[] args) //El método Main no puede ser asíncrono.
        {
            Console.WriteLine("Inicio APP");
            MainParallel();
            Console.WriteLine("Fin APP");
            Console.ReadKey();
        }

        static async void MainAsync() //Metodo asíncrono estático que utilizamos en vez del Main.
        {
            //Instanciamos el objeto donde estan los métodos:
            var pruebas = new PruebaTask();

            //Usar método sincrono:
            //pruebas.Caculos(); //Hacemos los cáclulos.
            //for (int i = 0; i < pruebas.array.LongLength; i++) //Pintamos el resultado
            //{
            //    Console.WriteLine($"Raíz cuadrada de {i}: {pruebas.array[i]}");
            //}

            //Usar método asíncrono -> Ejecutamos el cálculo y el pintar a la vez:
            //bool resultado = pruebas.CaculosAsync().Result; //Hacemos los cáclulos. Bloquearía el hilo principal.
            //bool resultado = await pruebas.CaculosAsync(); //Hacemos los cáclulos. Esperamos por el resultado sin bloquear el hilo principal.
            //for (int i = 0; i < pruebas.array.LongLength; i++) //Pintamos el resultado
            //{
            //    Console.WriteLine($"Raíz cuadrada de {i}: {pruebas.array[i]}");
            //}

            //Usar método que retorna una tarea:
            //Task tarea = pruebas.CaculosAsync3(); //Instanciamos la tarea.
            //tarea.Start(); //Iniciamos la tarea.
            //tarea.Wait(); //Esperamos que la tarea finalice. Bloqueo del hilo principal.
            //for (int i = 0; i < pruebas.array.LongLength; i++) //Pintamos el resultado
            //{
            //    Console.WriteLine($"Raíz cuadrada de {i}: {pruebas.array[i]}");
            //}

            //Usar método asíncrono con un EVENTO:
            //Metemós el código a ejecutar dentro del evento:
            pruebas.FinCalculos += ((sender, argumentos) =>
            {
                for (int i = 0; i < pruebas.array.LongLength; i++) //Pintamos el resultado.
                {
                    Console.WriteLine($"Raíz cuadrada de {i}: {pruebas.array[i]}");
                }
            });
            pruebas.CaculosAsync4(); //Hacemos los cáclulos. Siempre invocara el evento.
        }

        //EJECUCIÓN EN PARALELO:
        static void MainParallel()
        {
            double[] array = new double[50000000];
            List<string> frutas = new List<string>() 
            {
                "manzana",
                "pera",
                "fresa",
                "melón",
                "plátano",
            };
            //Recorrer la lista con un foreach:
            foreach (var item in frutas)
            {
                Console.WriteLine($"Fruta: {item}");
            }
            Console.WriteLine("");
            //Recorrer la lista en paralelo -> No procesa los elementos de la lista en un orden:
            Parallel.ForEach(frutas, item =>
            {
                Console.WriteLine($"Paralelo Fruta: {item}");
            });

            //Búsquedas con LINQ:
            var frutas2 = from c in frutas
                          where c[0] == 'm'
                          select c; //Buscar aquellas frutas cuya primera letra sea m.
            //Búsquedas en paralelo con PLINQ:
            var frutas3 = from c in frutas.AsParallel() //Convertimos las colecciones en paralelo
                          where c[0] == 'm'
                          select c; //Más rápida, el resultado no está en el orden.


            Console.ReadKey();

            //Ejecutar métodos en paralelo con un for (contador):
            DateTime a1 = DateTime.Now; //Captura el tiempo.
            //Cálculo con un for normal:
            for (int i = 0; i < 50000000; i++) array[i] = Math.Sqrt(i); //Cálculo de la raíz cuadrada en síncrono.
            DateTime a2 = DateTime.Now;
            //Cálculo en paralelo:
            Parallel.For(0, 50000000, i => { //Rango y función lambda a ejecutar.
                array[i] = Math.Sqrt(i);
                //Console.WriteLine($"Raíz cuadrada de {i}: {array[i]}");
            });
            DateTime a3 = DateTime.Now;

            //Cálculo de los tiempo de ejecución:
            Console.WriteLine("FOR -> {0}", a2.Subtract(a1).Milliseconds.ToString()); //Resta el tiempo de a1 a a2.
            Console.WriteLine("PARALLEL.FOR -> {0}", a3.Subtract(a2).Milliseconds.ToString());
        }
    }


    class PruebaTask
    {
        //MÉTODOS PARA CALCULAR LA RAÍZ CUADRADA DE MUCHOS NÚMEROS:
        public double[] array = new double[50000000]; //Array de números.

        //Definir un EVENTO:
        public event EventHandler<bool> FinCalculos;

        //Método de ejecución Sincrona:
        public bool Caculos()
        {
            for (int i = 0; i < 50000000; i++)
            {
                array[i] = Math.Sqrt(i); //Clacular la raíz cuadrada en cada una de las posiciones de la array.
            }

            return true; //Método que retorna true (bool).
        }


        //Método de ejecución Asíncrono 1:
        public async Task<bool> CaculosAsync()
        {
            //Creamos una tarea y la iniciamos:
            Task<bool> tarea = Task<bool>.Run(() =>
            {
                for (int i = 0; i < 50000000; i++)
                {
                    array[i] = Math.Sqrt(i);
                }

                return true;
            });

            return await tarea; //Retornamos la taerea.
        }
        //Método de ejecución Asíncrono 2 (Simplificado):
        public async Task<bool> CaculosAsync2()
        {
            //No necesitamos la variable, retornamos directamente la tarea con el resultado:
            return await Task<bool>.Run(() =>
            {
                for (int i = 0; i < 50000000; i++)
                {
                    array[i] = Math.Sqrt(i);
                }

                return true;
            });
        }

        //Método que retorna una tarea sin iniciar:
        public Task<bool> CaculosAsync3() //Quitamos async ya que no ejecutamos la tarea.
        {
            //Pasamos la tarea para poder arrancarla o no:
            return new Task<bool>(() =>
            {
                for (int i = 0; i < 50000000; i++)
                {
                    array[i] = Math.Sqrt(i);
                }

                return true;
            });
        }

        //El final de este método se controla mediante un evento:
        public Task<bool> CaculosAsync4() //Quitamos async ya que no ejecutamos la tarea.
        {
            //Pasamos la tarea para poder arrancarla o no:
            return Task.Run<bool>(() =>
            {
                for (int i = 0; i < 50000000; i++)
                {
                    array[i] = Math.Sqrt(i);
                }

                //Interruptor del evento -> Se lanza el evento:
                FinCalculos?.Invoke(this, true);

                return true;
            });
        }
    }
}
