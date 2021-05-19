using System;
using System.Collections; //Espacio de nombres para trabajar con colecciones.
using System.Collections.Generic;

namespace Formacion.CSharp.ConsoleAppDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary();
        }

        static void Array()
        {
            int[] array = new int[10];
            Console.WriteLine($"Número de elementos: {array.Length}");
        }

        static void ArrayList()
        {
            var arrayList = new ArrayList(); //MANEJA DIFERENTES TIPOS DE OBJETO.

            //Añadir elementos:
            arrayList.Add("azul");
            arrayList.Add("verde");
            arrayList.Add("negro");
            arrayList.Add("amarillo");
            arrayList.Add("blanco");

            //Añadir varios elementos a la vez:
            var colors = new string[] { "green", "black", "pink" };
            arrayList.AddRange(colors);

            //Número de elementos:
            Console.WriteLine($"Número de elementos: {arrayList.Count}");

            //Para recorrer:
            foreach (var c in arrayList)
            {
                Console.WriteLine("Item: {0}", c);
            }
            for (var i = 0; i < arrayList.Count; i++)
            {
                Console.WriteLine("Item: {0}", arrayList[i]);
            }

            //Eliminar un elemento:
            arrayList.Remove("verde");
            arrayList.RemoveAt(4);
            arrayList.RemoveRange(2, 2);
            //Eliminar todos los elementos de la colección:
            arrayList.Clear();
        }

        static void HashTable()
        {
            //HashTable -> Diccionario
            var dicc = new Hashtable(); //MANEJA DIFERENTES TIPOS DE OBJETO.

            //Añadir elementos:
            dicc.Add("azul", "blue");
            dicc.Add("verde", "green");
            dicc.Add("negro", "black");
            dicc.Add("amarillo", "yellow");
            dicc.Add("blanco", "white");

            //Número de elementos:
            Console.WriteLine($"Número de elementos: {dicc.Count}");

            //Para recorrer:
            foreach (var clave in dicc.Keys)
            {
                Console.WriteLine($"Clave: {clave} -> {dicc[clave]}"); //La posición es la clave.
            }

            //Eliminar un elemento:
            dicc.Remove("verde");
            //Eliminar todos los elementos de la colección:
            dicc.Clear();
        }

        static void List()
        {
            var lista = new List<string>(); //Al instanciar se tiene que especificar el tipo.

            //Añadir elementos:
            lista.Add("azul");
            lista.Add("verde");
            lista.Add("negro");
            lista.Add("amarillo");
            lista.Add("blanco");

            //Número de elementos:
            Console.WriteLine($"Número de elementos: {lista.Count}");

            //Para recorrer:
            foreach (string item in lista)
            {
                Console.WriteLine("Colores: {0}", item);
            }
            for (var i = 0; i < lista.Count; i++)
            {
                Console.WriteLine("Item: {0}", lista[i]);
            }

            //Eliminar un elemento:
            lista.Remove("verde");
            lista.RemoveAt(3);
            //Eliminar todos los elementos de la colección:
            lista.Clear();
        }

        static void Dictionary()
        {
            var dicc = new Dictionary<int, string>(); //Al instanciar se tiene que especificar el tipo.

            //Añadir elementos:
            dicc.Add(3, "blue");
            dicc.Add(6, "green");
            dicc.Add(1, "black");
            dicc.Add(5, "yellow");
            dicc.Add(80, "white");

            //Número de elementos:
            Console.WriteLine($"Número de elementos: {dicc.Count}");

            //Para recorrer:
            foreach (var clave in dicc.Keys)
            {
                Console.WriteLine($"Clave: {clave} -> {dicc[clave]}"); //La posición es la clave.
            }

            //Eliminar un elemento:
            dicc.Remove(6);
            //Eliminar todos los elementos de la colección:
            dicc.Clear();
        }

        static void Otros()
        {
            var cola = new Queue();
            cola.Enqueue("añadir");
            cola.Dequeue(); //Elimina el elemento de la primera posición.

            var stack = new Stack();
            stack.Push("añadir");
            stack.Pop(); //Elimina el elemento del final.
        }
    }
}