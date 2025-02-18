using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace L01_2022EO650_2022HC650.Models
{
    public class Motoristas
    {
        [Key]
        public int motoristaId { get; set; }

        [Required]
        [StringLength(200)]
        public string nombreMotorista { get; set; }
    }
}
