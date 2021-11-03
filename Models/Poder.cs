using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ef_template.Models
{
    public abstract class Poder
    { 
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Nombre { get; set; }

        [DisplayName("Daño")]
        public decimal Danio { get; set; }
    }
}