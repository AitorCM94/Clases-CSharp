using System;
using System.Collections.Generic;

namespace CSharp.Herencia.ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instanciamos un objeto de la clase DemoB.
            var demo = new DemoB();

            demo.Nombre = "Aitor";
            demo.Apellidos = "Cerdán";
            demo.Edad = 13;

            //demo.PintaDatos();
            demo.PintaDatosHijo();
            demo.PintaDatosPadre();


            //Compatibilidad: La compatibilidad es con la BASE. Los derivados entre si son incompatibles.
            DemoB b = new DemoB();
            DemoA a = new DemoA(); //Las variables tienen que contener objetos iguales.
            DemoA a2 = new DemoB(); //Las clases base son compatibles con las clases derivadas.
            //DemoB b2 = new DemoA(); //Clase derivada contiene clase base. NO
            //DemoC c = new DemoA(); //Clases no heredadas. NO
            //Sucede igual con el uso de interfaces:
            Lavadora lavadora = new Lavadora();
            IElectrodomestico Ilavadora = new Lavadora(); //Una variable de una interfaz puede contener cualquier objeto de sus clases derivadas.
            Nevera nevera = new Nevera(); //Estos objetos tienen acceso a toda la funcionalidad de la clase Nevera.
            IElectrodomestico Inevera = new Nevera(); //Estos objetos solo tendran acceso a los miembros mínimos que define la interfaz.
            Ilavadora = lavadora;
            Ilavadora = nevera; //Una variale de tipo interfaz puede contener cualquier objeto que derive de su misma interfaz. 
            Ilavadora = Inevera; //Inevera es un objeto interfaz.
            Inevera = lavadora; //etc. Inevera e Ilavadora son la misma interfaz.
            //Tipos:
            Console.WriteLine(lavadora.GetType().ToString()); //Objeto.
            Console.WriteLine(Ilavadora.GetType().ToString()); //Interfaz. A través de una conversión podemos acceder a los métodos del objeto.
            //Colecciones:
            IEnumerable<int> numeros = new List<int>(); //Interfaz IEnumerable que comparten todas las colecciones en .NET. Solo tenemos acceso a la funcionalidad mínima.
        }
    }
}


//HERENCIA:

class DemoA
{
    public string Nombre { get; set;  }
    public string Apellidos { get; set;  }
    public int Edad { get; set;  }

    public virtual void PintaDatos() //Virtual para que los métodos se puedan sobrescribir.
    {
        Console.WriteLine($"{Nombre} {Apellidos}");
    }
}

sealed class DemoB : DemoA //SEALED para sellar la clase -> No permite que otras clases hereden de ella. Puede aplicarse a los métodos.
{
    public void PintaDatos2()
    {
        Console.WriteLine($"{Nombre} {Apellidos} - {Edad}");
    }


    public override void PintaDatos() //Sobreescribir la funcionalidad de pintar datos padre (Invalidar un método).
    {
        Console.WriteLine($"Nombre: {Nombre} {Apellidos}");
        Console.WriteLine($"Edad: {Edad}");
    }
    public void PintaDatosHijo()
    {
        PintaDatos();
    }
    public void PintaDatosPadre()
    {
        base.PintaDatos(); //BASE -> Invocar a la clase base (a) -> Para recuperar funcionalidades de la clase base. Se utiliza en los constructores.
        //Console.WriteLine($"{Edad}");
    }
}

class DemoC : List<string> //Heredar de objetos que ya existen dentro de .NET -> Todas las funcionalidades de una lista.
{
    
}


//INTEFACES:

interface IElectrodomestico
{
    //Propiedades:
    int ConsumoWatios { get; set; }
    string Nombre { get; set; }
    string Color { get; set; }

    //Definición del método pero no de la lógica que va dentro del método.
    void Encender();
    void Apagar();
}

interface IDispositivoDomotico
{
    void Encender();
    void ApagarRemoto();
}

class Nevera : IElectrodomestico, IDispositivoDomotico //Obligación de implementar toda la interface.
{
    public int ConsumoWatios { get; set; }
    public string Nombre { get; set; }
    public string Color { get; set; }
    //Podemos añadir más.

    public void Apagar() //Método de la interfaz iElectrodomestico.
    {
        Console.WriteLine("Nevera Off");
    }

    public void ApagarRemoto() //Método de la interfaz iDispositivoDomotico.
    {
        Console.WriteLine("Nevera Remoto Off");
    }

    public void Encender() //Si ambas interfaces coinciden en el nombre de los métodos podemos hacer una única implementación.
    {
        Console.WriteLine("Compartido Nevera On");
    }

    void IDispositivoDomotico.Encender() //O hacer una implementación específica para ambos como privado.
    {
        Console.WriteLine("Domotica Nevera On");
    }
}

class Lavadora : IElectrodomestico
{
    public int ConsumoWatios { get; set; }
    public string Nombre { get; set; }
    public string Color { get; set; }
    //Podemos añadir más.

    public void Apagar()
    {
        Console.WriteLine("Nevera Off");
    }

    public void Encender()
    {
        Console.WriteLine("Nevera On");
    }
}

