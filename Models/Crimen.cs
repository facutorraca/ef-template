using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ef_template.Models
{
    public class Crimen
    {
        public int Id { get; set; }

        public Direccion Direccion { get; set; }

        public int VillanoCodigo { get; set; }
        public virtual Villano Villano { get; set; }


        public virtual ICollection<PeleaContraCrimen> Peleas { get; set; }
    }


    [Owned]
    public class Direccion
    {
        [Required]
        [MaxLength(100)]
        public string Ciudad { get; set; }
        
        
        [Required]
        [MaxLength(255)]
        public string CalleOLugar { get; set; }
    }
}