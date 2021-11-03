using System;
using System.Collections.Generic;

namespace ef_template.Models
{
    public class PoderBasico : Poder
    { 
        public TimeSpan TiempoDeRegeneracion { get; set; }




        public virtual ICollection<Villano> Villanos { get; set; }
        public virtual ICollection<Superheroe> Superheroes { get; set; }

    }
}