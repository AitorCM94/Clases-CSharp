using System;
using System.Collections.Generic;

namespace Formacion.CSharp.ConsoleAppHerencia
{
    class Program
    {
        static void Main(string[] args)
        {
            var demo = new DemoB();

            demo.Nombre = "Aitor";
            demo.Apellidos = "Cerdán";
            demo.Edad = 13;

            demo.PintaDatos();
        }
    }
}

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

sealed class DemoB : DemoA //Sealed para sellar la clase -> No permite que otras clases hereden de ella. Puede aplicarse a los métodos.
{
    public override void PintaDatos() //Sobreescribir la funcionalidad de pintar datos -> Invalidar un método.
    {
        //Console.WriteLine($"Nombre: {Nombre} {Apellidos}");
        base.PintaDatos();//Recuperar la funcionalidad de la clase padre.
        Console.WriteLine($"Edad: {Edad}");
    }

    public void PintaDatos2()
    {
        Console.WriteLine($"{Nombre} {Apellidos} - {Edad}");
    }

    public void PintaDatos3()
    {
        base.PintaDatos(); //Se utiliza mucho con el constructor. LLamada a la clase que heredas.
        Console.WriteLine($"{Edad}");
    }
}

class DemoC : List<string>
{
    //Heredar de objetos que ya existen dentro de .NET
}


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

class Nevera : IElectrodomestico //Obligación de toda la interface.
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