using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ef_template.Models
{
    public class Squad
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Nombre { get; set; }

        public virtual ICollection<Superheroe> Superheroes { get; set; }
    }
}