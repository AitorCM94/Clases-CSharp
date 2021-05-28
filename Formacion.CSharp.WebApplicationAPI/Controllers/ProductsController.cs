using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace WebApplicationAPI.Controllers
{
    [Route("api/[controller]")] //Decorador que determina la ruta -> URL: api/comodín.
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ModelNorthwind _context; //Propiedad que representa el contexto de la base de datos.

        public ProductsController(ModelNorthwind context) //Constructor -> Recibe la instancia definida en el startup.
        {
            _context = context;
        }

        //MÉTODOS ASÍNCRONOS:
        // GET: api/Products -> Retorna todos los productos.
        [HttpGet] //Decorador que añade funcionalidad al método. Determina el método que se ejecuta en función del verbo de la comunicación.
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5 -> Retorna el producto por el id.
        [HttpGet("{id}")] //Utiliza un comodín para el parámetro que añadir en la URL.
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var products = await _context.Products.FindAsync(id); //Utiliza el find para buscar el valor de la clave primaria (valores únicos).
            //var products = await _context.Products
            //    .Where(r => r.ProductID == id)
            //    .FirstOrDefaultAsync();

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products/5 -> Para las modificaciones.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Products products) //Recibe un id y un producto.
        {
            if (id != products.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified; //Cambia el estado del producto.

            try
            {
                await _context.SaveChangesAsync(); //Guarda los cambios.
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); //Retorna un código 204.
        }

        // POST: api/Products -> Añadir un nuevo producto.
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            _context.Products.Add(products); //Añadir el producto.
            await _context.SaveChangesAsync(); //Guardar cambios.

            return CreatedAtAction("GetProducts", new { id = products.ProductID }, products); //Retornar código de estado + el producto con los id.
        }

        // DELETE: api/Products/5 -> Borrado.
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return products; //Retorna el producto. Implícito con un código de estado 200.
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}