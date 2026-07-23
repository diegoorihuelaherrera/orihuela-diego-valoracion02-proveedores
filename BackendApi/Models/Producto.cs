using System.ComponentModel.DataAnnotations;

namespace TuProyecto.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        // Relación opcional con Proveedor/Emprendimiento
        public int? EmprendimientoId { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}