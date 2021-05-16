using System;
using Formacion.CSharp.Objects; //Indicar el espacio de nombres donde se encuentre la clase del objeto. 

namespace Formacion.CSharp.ConsoleApp1
{
    class Program
    {
        static void Main(string[] args) //Comienzo de la aplicación.
        {
            //DIFERENTES FORMAS DE DECLARAR UNA VARIABLE DE TIPO OBJETO + ASIGNACIONES:
            Alumno alumno1 = new Alumno(); //Especificando la clase -> ALUMNO. (Valor por defecto -> Null)
            var alumno2 = new Alumno(); //En el momento de la asignación toma tipo.

            Object alumno3 = new Alumno(); //Admite cualquier valor (números, objetos, strings...).
            dynamic alumno4 = new Alumno();
            //Pintar el tipo de la variable:
            Console.WriteLine("Tipo variable 1: " + alumno1.GetType()); //Concatenando.
            Console.WriteLine("Apellido: {0}", alumno1.Apellidos); //Uso de comodines.
            Console.WriteLine($"Tipo variable 2: {alumno2.GetType()}"); //Con el $ para formatear.
            Console.WriteLine(@"Apellido: \\{0}\", alumno2.Apellidos); //Para pintar carácteres especiales como \ (carácter de escape).

            Console.WriteLine("Tipo variable 3: " + alumno3.GetType());
            Console.WriteLine("Apellido: {0}", ((Alumno)alumno3).Apellidos); //Conversión: de tipo object a -> tipo Alumno.

            Console.WriteLine("Tipo variable 4: " + alumno4.GetType());
            Console.WriteLine("Apellido: {0}", ((Alumno)alumno4).Apellidos); //No detecta errores. Conversión para trabajar con el intelliSense.


            //Alumno alumno = new Alumno(); //Instanciar el objeto (creación de variables que contienen objetos).

            //alumno.Apellidos = "Mañé"; //Podemos acceder a la variable pública y modificarla...
            //Console.WriteLine("Apellido: {0}", alumno.Apellidos); //o pintarla.

            int a = 10;
            //Operadores:
            a = a + 1;
            a += 1;
            a++; //Todo lo mismo.


            //CONVERSIONES:
            byte b = 10; //Entero de 8 bits.
            int c = 35; //Entero de 32 bits.
            string d = "43"; //También podemos convertir a alfanumérico.

            c = b; //almacenamos b dentro de c sin problema -> C de mayor tamaño. Conversion implícita (automática). //b = c; //No es posible
            //Conversiones explícitas:
            b = (byte)c; //para meter c dentro de b -> Si c no es mayor a 255 (límite de representación del byte).
            b = Convert.ToByte(c); //DA ERROR si se supera el límite de representación del byte (u otro).
            byte.TryParse(d, out b); //Convertir de texto a byte.


            //ARRAYS:
            //int numero = 10; //Almacenar un número.
            int[] numeros1 = { 10, 13, 6, 30, 40 }; //Definir e inicializar la array.
            int[] numeros2 = new int[10]; //Definir un array e inicializarla. Entre Corchetes el número de elementos. Valores por defecto -> 0.

            numeros1[2] = 500; //Para modificar el valor de una posición.
            Console.WriteLine(numeros1[2]); //Acceso a los elementos del array.

            Alumno[] alumnos1 = new Alumno[11]; //Valor por defecto Null.
            Alumno[] alumnos2 = { new Alumno(), new Alumno (), new Alumno()}; //Array de objetos -> En cada una de las posiciones nuevas instancias del objeto.

            alumnos2[3].Apellidos = "Cerdán"; //Para modificar las propiedad/variables del objeto en la posición [x].
            Console.WriteLine(alumnos2[3].Apellidos); //Acceso a las propiedades/variables del elemento [x] del array.

            //CUANDO CREAMOS UN OBJETO TENEMOS LA POSIBILIDAD DE ASIGNAR VALORES A LAS DIFERENTES PROPIEDADES O VARIABLES PÚBLICAS:
            Alumno alumno10 = new Alumno()
            {
                Apellidos = "Berta" //, para añadir más variables a modificar
            };

            Console.ReadKey(); //Para que se quede parado esperando que pulsemos una tecla
        }
    }
}

namespace Formacion.CSharp.Objects
{
    public class Alumno //Por defecto, clase privada.
    {
        string Nombre = "Aitor"; //Creación de variables que contienen alfanumericos.
        public string Apellidos = "Cerdán"; //Hacemos la variable pública
        int Edad = 26; //Creación de variables que contienen numéricos.
    }
}