using ef_template.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ef_template.Data
{
    public class TemplateContext : DbContext
    {
        public TemplateContext(DbContextOptions<TemplateContext> options) : base(options)
        {
        }

        /*
         * An entity set typically corresponds to a database table.
         * An entity corresponds to a row in the table.
         *
         * When the database is created, EF creates tables that have names the same
         * as the DbSet property names. Property names for collections are typically plural.
         * For example, Students rather than Student.
         */

        public DbSet<Squad> Squads { get; set; }
        public DbSet<Crimen> Crimenes { get; set; }
        public DbSet<Villano> Villanos { get; set; }
        public DbSet<Superheroe> Superheroes { get; set; }
        public DbSet<Superpoder> Superpoderes { get; set; }
        public DbSet<PoderBasico> PoderesBasicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villano>()
                .HasMany(v => v.CrimenesCometidos)
                .WithOne(c => c.Villano)
                .OnDelete(DeleteBehavior.NoAction);

            
            modelBuilder.Entity<Superheroe>()
                .HasMany(s => s.PoderesBasicos)
                .WithMany("Superheroes");

            modelBuilder.Entity<Villano>()
                .HasMany(s => s.PoderesBasicos)
                .WithMany("Villanos");


            // many-to-many con la tabla intermedia descripcion
            modelBuilder.Entity<PeleaContraCrimen>()
                .HasOne(p => p.Superheroe)
                .WithMany(s => s.Peleas);


            // conversiones
            modelBuilder.Entity<PeleaContraCrimen>()
                .Property(p => p.Resultado)
                .HasConversion(new EnumToStringConverter<ResultadoPelea>());


            // indicate composite foreign key
            modelBuilder.Entity<PeleaContraCrimen>()
                .HasKey(p => new {p.CrimenId, p.SuperheroeCodigo});
        }
    }
}