using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace L01_2022EO650_2022HC650.Models
{
    public class Platos
    {
        [Key]
        public int platoId { get; set; }

        [Required]
        [StringLength(200)]
        public string nombrePlato { get; set; }

        [Column(TypeName = "numeric(18,4)")]
        public decimal precio { get; set; }
    }
}
