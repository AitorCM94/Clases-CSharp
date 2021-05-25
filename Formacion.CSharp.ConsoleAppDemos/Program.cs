using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Formacion.CSharp.ConsoleAppDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            ejercicios2();
        }

        static void expresiones()
        {
            //RECORRIENDO LA LISTA:
            //0. Buscar productos con precio superior a 2:
            foreach (var item in DataLists.ListaProductos)
            {
                if (item.Precio > 2) Console.WriteLine($"{item.Descripcion} -> {item.Precio} euros.");
            }
            Console.WriteLine(Environment.NewLine); //Espacios al pintar.


            //EXPRESIONES LINQ: (como construir una sentencia de SQL)
            //1. Buscar productos con precio superior a 2:
            var resultado1 = from r in DataLists.ListaProductos where r.Precio > 2 select r; //r de registro, puede ser otra. Retorna una lista.
            foreach (var item in resultado1)
            {
                Console.WriteLine($"{item.Descripcion} -> {item.Precio} euros."); //Pintamos el resultado.
            }
            Console.WriteLine(Environment.NewLine);

            //2. Buscar productos con precio superior a 2 ORDER BY precio DESC:
            var resultado2 = from r in DataLists.ListaProductos where r.Precio > 2 orderby r.Precio descending select r;
            foreach (var item in resultado2)
            {
                Console.WriteLine($"{item.Descripcion} -> {item.Precio} euros.");
            }
            Console.WriteLine(Environment.NewLine);

            //3. Buscar LIMITANDO los datos (precio y descripción) -> Creamos un nuevo tipo de dato ANÓNIMO:
            var resultado3 = from r in DataLists.ListaProductos where r.Precio > 2 orderby r.Precio descending select new { r.Descripcion, r.Precio };
            foreach (var item in resultado3)
            {
                Console.WriteLine($"{item.Descripcion} -> {item.Precio} euros.");
            }
            Console.WriteLine(Environment.NewLine); //SIN ESTO NO PINTA EL SIGUIENTE!!!

            //4. Buscar por carácteres:
            var products = from r in DataLists.ListaProductos where r.Descripcion == "Cuaderno" select r;
            foreach (var item in products)
            {
                Console.WriteLine($"{item.Id}# {item.Descripcion} -> {item.Precio} euros.");
            }
        }

        static void metodos()
        {
            //METODO LINQ:
            //1. Buscar productos con precio superior a 2:
            var result1 = DataLists.ListaProductos.Where(x => x.Precio > 2).ToList();
            foreach (var item in result1)
            {
                Console.WriteLine($"{item.Descripcion} -> {item.Precio} euros.");
            }
            Console.WriteLine(Environment.NewLine);

            //2. Buscar productos con precio superior a 2 ORDER BY precio DESC:
            var result2 = DataLists.ListaProductos.Where(x => x.Precio > 2).OrderByDescending(x => x.Precio).ToArray();
            foreach (var item in result2)
            {
                Console.WriteLine($"{item.Descripcion} -> {item.Precio} euros.");
            }
            Console.WriteLine(Environment.NewLine);

            //3. Buscar LIMITANDO los datos (precio y descripción) -> Creamos un nuevo tipo de dato ANÓNIMO:
            var result3 = DataLists.ListaProductos.Where(x => x.Precio > 2).OrderByDescending(x => x.Precio).Select(x => new { x.Precio, x.Descripcion }).ToArray();
            foreach (var item in result3)
            {
                Console.WriteLine($"{item.Descripcion} -> {item.Precio} euros.");
            }
            Console.WriteLine(Environment.NewLine);

            //4. Buscar por carácteres:
            var productos = DataLists.ListaProductos
                .Where(r => r.Descripcion
                .StartsWith("Cuaderno")) //Que comience por. Sensible a mayúsculas y minúsculas.
                .ToList();
            foreach (var item in productos)
            {
                Console.WriteLine($"{item.Id}# {item.Descripcion} -> {item.Precio} euros.");
            }
        }

        static void ejercicios1()
        {
            //1. Lista completa de CLientes:
            var clientes1 = DataLists.ListaClientes.ToList(); //Retorna una lista que podemos recorrer con el foreach.

            var clients1 = from c in DataLists.ListaClientes select c; //Retorna una lista que podemos recorrer con el foreach.

            //2. Id igual a 2 (solo existe uno):
            var clientes2 = DataLists.ListaClientes.Where(x => x.Id == 2).FirstOrDefault(); //FirstOrDefault retorna un único elemento.
            Console.WriteLine("Id:{0} -> {1} {2}", clientes2.Id, clientes2.Nombre, clientes2.FechaNac.ToShortDateString()); //Un único elemento.

            var clients2 = from c in DataLists.ListaClientes where c.Id == 2 select c;

            //3. Nacidos entre 1980 y 1990:
            var clientes3 = DataLists.ListaClientes.Count(x => x.FechaNac.Year >= 1980 && x.FechaNac.Year <= 1990); //Usando Count en lugar de Where.
            Console.WriteLine($"Nacidos en los ochenta: {clientes3} clientes"); //Obtenemos el número.

            var clients3 = (from cliente in DataLists.ListaClientes where cliente.FechaNac.Year >= 1980 && cliente.FechaNac.Year <= 1990 select cliente).Count();

            //4. EJERCICIO BUSCAR EL PRECIO MÁS ALTO:
            //4.1 Ejercicio Aitor:
            var productos1 = from producto in DataLists.ListaProductos select producto;
            float proMax = 0;
            foreach (var p in productos1)
            {
                if (p.Precio > proMax)
                {
                    proMax = p.Precio;
                }
            }
            var producto1 = from producto in DataLists.ListaProductos where producto.Precio == proMax select producto;
            foreach (var item in producto1)
            {
                Console.WriteLine($"Mayor precio: {item.Descripcion} -> {item.Precio} euros.");
            }
            //4.2 MÉTODO Ordenación:
            var productos2 = DataLists.ListaProductos
                .OrderByDescending(r => r.Precio) //Ordenamos por precio de mayor a menor.
                .FirstOrDefault(); //Retornamos el primero.
            Console.WriteLine($"Mayor precio: {productos2.Descripcion} -> {productos2.Precio} euros.");
            //4.3 MÉTODO Max:
            var precioMax = DataLists.ListaProductos
                .Select(r => r.Precio)
                .Max(); //Podemos poner el filtro directamente en el método Max.
            var producto3 = DataLists.ListaProductos
                .Where(r => r.Precio == precioMax)
                .FirstOrDefault();
            Console.WriteLine($"Mayor precio: {producto3.Descripcion} -> {producto3.Precio} euros.");
            //4.3.1 Haciendo una SubConsulta:
            var producto4 = DataLists.ListaProductos
                .Where(r => r.Precio == DataLists.ListaProductos.Select(r => r.Precio).Max())
                .FirstOrDefault();
            Console.WriteLine($"Mayor precio: {producto4.Descripcion} -> {producto4.Precio} euros.");

            //4.4 EXPRESIÓN
            var producto5 = from producto in DataLists.ListaProductos 
                            where producto.Precio == (from r in DataLists.ListaProductos select r.Precio).Max() 
                            select producto; //Devuelve una lista si no añadimos al final (...).FirstOfDefault()
            foreach (var item in producto5)
            {
                Console.WriteLine($"Mayor precio: {item.Descripcion} -> {item.Precio} euros.");
            }
        }

        static void agrupaciones()
        {
            //PEDIDOS AGRUPADOS POR IDCLIENTE:
            //0. Sin .GroupBy (2 consultas):
            foreach (var c in DataLists.ListaClientes.ToList()) //Buscar todos los clientes.
            {
                int numPedidos = DataLists.ListaPedidos.Where(r => r.IdCliente == c.Id).Count(); //Por cada IdCliente buscar el número de Id (pedidos).
                Console.WriteLine($"{c.Nombre} - {numPedidos} pedidos.");
            }
            Console.WriteLine(Environment.NewLine);

            //1. CON GROUP BY:
            //Consulta:
            var listIdClientes = DataLists.ListaPedidos
                .GroupBy(r => r.IdCliente) //Se genera la key (propiedad por la que agrupas).
                .ToList(); //Retorna una colección de keys -> los IdClientes que son iguales agrupados en cada posición.
            //Pintamos:
            foreach (var valorDis in listIdClientes) //Recorremos la lista de Keys.
            {
                Console.WriteLine($"Clave (IdCliente): {valorDis.Key}");
                foreach (var pedido in valorDis) //Recorremos cada key para acceder a los objetos pedido y sus propiedades.
                {
                    Console.WriteLine($" -> {pedido.Id} {pedido.FechaPedido.ToShortDateString()}"); //Accedemos a las propiedades de cada objeto pedido.
                }
                Console.WriteLine("");
            }

            //2. GROUP BY + SELECT:
            var listGroupP = DataLists.ListaPedidos
                .GroupBy(r => r.IdCliente) //Agrupamos por IdCliente.
                .Select(r => new { //Objeto anónimo -> podemos ir añadiendo los valores que necesitamos (des de la DB):
                    pedidos = r, //Listado de KEYS (pedidos agrupados por X) -> Le ponemos un sinónimo: meter el dato en una variable (pedidos).
                    TotalPedidos = r.Count(), //Calcular cuantos pedidos tiene cada cliente.
                    //SubConsulta para sacar el nombre del cliente de otra lista:
                    nombreCliente = DataLists.ListaClientes.Where(s => s.Id == r.Key).Select(s => s.Nombre).FirstOrDefault()
                })
                .ToList(); //Retorna un objeto lista (IGrouping) con los pedidos agrupados por Keys + los datos de Select.
            foreach (var grupo in listGroupP)
            {
                Console.WriteLine($"{grupo.nombreCliente} - {grupo.TotalPedidos} pedidos."); //Lo sacamos de los calculos en Select.
                foreach (var pedido in grupo.pedidos) //Recorrer pedidos (= r).
                {
                    Console.WriteLine($" --> {pedido.Id} - {pedido.FechaPedido}");
                }
            }
        }

        static void ejercicios2()
        {
            //Lista CLIENTE     -> idClinete     / Nombre      / FechaNac
            //Lista PRODUCTO    -> idProducto    / Descripción / Precio
            //Lista PEDIDO      -> idPedido      / idCliente   / FechaPedido

            //Lista LineaPedido -> idLineaPedido / idPedido    / idProducto  / Cantidad


            //1. AGRUPAR Lista LineaPedido POR idPedido / CONTAR "productos" DE CADA idPedido / SUMAR cantidad DE CADA "producto" EN CADA idPedido:
            var lineasPedidos = DataLists.ListaLineasPedido
                .GroupBy(r => r.IdPedido)
                .Select(r => new {
                    productos = r, //Cada objeto de la Lista LineaPedido. Te estas traiendo toda la información.
                    idPedido = r.Key, //Las claves (idPedido) de cada grupo.
                    numProductos = r.Count(), //Suma del número de objetos en cada grupo.
                    cantidad = r.Sum(s => s.Cantidad) //Suma de la propiedad cantidad en cada grupo.
                }) //Solo podemos acceder a las variables dentro de Select. Recomendable cuando no quieres traerte todos los datos (en este caso si).
                .ToList(); //Retorna un objeto lista con las líneas agrupadas -> Keys

            foreach (var pedido in lineasPedidos) //Recorremos los pedidos:
            {
                Console.WriteLine($"Id Pedido: {pedido.idPedido} - Productos: {pedido.numProductos} -> Cantidades: {pedido.cantidad}");
                foreach (var prod in pedido.productos) //Recorremos cada línea de pedido, es decir, los productos de cada pedido:
                {
                    Console.WriteLine($" -> Id Producto: {prod.IdProducto} - Cantidad: {prod.Cantidad}");
                }
            }
            Console.WriteLine(Environment.NewLine);

            //2. Hacer una subconsulta para sacar de la lista Productos el precio y la descripción:
                //-> Calcular el precio de cada producto en cada pedido: Cantidad (lista LineaPedido) * precio (lista Productos).
                //-> Calcular el precio de cada pedido: La suma de Cantidades (var LineasPedidos) * precio (lista Productos).

            //Importe de cada pedido: (el precio está en Producto) -> Pintar el precio y la descripción.
            var lineasPedidos2 = DataLists.ListaLineasPedido
                .GroupBy(r => r.IdPedido)
                .Select(r => new {
                    r.Key, //Clave del pedido (IdPedido)
                    totalPedido = r.Sum( x => //La suma para calcular el precio total de cada pedido.
                        x.Cantidad * DataLists.ListaProductos //Multiplicamos el precio por la cantidad (subconsulta).
                            .Where(s => s.Id == x.IdProducto) //Igualamos el Id de producto con el idProducto de la línea de pedidos.
                            .Select(p => p.Precio) //Seleccionamos el precio.
                            .FirstOrDefault()) // Cogemos un solo dato.
                })
                .ToList();
            foreach (var pedido in lineasPedidos2)
            {
                //Console.WriteLine($"Consulta: {pedido}");
                Console.WriteLine($"Clave (IdPedido): {pedido.Key} - Total: {pedido.totalPedido}");
            }
        }
    }
}

//Classes que hacen de getters y setters de una base de datos:
public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaNac { get; set; }
} //Clase cliente con 3 propiedades que funcionan como si fueran variables (son públicas).

public class Producto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public float Precio { get; set; }
} //Clase producto con 3 propiedades que funcionan como si fueran variables (son públicas).

public class Pedido
{
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public DateTime FechaPedido { get; set; }
} //Clase cliente con 3 propiedades que funcionan como si fueran variables (son públicas).

public class LineaPedido
{
    public int Id { get; set; }
    public int IdPedido { get; set; }
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }
} //Clase con la línea del pedido con 4 propiedades que funcionan como si fueran variables (son públicas).

public static class DataLists //Clase que contiene los datos (guardados en listas) -> Funciona como una base de datos:
{   //  [variable:]   [tipo]        [nombre]       [instancia lista]  { [la llena de objetos cliente] }
    private static List<Cliente> _listaClientes = new List<Cliente>() { //Declara una variable con una lista de objetos que inicializa directamente.
            new Cliente { Id = 1,   Nombre = "Carlos Gonzalez Rodriguez",   FechaNac = new DateTime(1980, 10, 10) },
            new Cliente { Id = 2,   Nombre = "Luis Gomez Fernandez",        FechaNac = new DateTime(1961, 4, 20) },
            new Cliente { Id = 3,   Nombre = "Ana Lopez Diaz ",             FechaNac = new DateTime(1947, 1, 15) },
            new Cliente { Id = 4,   Nombre = "Fernando Martinez Perez",     FechaNac = new DateTime(1981, 8, 5) },
            new Cliente { Id = 5,   Nombre = "Lucia Garcia Sanchez",        FechaNac = new DateTime(1973, 11, 3) }
        }; //Una lista de OBJETOS cliente con los datos Id, Nombre y FechaNac (propiedades).

    private static List<Producto> _listaProductos = new List<Producto>()
        {
            new Producto { Id = 1,      Descripcion = "Boligrafo",          Precio = 0.35f },
            new Producto { Id = 2,      Descripcion = "Cuaderno grande",    Precio = 1.5f },
            new Producto { Id = 3,      Descripcion = "Cuaderno pequeño",   Precio = 1 },
            new Producto { Id = 4,      Descripcion = "Folios 500 uds.",    Precio = 3.55f },
            new Producto { Id = 5,      Descripcion = "Grapadora",          Precio = 5.25f },
            new Producto { Id = 6,      Descripcion = "Tijeras",            Precio = 2 },
            new Producto { Id = 7,      Descripcion = "Cinta adhesiva",     Precio = 1.10f },
            new Producto { Id = 8,      Descripcion = "Rotulador",          Precio = 0.75f },
            new Producto { Id = 9,      Descripcion = "Mochila escolar",    Precio = 12.90f },
            new Producto { Id = 10,     Descripcion = "Pegamento barra",    Precio = 1.15f },
            new Producto { Id = 11,     Descripcion = "Lapicero",           Precio = 0.55f },
            new Producto { Id = 12,     Descripcion = "Grapas",             Precio = 0.90f }
        }; //Una lista de OBJETOS producto con los datos Id, Descripción y Precio (propiedades).

    private static List<Pedido> _listaPedidos = new List<Pedido>()
        {
            new Pedido { Id = 1,     IdCliente = 1,  FechaPedido = new DateTime(2013, 10, 1) },
            new Pedido { Id = 2,     IdCliente = 1,  FechaPedido = new DateTime(2013, 10, 8) },
            new Pedido { Id = 3,     IdCliente = 1,  FechaPedido = new DateTime(2013, 10, 12) },
            new Pedido { Id = 4,     IdCliente = 1,  FechaPedido = new DateTime(2013, 11, 5) },
            new Pedido { Id = 5,     IdCliente = 2,  FechaPedido = new DateTime(2013, 10, 4) },
            new Pedido { Id = 6,     IdCliente = 3,  FechaPedido = new DateTime(2013, 7, 9) },
            new Pedido { Id = 7,     IdCliente = 3,  FechaPedido = new DateTime(2013, 10, 1) },
            new Pedido { Id = 8,     IdCliente = 3,  FechaPedido = new DateTime(2013, 11, 8) },
            new Pedido { Id = 9,     IdCliente = 3,  FechaPedido = new DateTime(2013, 11, 22) },
            new Pedido { Id = 10,    IdCliente = 3,  FechaPedido = new DateTime(2013, 11, 29) },
            new Pedido { Id = 11,    IdCliente = 4,  FechaPedido = new DateTime(2013, 10, 19) },
            new Pedido { Id = 12,    IdCliente = 4,  FechaPedido = new DateTime(2013, 11, 11) }
        }; //Una lista de OBJETOS pedido con los datos Id, IdCliente y FechaPedido (propiedades).

    private static List<LineaPedido> _listaLineasPedido = new List<LineaPedido>()
        {
            new LineaPedido() { Id = 1,  IdPedido = 1,  IdProducto = 1,     Cantidad = 9 },
            new LineaPedido() { Id = 2,  IdPedido = 1,  IdProducto = 3,     Cantidad = 7 },
            new LineaPedido() { Id = 3,  IdPedido = 1,  IdProducto = 5,     Cantidad = 2 },
            new LineaPedido() { Id = 4,  IdPedido = 1,  IdProducto = 7,     Cantidad = 2 },
            new LineaPedido() { Id = 5,  IdPedido = 2,  IdProducto = 9,     Cantidad = 1 },
            new LineaPedido() { Id = 6,  IdPedido = 2,  IdProducto = 11,    Cantidad = 15 },
            new LineaPedido() { Id = 7,  IdPedido = 3,  IdProducto = 12,    Cantidad = 2 },
            new LineaPedido() { Id = 8,  IdPedido = 3,  IdProducto = 1,     Cantidad = 4 },
            new LineaPedido() { Id = 9,  IdPedido = 4,  IdProducto = 2,     Cantidad = 3 },
            new LineaPedido() { Id = 10, IdPedido = 5,  IdProducto = 4,     Cantidad = 2 },
            new LineaPedido() { Id = 11, IdPedido = 6,  IdProducto = 1,     Cantidad = 20 },
            new LineaPedido() { Id = 12, IdPedido = 6,  IdProducto = 2,     Cantidad = 7 },
            new LineaPedido() { Id = 13, IdPedido = 7,  IdProducto = 1,     Cantidad = 4 },
            new LineaPedido() { Id = 14, IdPedido = 7,  IdProducto = 2,     Cantidad = 10 },
            new LineaPedido() { Id = 15, IdPedido = 7,  IdProducto = 11,    Cantidad = 2 },
            new LineaPedido() { Id = 16, IdPedido = 8,  IdProducto = 12,    Cantidad = 2 },
            new LineaPedido() { Id = 17, IdPedido = 8,  IdProducto = 3,     Cantidad = 9 },
            new LineaPedido() { Id = 18, IdPedido = 9,  IdProducto = 5,     Cantidad = 11 },
            new LineaPedido() { Id = 19, IdPedido = 9,  IdProducto = 6,     Cantidad = 9 },
            new LineaPedido() { Id = 20, IdPedido = 9,  IdProducto = 1,     Cantidad = 4 },
            new LineaPedido() { Id = 21, IdPedido = 10, IdProducto = 2,     Cantidad = 7 },
            new LineaPedido() { Id = 22, IdPedido = 10, IdProducto = 3,     Cantidad = 2 },
            new LineaPedido() { Id = 23, IdPedido = 10, IdProducto = 11,    Cantidad = 10 },
            new LineaPedido() { Id = 24, IdPedido = 11, IdProducto = 12,    Cantidad = 2 },
            new LineaPedido() { Id = 25, IdPedido = 12, IdProducto = 1,     Cantidad = 20 }
        }; //Una lista de OBJETOS con los datos Id, IdPedido, IdProducto y Cantidad (propiedades).

    // Propiedades de los elementos privados -> Expone las variables privadas de la clase. Sólo puedes acceder a estas propiedades:
    public static List<Cliente> ListaClientes { get { return _listaClientes; } } //No tienen set, no se puede hacer modificaciones.
    public static List<Producto> ListaProductos { get { return _listaProductos; } } //Retorna lo que contiene la variable _listaProductos.
    public static List<Pedido> ListaPedidos { get { return _listaPedidos; } }
    public static List<LineaPedido> ListaLineasPedido { get { return _listaLineasPedido; } }
}