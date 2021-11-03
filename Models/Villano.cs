using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ef_template.Models
{
    public class Villano
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(255)]
        public string NombreReal { get; set; }


        public int SuperpoderId { get; set; }
        public virtual Superpoder Superpoder { get; set; }



        public virtual ICollection<Crimen> CrimenesCometidos { get; set; }
        public virtual ICollection<PoderBasico> PoderesBasicos { get; set; }
    }

    
}