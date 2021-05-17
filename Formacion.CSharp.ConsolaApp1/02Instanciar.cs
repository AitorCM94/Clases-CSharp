using System;
using Formacion.CSharp.Objects; //Indicar el espacio de nombres donde se encuentre la clase del objeto. 

namespace Formacion.CSharp.ConsoleApp1
{
    class Program
    {
        static void Main(string[] args) //Comienzo de la aplicación.
        {
            //Recorrer array con un while y do while.
            string[] fruta2 = {"naranja", "limón", "pomelo", "lima", "fresa", "sandía", "melón" };
            int posicion = 0;
            while (posicion < fruta2.Length)
            {
                Console.WriteLine(fruta2[posicion]);
                posicion += 1;
            }
            int posicion2 = 0;
            do
            {
                Console.WriteLine(fruta2[posicion2]);
                posicion2 += 1;
            } while (posicion2 < fruta2.Length);


            Console.ReadKey();
            //Preguntamos un número
            Console.Write("Número: ");
            string input = Console.ReadLine();
            int numero = Convert.ToInt32(input);

            for (int i = 1; i < 11; i++)
                Console.WriteLine(i * numero);


            //Pintamos la tabla de multiplicar


            
            decimal[] numeros3 = { 10, 5, 345, 55, 13, 1000, 83 };
            //Suma total
            decimal total = 0;
            for (int i = 0; i < numeros3.Length; i++)
            {
                total += numeros3[i];
            }
            int total2 = 0;
            foreach (int num in numeros3)
            {
                total2 += num;
            }

            //Media -> Dividir el total entre len
            decimal media = total / numeros3.Length;
            int media2 = total2 / numeros3.Length;

            //Max y min
            decimal max = 0;
            decimal min = numeros3[0];
            for (int i = 0; i < numeros3.Length; i++)
            {
                if (numeros3[i] > max) max = numeros3[i];
                if (numeros3[i] < min) min = numeros3[i];
            }

            
            Reserva reserva = new Reserva();

            Console.Write("ID de la Reserva: ");
            reserva.id = Console.ReadLine();

            Console.Write("Nombre del Cliente: ");
            reserva.cliente = Console.ReadLine();

            // 100: Habitación Individual  200: Habitación Doble  300: Junior Suite  400: Suite
            Console.Write("Tipo de Reserva: ");
            //reserva.tipo = Convert.ToInt32(Console.ReadLine());
            string respuesta = Console.ReadLine();
            int.TryParse(respuesta, out reserva.tipo);

            Console.Write("Es Fumador ? ");
            //reserva.fumador = Convert.ToBoolean(Console.ReadLine());
            string respuesta2 = Console.ReadLine();
            //if (respuesta2.ToLower().Trim() == "si" || respuesta2.ToLower().Trim() == "sí") reserva.fumador = true; else reserva.fumador = false;
            //reserva.fumador = (respuesta2.ToLower().Trim() == "si" || respuesta2.ToLower().Trim() == "sí") ? true : false;
            switch (respuesta2.ToLower().Trim())
            {
                case "si":
                    reserva.fumador = true;
                    break;
                case "sí":
                    reserva.fumador = true;
                    break;
                default:
                    reserva.fumador = false;
                    break;
            }

            Console.Clear();

            //Pinta el Número de la Reserva:
            Console.WriteLine($"ID de la Reserva: {reserva.id}");

            //Pinta Nombre del Cliente:
            Console.WriteLine($"Nombre del Cliente: {reserva.cliente}");

            //Pinta el tipo de reserva en texto:
            if (reserva.tipo == 100)
                Console.WriteLine("Habitación individual.");
            else if (reserva.tipo == 200)
                Console.WriteLine("Habitación doble.");
            else if (reserva.tipo == 300)
                Console.WriteLine("Junior Suite");
            else if (reserva.tipo == 400)
                Console.WriteLine("Suite.");
            else
                Console.WriteLine("No es correcto.");
            
            //Pinta si es fumador en texto:
            switch(reserva.fumador)
            {
                case true:
                    Console.WriteLine("Es fumador.");
                    break;
                case false:
                    Console.WriteLine("No es fumador.");
                    break;
            }
            

            Console.ReadKey();
            //DIFERENTES FORMAS DE DECLARAR UNA VARIABLE DE TIPO OBJETO. Y ASIGNACIONES:
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

            //Console.ReadKey(); //Para que se quede parado esperando que pulsemos una tecla
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

    public class Reserva

    {

        public string id;

        public string cliente;



        // 100: Habitación Individual  200: Habitación Doble  300: Junior Suite  400: Suite

        public int tipo;

        public bool fumador;

    }
}