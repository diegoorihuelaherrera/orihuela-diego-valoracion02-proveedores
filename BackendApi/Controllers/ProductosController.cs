using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuProyecto.Data;
using TuProyecto.Models;

namespace TuProyecto.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }
        // GET: api/productos/buscar?nombre=camisa
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Producto>>> Buscar([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
              return Ok(new List<Producto>());

         var productos = await _context.Productos
             .Where(p => p.Nombre.Contains(nombre))
              .ToListAsync();

         return Ok(productos);
        }

        // GET: api/productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound(new { mensaje = $"Producto con id {id} no encontrado" });

            return Ok(producto);
        }

        // POST: api/productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            producto.FechaRegistro = DateTime.Now;
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        // PUT: api/productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
                return BadRequest(new { mensaje = "El id de la URL no coincide con el del producto" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existente = await _context.Productos.FindAsync(id);
            if (existente == null)
                return NotFound(new { mensaje = $"Producto con id {id} no encontrado" });

            existente.Nombre = producto.Nombre;
            existente.Descripcion = producto.Descripcion;
            existente.Precio = producto.Precio;
            existente.Stock = producto.Stock;
            existente.EmprendimientoId = producto.EmprendimientoId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound(new { mensaje = $"Producto con id {id} no encontrado" });

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
    }
}