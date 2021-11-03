namespace ef_template.Models
{
    public class PeleaContraCrimen
    { 
        public bool Herido { get; set; }
        public ResultadoPelea Resultado { get; set; }


        public int CrimenId { get; set; }
        public virtual Crimen Crimen { get; set; }

        public int SuperheroeCodigo { get; set; }
        public virtual Superheroe Superheroe { get; set; }
    }
}