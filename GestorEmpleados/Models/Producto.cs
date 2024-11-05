using System.ComponentModel.DataAnnotations;

namespace MiWebAPI.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        public string Color { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
    }
}

