using System.ComponentModel.DataAnnotations;

namespace GestorEmpleados.API.Models
{
    public class RespuestaDB
    {
        [Key]
        public int Tipo_error {  get; set; }
        public string Mensaje { get; set; }
    }
}
