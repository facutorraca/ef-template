using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ef_template.Data;

namespace ef_template.Models
{
    public class Superheroe
    {
        private readonly TemplateContext _context;

        public Superheroe(TemplateContext context)
        {
            _context = context;
        }

        public Superheroe()
        {
        }

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


        public virtual ICollection<Squad> Squads { get; set; }
        public virtual ICollection<PeleaContraCrimen> Peleas { get; set; }
        public virtual ICollection<PoderBasico> PoderesBasicos { get; set; }


        public IList<Crimen> CrimenesQueCombatioCon(Superheroe otroSuperheroe)
        {
            if (_context == null)
                throw new Exception("Context no inicializado");

            return _context.Crimenes.Where(crimen =>
                crimen.Peleas.Any(pelea => pelea.SuperheroeCodigo == otroSuperheroe.Codigo) &&
                crimen.Peleas.Any(pelea => pelea.SuperheroeCodigo == Codigo)
            ).ToList();
        }
    }
}