using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace L01_2022EO650_2022HC650.Models
{
    public class Clientes
    {
        [Key]
        public int clienteId { get; set; }

        [Required]
        [StringLength(200)]
        public string nombreCliente { get; set; }

        [StringLength(500)]
        public string direccion { get; set; }
    }
}
