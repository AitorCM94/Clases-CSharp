using System;
using System.Net.Http; //2. Espacio de nombres para instanciar el objeto HttpClient.
using Newtonsoft.Json; //7. (Incorporar la librería) Instanciar el objeto para des/serializar JSON.
using System.Collections.Generic; //7. Espacio de nombres para especificar el tipo <List>.

using Data.Models; //Incorporar nuestra librería.

using System.Net.Http.Formatting; //(incorporar libreria) -> HttpClientImplicita
using System.Net.Http.Json; //(incorporar libreria) -> HttpClientImplicita

using System.Net.Http.Headers; //Espacio de nombres para las cabeceras.


namespace Formacion.CSharp.ConsoleAppAPIClient
{
    class Program
    {
        //Variables globales -> públicas, estáticas y de solo lectura:
        static readonly HttpClient http = new HttpClient(); //2. Instanciar una única vez para toda la aplicación.
        static readonly string url = "http://api.labs.com.es/v1.0/"; //1. URL básica para la conexión con el Microservicio.

        static void Main(string[] args)
        {
            Ejercicio3DELETE();
        }

        static void HttpClientWithDynamic()
        {
            //3. Definimos la dirección base:
            http.BaseAddress = new Uri(url); //Uri -> Tipo de objeto para manejar urls.

            //4. Métodos para hacer las LLAMADAS en los diferentes verbos:
            var response = http.GetAsync("clientes.ashx?all") //Variable respuesta -> Retorna una tarea (método asíncrono).
                .Result; //Para capturar el resultado -> Obtier el resultado de la tarea.

            //5. Coger la respuesta y analizar el código de estado:
            if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //6. Cogemos la respuesta y accedemos al contenido:
                var content = response.Content.ReadAsStringAsync().Result; //Retorna un texto que representa el JSON.
                //Console.WriteLine("Respuesta JSON: {0}", content);

                //7. Deserializar el contenido:
                var clientes = JsonConvert.DeserializeObject<List<dynamic>>(content); //Transformar la respuesta en una LISTA (texto con []).

                //8. Coger y recorrer la lista de clientes:
                foreach (var c in clientes) //Recorremos un objeto dinámico -> sin intellisense ni errores detectables.
                {
                    Console.WriteLine($"{c.CustomerID}# {c.CompanyName} - {c.Country}");
                }
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void HttpClientWithCustomers()
        {
            http.BaseAddress = new Uri(url);

            var response = http.GetAsync("clientes.ashx?all").Result;

            if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine("Respuesta JSON: {0}", content);

                var clientes = JsonConvert.DeserializeObject<List<Customers>>(content); //Con customers -> Incorporamos los objetos de la libreria.

                foreach (var c in clientes)
                {
                    Console.WriteLine($"{c.CustomerID}# {c.CompanyName} - {c.Country}");
                }
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void HttpClientImplicita()
        {
            //Llamada y deserialización de una sola vez:
            http.BaseAddress = new Uri(url);

            //Uso del paquete AspNet.WebApi.Client.
            var clientes = http.GetFromJsonAsync<List<Customers>>("clientes.ashx?all").Result; //Coger el resultado y transformarlo en una lista de clientes.

            foreach (var c in clientes)
            {
                Console.WriteLine($"{c.CustomerID}# {c.CompanyName} - {c.Country}");
            }
        }

        static void HttpClientWithHeaders() //MENSAJE DE PETICIÓN (+ contenido)
        {
            //1. Instanciamos el objeto Request -> Mensaje de peticion:
            var request = new HttpRequestMessage(HttpMethod.Get, "http://api.labs.com.es/v1.0/clientes.ashx?all"); //Colección con cabeceras. (Epecificamos el método y la url)

            //2. Borrar cabeceras por defecto (opcional):
            request.Headers.Clear();

            //3. Añadir cabeceras -> clave-valor:
            request.Headers.Add("ContentType", "application/json");
            //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //Añadirla instanciando un objeto.
            request.Headers.Add("User-Agent", "ConsoleApp for Northwind");
            request.Headers.Add("Authorization", "Key of access");

            //5. Con el método Send hago el envío del (mensaje):
            var response = http.SendAsync(request).Result; //Respuesta.

            //6. Leemos el contenido igual que antes:
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var clientes = JsonConvert.DeserializeObject<List<Customers>>(content);

                foreach (var c in clientes)
                {
                    Console.WriteLine($"{c.CustomerID}# {c.CompanyName} - {c.Country}");
                }
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void HttpClientWithHeaders2() //SIN MENSAJE DE PETICIÓN
        {
            //INCORPORAR CABECERAS CON EL OBJETO HTTPCLIENT -> SIN CONTENIDO:
            http.BaseAddress = new Uri(url);

            //Uso de la propiedad DefaultRequestHeaders:
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("ContentType", "application/json");
            http.DefaultRequestHeaders.Add("User-Agent", "ConsoleApp for Northwind");
            http.DefaultRequestHeaders.Add("Authorization", "Key of access");

            var response = http.GetAsync("clientes.ashx?all").Result;

            if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine("Respuesta JSON: {0}", content);

                var clientes = JsonConvert.DeserializeObject<List<Customers>>(content);

                foreach (var c in clientes)
                {
                    Console.WriteLine($"{c.CustomerID}# {c.CompanyName} - {c.Country}");
                }
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void HttpClientPost() //PARA ENVIAR CONTENIDO - POST
        {
            //1. Dirección base de ECHO:
            http.BaseAddress = new Uri("http://postman-echo.com/");

            //2. Instanciamos un objeto de la base de datos -> Objeto a enviar:
            var region = new Region() { RegionID = 10, RegionDescription = "Madrid" }; //Añadimos identificador y descripción.

            //3. Convertir el objeto a JSON -> Uso del método Serialize:
            var regionJSON = JsonConvert.SerializeObject(region); //Tenemos el objeto en formato JSON.
            //Console.WriteLine($"Región en JSON: {regionJSON}");

            //4. Objeto Http de tipo content -> Instanciamos el método StringContent:
            var content = new StringContent(regionJSON, System.Text.Encoding.UTF8, "application/json"); //(datos, encoding, contentType)

            //5. Llamada con el método Post (url, contenido)
            var response = http.PostAsync("post", content).Result;

            //6. Preguntamos:
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //7. Pintamos el contenido de la respuesta:
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Respuesta: {responseContent}");
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void Ejercicio1()
        {
            http.BaseAddress = new Uri("http://ip-api.com/json/");

            Console.Write("Dirección IP: "); //193.146.141.207
            string IP = Console.ReadLine();

            var response = http.GetAsync(IP).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine("Respuesta JSON: {0}", content);

                var respuesta = JsonConvert.DeserializeObject<dynamic>(content);
                Console.WriteLine($"{respuesta.city}, {respuesta.country}");

                //foreach (var c in respuesta)
                //{
                //    Console.WriteLine($"{c}");
                //}
                //GITHUB BORJA
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void Ejercicio2()
        {
            http.BaseAddress = new Uri("https://localhost:44376/api/v1.0/");

            Console.Clear(); //Limpiar la consola.
            Console.Write("ID Empleado: "); //1-5
            string id = Console.ReadLine();

            var response = http.GetAsync($"empleados.ashx?id={id}").Result; //Abre una comunicación de tipo get con la url de base + ().

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contenttype = response.Content.Headers.ContentType.MediaType; //Cogemos el CONTENT TYPE.

                var content = response.Content.ReadAsStringAsync().Result; //Cogemos el mensaje de respuesta (cabecera y contenido).
                //Console.WriteLine("Respuesta JSON: {0}", content);

                if (contenttype == "text/plain")
                {
                    Console.WriteLine(content);
                }
                else if (contenttype == "application/json")
                {
                    var respuesta = JsonConvert.DeserializeObject<Employees>(content);

                    Console.WriteLine($"{respuesta.FirstName} {respuesta.LastName}");
                }
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void Ejercicio3GET()
        {
            //Preguntar por el identificador de un producto y si pone all mostrar el listado.
            http.BaseAddress = new Uri("https://localhost:44396/api/"); //products/id
            Console.Clear();
            Console.Write("ID Producto: ");
            string id = Console.ReadLine();

            if (id == "all")
            {
                var response = http.GetAsync("products").Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var contenttype = response.Content.Headers.ContentType.MediaType;

                    var content = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine("Respuesta JSON: {0}", content);

                    if (contenttype == "text/plain")
                    {
                        Console.WriteLine(content);
                    }
                    else if (contenttype == "application/json")
                    {
                        var respuesta = JsonConvert.DeserializeObject<List<Products>>(content);

                        foreach (var item in respuesta)
                        {
                            Console.WriteLine($"{item.ProductName}");
                        }
                    }
                }
                else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
            }
            else
            {
                var response = http.GetAsync($"products/{id}").Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var contenttype = response.Content.Headers.ContentType.MediaType;

                    var content = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine("Respuesta JSON: {0}", content);

                    if (contenttype == "text/plain")
                    {
                        Console.WriteLine(content);
                    }
                    else if (contenttype == "application/json")
                    {
                        var respuesta = JsonConvert.DeserializeObject<Products>(content);

                        Console.WriteLine($"{respuesta.ProductName}");
                    }
                }
                else Console.WriteLine("Error: {0}", response.StatusCode.ToString());

                //GITHUB BORJA
            }
        }

        static void Ejercicio3POST()
        {
            //Añadir un producto:

            HttpResponseMessage response;
            http.BaseAddress = new Uri("https://localhost:44396/api/"); //products/id

            Products producto = new Products();
            //producto.ProductName = "Sillas";
            //producto.UnitPrice = 10;
            //producto.UnitsInStock = 1000;
            //Preguntando:
            Console.Clear();
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            producto.ProductName = nombre;
            Console.Write("Precio: ");
            string precio = Console.ReadLine();
            producto.UnitPrice = Convert.ToDecimal(precio);
            Console.Write("Unidades: ");
            string unidades = Console.ReadLine();
            producto.UnitsInStock = Convert.ToSByte(unidades);

            var productJSON = JsonConvert.SerializeObject(producto);
            //Console.WriteLine($"Región en JSON: {productJSON}");

            //4. Objeto Http de tipo content -> Instanciamos el método StringContent: (contenido en formato texto)
            var content = new StringContent(productJSON, System.Text.Encoding.UTF8, "application/json"); //(datos, encoding, contentType)

            //5. Llamada con el método Post (url, contenido)
            response = http.PostAsync("products", content).Result;

            //6. Preguntamos:
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                //7. Pintamos el contenido de la respuesta:
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Respuesta: {responseContent}");
            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void Ejercicio3PUT()
        {
            //Coger un objeto y actualizarlo.
            //HttpResponseMessage response;
            http.BaseAddress = new Uri("https://localhost:44396/api/"); //products/id

            Console.Clear();
            Console.Write("ID Producto: ");
            string id = Console.ReadLine();

            var request = new HttpRequestMessage(HttpMethod.Get, $"products/{id}");

            var response = http.SendAsync(request).Result;


            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine($"Respuesta: {content}");

                var producto = JsonConvert.DeserializeObject<Products>(content);

                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();
                producto.ProductName = nombre;
                Console.Write("Precio: ");
                string precio = Console.ReadLine();
                producto.UnitPrice = Convert.ToDecimal(precio);
                Console.Write("Unidades: ");
                string unidades = Console.ReadLine();
                producto.UnitsInStock = Convert.ToSByte(unidades);

                var productJSON = JsonConvert.SerializeObject(producto);
                Console.WriteLine($"Región en JSON: {productJSON}");

                var content2 = new StringContent(productJSON, System.Text.Encoding.UTF8, "application/json");

                response = http.PutAsync($"https://localhost:44396/api/products/{id}", content2).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine($"Producto modificado correctamente.");
                }
                else Console.WriteLine("Error: {0}", response.StatusCode.ToString());

            }
            else Console.WriteLine("Error: {0}", response.StatusCode.ToString());
        }

        static void Ejercicio3DELETE()
        {
            //Preguntar por un id y borrar (uno que creemos).
            //Coger un objeto y actualizarlo.
            //HttpResponseMessage response;
            http.BaseAddress = new Uri("https://localhost:44396/api/"); //products/id

            Console.Clear();
            Console.Write("ID Producto: ");
            string id = Console.ReadLine();

            Console.Write("Borrar: ");
            string sino = Console.ReadLine();

            if (sino == "si")
            {
                var response = http.DeleteAsync($"products/{id}").Result;
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }
        }
    }
}
