using System;
using Data.Models;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CSharp.EntityFram.ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            EntFramInclude();
        }

        static void ADONET()
        {
            //CONEXIÓN CON ADO:

            //1. Crear un objeto para definir la cadena de conexión:
            var connectionString = new SqlConnectionStringBuilder() //Constructor de cadenas de conexión.
            {
                DataSource = "LOCALHOST",
                InitialCatalog = "NORTHWIND",
                UserID = "",
                Password = "",
                IntegratedSecurity = true,
                ConnectTimeout = 60
            };
            Console.WriteLine("Cadena de conexión: {0}", connectionString);

            //2. Creamos un objeto conexión, representa la conexión con la base de datos:
            var connect = new SqlConnection(connectionString.ToString()); //Como parámetro le pasamos la cadena de conexión.

            Console.WriteLine($"Estado: {connect.State.ToString()}"); //To string lo transforma en texto.
            connect.Open(); //Abrimos la conexión.
            Console.WriteLine($"Estado: {connect.State.ToString()}");


            //3. Creamos un objeto Command que nos permite lanzar comandos contra la base de datos:
            var command = new SqlCommand()
            {
                Connection = connect,
                CommandText = "SELECT * FROM dbo.Customers" //Ejecutamos el comando.
            };

            //4. Creamos un objeto que funcione como cursor, permitiendo recorrer los datos retornados por la base de datos:
            var reader = command.ExecuteReader();

            if (!reader.HasRows) //La propiedad HasRows para determinar si tenemos información.
            {
                Console.WriteLine("Registros no encontrados.");
            }
            else
            {
                while(reader.Read()) //Leemos y pintamos la información.
                {
                    Console.WriteLine($"ID: {reader["CustomerID"]}");
                    Console.WriteLine($"Empresa: {reader.GetValue(1)}");
                    Console.WriteLine($"Pais: {reader["Country"]}" + Environment.NewLine);
                }
            }

            //5. Cerramos conexiones y destruimos variables.
            reader.Close();
            command.Dispose();
            connect.Close();
            connect.Dispose();
        }

        static void EntFram()
        {
            //CONEXIÓN CON ENTITY FRAMEWORK:

            //Crear las clases que representen cada una de las tablas (apuntes). Entre ellas la clase del CONTEXTO -> ModelNorthwind

            //INSTANCIAMOS LA CLASE DEL CONTEXTO:
            var context = new ModelNorthwind();

            //Tenemos acceso a cada una de las tablas, representadas por una clase:
            var clientes1 = context.Customers.ToList();
            var clientes2 = from c in context.Customers select c;


            //CONSULTA DE DATOS:

            //01. Consulta de Datos - SELECT:
            var clientes3 = context.Customers //Utilizamos simpre la clase del contexto.
                .Where(r => r.Country == "Spain") //Filtrado.
                .OrderBy(r => r.City) //Ordenación.
                .ThenBy(r => r.CompanyName) //Varias ordenaciones.
                .ToList();

            var clientes4 = from c in context.Customers //En expresión linq
                            where c.Country == "Spain"
                            orderby c.City
                            select c;

            foreach (var c in clientes4)
            {
                Console.WriteLine($"ID: {c.CustomerID}");
                Console.WriteLine($"Empresa: {c.CompanyName}");
                Console.WriteLine($"Pais: {c.Country}" + Environment.NewLine);
            }


            //02. Insertar Datos - INSERT:
            //Creamos un nuevo cliente:
            var cliente = new Customers(); //Instanciar el cliente
            cliente.CustomerID = "DEMO1"; //Rellenar los campos (propiedades)...
            cliente.CompanyName = "Empresa Uno, SL";
            cliente.ContactName = "Aitor Cerdán";
            //Insertamos al nuevo cliente:
            context.Customers.Add(cliente); //Añadir el cliente como de una lista se tratara.
            context.SaveChanges(); //Confirmar los cambios -> Como el commit en python.


            //03. Modificar Datos - UPDATE:
            //Me posiciono en el registro -> Buscar el cliente:
            var cliente2a = context.Customers
                .Where(r => r.CustomerID == "DEMO1")
                .FirstOrDefault();

            var cliente2b = (from c in context.Customers
                            where c.CustomerID == "DEMO1"
                            select c).FirstOrDefault();
            //Hago los cambios:
            cliente2a.ContactTitle = "Boss";
            context.SaveChanges(); //Confirmar los cambios.


            //04. Eliminar Datos - DELET:
            //Hacer la búsqueda (igual):
            var cliente3a = context.Customers
                .Where(r => r.CustomerID == "DEMO1")
                .FirstOrDefault();
            //Eliminar:
            context.Customers.Remove(cliente3a); //Un elemento.
            //context.Customers.RemoveRange(context.Customers.Where(r => r.Country == "Spain").ToList()); //Para listas.
            context.SaveChanges(); //Confirmar los cambios.
        }

        static void EntFramInclude()
        {
            var context = new ModelNorthwind();
            //00. Las dos consultas por separado:
            //Consulta para obtener los datos del cliente:
            var cliente = context.Customers
                .Where(r => r.CustomerID == "ANATR")
                .FirstOrDefault();
            //Consulta para obtener los datos del pedido...
            var pedidos = context.Orders
                .Where(r => r.CustomerID == "ANATR")
                .ToList();

            //01. La consulta con Include (JOIN):
            //JOIN - Elementos virtuales:
            var cliente2 = context.Customers //De la tabla Customers.
                .Include(r => r.Orders) //Nos traemos los datos del pedido.
                .Where(r => r.CustomerID == "ANATR") //De ANATR.
                .FirstOrDefault();

            var cliente2b = (from c in context.Customers //En sentencia, muy similar TransatSQL
                             join o in context.Orders on c.CustomerID equals o.CustomerID
                             where c.CustomerID == "ANATR"
                             select c).FirstOrDefault();

            Console.WriteLine("Cliente: {0} {1}", cliente2.CustomerID, cliente2.ContactName); //Pintamos datos del cliente.

            foreach (var p in cliente2.Orders) //Recorremos para pintar datos de los pedidos.
            {
                Console.WriteLine($"  -> Pedido {p.OrderID}, {p.OrderDate}");
            }
        }
    }
}