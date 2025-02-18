using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace L01_2022EO650_2022HC650.Models
{
    public class Pedidos
    {
        [Key]
        public int pedidoId { get; set; }

        public int motoristaId { get; set; }
        public int clienteId { get; set; }
        public int platoId { get; set; }

        public int cantidad { get; set; }

        [Column(TypeName = "numeric(18,4)")]
        public decimal precio { get; set; }
    }
}
