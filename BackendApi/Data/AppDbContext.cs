using Microsoft.EntityFrameworkCore;
using TuProyecto.Models;

namespace TuProyecto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        // public DbSet<Proveedor> Proveedores { get; set; } // ya existente
    }
}